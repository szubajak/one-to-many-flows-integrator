namespace OneToManyFlows.Core.Extensions
{
    using System;
    using Enums;
    using Microsoft.AspNetCore.Http;

    public static class HttpContextExtensions
    {
        public const string ProviderKey = nameof(Provider);

        public static bool TrySetProvider(this HttpContext context, int? providerId)
        {
            if (!providerId.HasValue || !Enum.IsDefined(typeof(Provider), providerId))
            {
                return false;
            }

            context.Items[ProviderKey] = (Provider)providerId;
            return true;
        }

        public static Provider GetProvider(this HttpContext context) => (Provider)context.Items[ProviderKey];
    }
}