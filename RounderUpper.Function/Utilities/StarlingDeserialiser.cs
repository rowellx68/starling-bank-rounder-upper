﻿using Newtonsoft.Json;
using StarlingBank.Contracts.Webhook;

namespace RounderUpper.Function.Utilities
{
    public static class StarlingDeserialiser
    {
        public static TransactionPayload WebhookPayload(string json)
        {
            Guard.AgainstNullOrWhitespaceArgument(nameof(json), json);

            return JsonConvert.DeserializeObject<TransactionPayload>(json);
        }

        public static T Payload<T>(string json)
        {
            Guard.AgainstNullOrWhitespaceArgument(nameof(json), json);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}