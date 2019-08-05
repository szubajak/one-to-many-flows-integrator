namespace OneToManyFlows.Flows.Some.Operations
{
    using System.Threading.Tasks;
    using Core.Attributes;
    using Core.Base;
    using Core.Clients;
    using Core.Enums;
    using Core.Extensions;
    using Models;

    [Provider(Provider.XProvider)]
    public class XProviderSomeOperation : ISomeOperation
    {
        public const string UrlPath = "api/some-path/{some-parameter}";

        private readonly IXProviderServiceClient _serviceClient;

        public XProviderSomeOperation(IXProviderServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task<Result<SomeOutputDto>> Execute(SomeInputDto input)
        {
            var response = await _serviceClient.GetAsync<XProviderSomeOutputDto>(GetUrlPath(input));
            return response.IsError
                ? Result.Fail<SomeOutputDto>(response.Error)
                : Result.Success(Map(response.Value));
        }

        private static SomeOutputDto Map(XProviderSomeOutputDto providerOutputDto) => new SomeOutputDto
        {
            TestProperty = providerOutputDto.TestProperty
        };

        private static string GetUrlPath(SomeInputDto inputDto) => UrlPath.Replace("{some-parameter}", inputDto.TestProperty);
    }
}