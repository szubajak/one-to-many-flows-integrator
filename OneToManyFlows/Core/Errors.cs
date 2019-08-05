namespace OneToManyFlows.Core
{
    public class Errors
    {
        public const string UnexpectedError = "Unexpected error has occured";
        public const string ProviderError = "Request to provider service has failed";

        public static string ProviderNotFound(string providerId) => $"Provider not found for providerId: {providerId}";

        public static string OperationNotFound(string operation, string providerId) => $"Operation {operation} not found for providerId: {providerId}";
    }
}