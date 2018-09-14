using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StarlingBank.Contracts.Common;
using StarlingBank.Contracts.SavingsGoals;

namespace RounderUpper.Function.Extensions
{
    internal static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> SavingsGoalsAddMoney(this HttpClient http, string goalId, long minorUnits)
        {
            var req = new TopUpRequest
            {
                Amount = new CurrencyAndAmount
                {
                    Currency = "GBP",
                    MinorUnits = minorUnits
                }
            };

            return http.StarlingPutAsync($"v1/savings-goals/{goalId}/add-money/{Guid.NewGuid()}", req);
        }

        private static Task<HttpResponseMessage> StarlingPutAsync<T>(this HttpClient http, string uri, T value)
        {
            return http.PutAsync(uri, value, _formatter);
        }

        private static JsonMediaTypeFormatter _formatter = new JsonMediaTypeFormatter
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        };
    }
}