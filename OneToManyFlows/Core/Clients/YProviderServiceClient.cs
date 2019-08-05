namespace OneToManyFlows.Core.Clients
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Attributes;
    using Base;
    using Enums;
    using Extensions;

    public interface IYProviderServiceClient : IServiceClient
    {
    }

    [ObjectLifetime(ObjectLifetime.InstancePerLifetimeScope)]
    public class YProviderServiceClient : IYProviderServiceClient
    {
        public const string BaseAddress = "https://www.youtube.com/";

        private readonly IHttpClientFactory _httpClientFactory;

        public YProviderServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<TResult>> GetAsync<TResult>(string path)
            where TResult : class
        {
            var client = GetClient();
            try
            {
                var response = await client.GetAsync(path);

                return Result.Success(await response.Content.ReadAsAsync<TResult>());
            }
            catch (Exception e)
            {
                return Result.Fail<TResult>(new Error(Errors.ProviderError, e));
            }
        }

        private HttpClient GetClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(BaseAddress);

            return client;
        }
    }
}