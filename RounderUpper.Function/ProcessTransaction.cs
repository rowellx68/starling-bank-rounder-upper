using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RounderUpper.Function.Extensions;
using RounderUpper.Function.Utilities;
using RounderUpper.Function.Validators;
using StarlingBank.Contracts.Common;
using StarlingBank.Contracts.SavingsGoals;

namespace RounderUpper.Function
{
    public static class ProcessTransaction
    {
        private static readonly JsonMediaTypeFormatter Formatter = new JsonMediaTypeFormatter
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        };

        [FunctionName("process-transaction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")]string data, 
            [Table("transactions", "AzureWebJobsStorage")]CloudTable table, HttpRequestMessage req, TraceWriter log)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                log.Error("Webhook content empty.");
                return req.CreateResponse(HttpStatusCode.OK);
            }

            var http = ProcessTransaction.GetHttpClient();

#if !DEBUG
            req.Headers.TryGetValues("X-Hook-Signature", out var headers);
            var sign = headers?.FirstOrDefault();

            var secret = Environment.GetEnvironmentVariable("STARLING_WEBHOOK_SECRET", EnvironmentVariableTarget.Process);
            var (_, valid) = WebhookPayloadValidator.Validate(sign, secret, data);

            if (!valid)
            {
                log.Error("Webhook signature mismatch. Rejected.");

                http.Dispose();
                return req.CreateResponse(HttpStatusCode.OK);
            }

#endif

            var payload = StarlingDeserialiser.WebhookPayload(data);

            var existing = await table.GetExistingTransaction(payload.AccountHolderUid, payload.Content.TransactionUid);
            if (existing != null)
            {
                log.Warning($"Transaction {existing.RowKey} already processed on {existing.Timestamp}.");

                http.Dispose();
                return req.CreateResponse(HttpStatusCode.OK);
            }

            var goalId = Environment.GetEnvironmentVariable("STARLING_GOAL_ID", EnvironmentVariableTarget.Process);
            var amountInPennies = (long)(Math.Abs(payload.Content.Amount) * 100 % 100);

            var topUpRequest = new TopUpRequest
            {
                Amount = new CurrencyAndAmount
                {
                    Currency = "GBP",
                    MinorUnits = 100 - amountInPennies
                }
            };

            await http.PutAsync($"v1/savings-goals/{goalId}/add-money/{Guid.NewGuid()}", topUpRequest, Formatter);
            await table.SaveTransaction(payload.AccountHolderUid, payload.Content.TransactionUid,
                topUpRequest.Amount.MinorUnits);

            http.Dispose();
            return req.CreateResponse(HttpStatusCode.OK);
        }

        private static HttpClient GetHttpClient()
        {
            var baseAddress =
                Environment.GetEnvironmentVariable("STARLING_BASE_URL", EnvironmentVariableTarget.Process);

            var accessToken =
                Environment.GetEnvironmentVariable("STARLING_ACCESS_TOKEN", EnvironmentVariableTarget.Process);

            var http = new HttpClient
            {
                BaseAddress = new Uri($"{baseAddress}"),
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", $"{accessToken}"),
                    Accept =
                    {
                        new MediaTypeWithQualityHeaderValue("application/json")
                    }
                }
            };

            return http;
        }
    }
}
