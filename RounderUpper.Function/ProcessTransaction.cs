using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using RounderUpper.Function.Extensions;
using RounderUpper.Function.Utilities;
using RounderUpper.Function.Validators;

// ReSharper disable FieldCanBeMadeReadOnly.Local
namespace RounderUpper.Function
{
    public static class ProcessTransaction
    {
        private static HttpClient _httpClient = InitHttpClient();

        [FunctionName("process-transaction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")]string data, 
            [Table("transactions", "AzureWebJobsStorage")]CloudTable table, HttpRequestMessage req, TraceWriter log)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                log.Error("Webhook content empty.");
                return req.CreateResponse(HttpStatusCode.OK);
            }

//#if !DEBUG
            req.Headers.TryGetValues("X-Hook-Signature", out var headers);
            var sign = headers?.FirstOrDefault();

            var secret = EnvironmentExtensions.GetEnvString("STARLING_WEBHOOK_SECRET");
            var (_, valid) = WebhookPayloadValidator.Validate(sign, secret, data);

            if (!valid)
            {
                log.Error("Webhook signature mismatch. Rejected.");
                return req.CreateResponse(HttpStatusCode.OK);
            }

//#endif

            var payload = StarlingDeserialiser.WebhookPayload(data);

            var existing = await table.GetExistingTransaction(payload.AccountHolderUid, payload.Content.TransactionUid);
            if (existing != null)
            {
                log.Warning($"Transaction {existing.RowKey} already processed on {existing.Timestamp}.");
                return req.CreateResponse(HttpStatusCode.OK);
            }

            var goalId = EnvironmentExtensions.GetEnvString("STARLING_GOAL_ID");
            var remainder = (long)(Math.Abs(payload.Content.Amount) * 100 % 100);
            var difference = 100 - remainder;

            var threshold = EnvironmentExtensions.GetEnvInt("ROUND_UP_THRESHOLD");
            if (remainder >= threshold)
            {
                await _httpClient.SavingsGoalsAddMoney(goalId, difference);
            }

            await table.SaveTransaction(payload.AccountHolderUid, payload.Content.TransactionUid, difference);

            return req.CreateResponse(HttpStatusCode.OK);
        }

        private static HttpClient InitHttpClient()
        {
            var baseAddress = EnvironmentExtensions.GetEnvString("STARLING_BASE_URL");
            var accessToken = EnvironmentExtensions.GetEnvString("STARLING_ACCESS_TOKEN");

            Guard.AgainstNullOrWhitespaceArgument(nameof(baseAddress), baseAddress);
            Guard.AgainstNullOrWhitespaceArgument(nameof(accessToken), accessToken);

            return new HttpClient
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
        }
    }
}
