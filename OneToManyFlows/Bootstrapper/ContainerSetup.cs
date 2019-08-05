namespace OneToManyFlows.Bootstrapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Core.Attributes;
    using Core.Enums;
    using Flows.Other.Operations;
    using Flows.Some.Operations;

    public interface IContainerSetup
    {
        void RegisterServices(ContainerBuilder builder);
    }

    public class ContainerSetup : IContainerSetup
    {
        public void RegisterServices(ContainerBuilder builder)
        {
            Type[] FindTypes(IEnumerable<Type> types, ObjectLifetime lifetime) => types.Where(t => lifetime.Equals(t.GetCustomAttribute<ObjectLifetimeAttribute>()?.Lifetime)).ToArray();

            var allTypes = Assembly.GetExecutingAssembly().GetExportedTypes();

            builder.RegisterTypes(FindTypes(allTypes, ObjectLifetime.SingleInstance)).AsImplementedInterfaces().SingleInstance();
            builder.RegisterTypes(FindTypes(allTypes, ObjectLifetime.InstancePerLifetimeScope)).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterTypes(FindTypes(allTypes, ObjectLifetime.InstancePerDependency)).AsImplementedInterfaces().InstancePerDependency();

            RegisterOperations(builder, allTypes);
        }

        private static void RegisterOperations(ContainerBuilder builder, Type[] allTypes)
        {
            void RegisterProviderOperations(Type operationInterface)
            {
                Type FindType(Provider provider, Type operation) => allTypes.SingleOrDefault(t => provider.Equals(t.GetCustomAttribute<ProviderAttribute>()?.Provider) && operation.IsAssignableFrom(t));

                foreach (Provider provider in Enum.GetValues(typeof(Provider)))
                {
                    var operationImplementation = FindType(provider, operationInterface);
                    if (operationImplementation != null)
                    {
                        builder.RegisterType(operationImplementation).Keyed(provider, operationInterface).AsImplementedInterfaces().InstancePerDependency();
                    }
                }
            }

            RegisterProviderOperations(typeof(ISomeOperation));
            RegisterProviderOperations(typeof(IOtherOperation));
        }
    }
}