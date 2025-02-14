namespace OneToManyFlows.Api.Core;

public class ErrorMessages
{
    public static string ProviderNotFound(Provider provider) => $"Provider {provider} not found!";

    public static string OperationNotFound(string operation, Provider provider) => $"Operation {operation} not found for provider {provider}!";
}