namespace OneToManyFlows.Core.Attributes
{
    using System;
    using Enums;

    public class ObjectLifetimeAttribute : Attribute
    {
        public ObjectLifetimeAttribute(ObjectLifetime lifetime)
        {
            Lifetime = lifetime;
        }

        public ObjectLifetime Lifetime { get; }
    }
}