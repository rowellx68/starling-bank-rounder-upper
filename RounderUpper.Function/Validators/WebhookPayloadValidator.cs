using System;
using System.Security.Cryptography;
using System.Text;
using RounderUpper.Function.Utilities;

namespace RounderUpper.Function.Validators
{
    public static class WebhookPayloadValidator
    {
        /// <summary>
        /// Validator for the webhook payload.
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="secret"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static (string computed, bool valid) Validate(string signature, string secret, string json)
        {
            Guard.AgainstNullOrWhitespaceArgument(nameof(signature), signature);
            Guard.AgainstNullOrWhitespaceArgument(nameof(secret), secret);
            Guard.AgainstNullOrWhitespaceArgument(nameof(json), json);

            string computed;
            using (var sha = new SHA512Managed())
            {
                var hashed = sha.ComputeHash(Encoding.UTF8.GetBytes($"{secret}{json}"));
                computed = Convert.ToBase64String(hashed);
            }

            return (computed: computed, valid: computed == signature);
        }
    }
}