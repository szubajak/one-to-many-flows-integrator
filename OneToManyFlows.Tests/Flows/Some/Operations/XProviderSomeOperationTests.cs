namespace OneToManyFlows.Tests.Flows.Some.Operations
{
    using System.Threading.Tasks;
    using Core.Clients;
    using Core.Enums;
    using Core.Extensions;
    using NSubstitute;
    using NUnit.Framework;
    using OneToManyFlows.Flows.Some.Models;
    using OneToManyFlows.Flows.Some.Operations;
    using Shouldly;

    public class XProviderSomeOperationTests
    {
        private XProviderSomeOperation _someOperation;

        private IXProviderServiceClient _mockServiceClient;

        [SetUp]
        public void Setup()
        {
            _mockServiceClient = Substitute.For<IXProviderServiceClient>();
            _someOperation = new XProviderSomeOperation(_mockServiceClient);
        }

        [Test]
        public async Task Execute_GivenProperInput_CallsService()
        {
            // Arrange
            const Provider provider = Provider.XProvider;
            var inputDto = new SomeInputDto { ProviderId = (int)provider, TestProperty = "Test" };
            var path = XProviderSomeOperation.UrlPath.Replace("{some-parameter}", inputDto.TestProperty);

            _mockServiceClient.GetAsync<XProviderSomeOutputDto>(path).Returns(Result.Success(new XProviderSomeOutputDto()));

            // Act
            var result = await _someOperation.Execute(inputDto);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            await _mockServiceClient.Received(1).GetAsync<XProviderSomeOutputDto>(path);
        }
    }
}
