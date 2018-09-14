using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using RounderUpper.Function.Validators;

namespace RounderUpper.Function
{
    public static class ProcessTransaction
    {
        [FunctionName("process-transaction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")]string data, HttpRequestMessage req, TraceWriter log)
        {
            req.Headers.TryGetValues("X-Hook-Signature", out var headers);
            var sign = headers?.FirstOrDefault();

            var secret = Environment.GetEnvironmentVariable("STARLING_WEBHOOK_SECRET", EnvironmentVariableTarget.Process);

            var (computed, valid) = WebhookPayloadValidator.Validate(sign, secret, data);

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
