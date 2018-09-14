using System;
using System.Security.Cryptography;
using System.Text;

namespace RounderUpper.Function.Validators
{
    internal static class WebhookPayloadValidator
    {
        public static (string computed, bool valid) Validate(string signature, string secret, string json)
        {
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