namespace OneToManyFlows.Flows.Other.Operations
{
    using System.Threading.Tasks;
    using Core.Attributes;
    using Core.Base;
    using Core.Clients;
    using Core.Enums;
    using Core.Extensions;
    using Models;

    [Provider(Provider.XProvider)]
    public class XProviderOtherOperation : IOtherOperation
    {
        public const string UrlPath = "api/some-path/{some-parameter}";

        private readonly IXProviderServiceClient _serviceClient;

        public XProviderOtherOperation(IXProviderServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task<Result<OtherOutputDto>> Execute(OtherInputDto input)
        {
            var response = await _serviceClient.GetAsync<XProviderOtherOutputDto>(GetUrlPath(input));
            return response.IsError
                ? Result.Fail<OtherOutputDto>(response.Error)
                : Result.Success(Map(response.Value));
        }

        private static OtherOutputDto Map(XProviderOtherOutputDto providerOutputDto) => new OtherOutputDto
        {
            TestProperty = providerOutputDto.TestProperty
        };

        private static string GetUrlPath(OtherInputDto inputDto) => UrlPath.Replace("{some-parameter}", inputDto.TestProperty);
    }
}