namespace OneToManyFlows.Core.Attributes
{
    using System;
    using Enums;

    public class ProviderAttribute : Attribute
    {
        public ProviderAttribute(Provider provider)
        {
            Provider = provider;
        }

        public Provider Provider { get; }
    }
}