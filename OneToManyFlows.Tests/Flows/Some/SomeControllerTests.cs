namespace OneToManyFlows.Tests.Flows.Some
{
    using System.Threading.Tasks;
    using Autofac.Features.Indexed;
    using Core;
    using Core.Enums;
    using Core.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using NSubstitute;
    using NUnit.Framework;
    using OneToManyFlows.Flows.Some;
    using OneToManyFlows.Flows.Some.Models;
    using OneToManyFlows.Flows.Some.Operations;
    using Shouldly;

    public class SomeControllerTests
    {
        private SomeController _someController;

        private IIndex<Provider, ISomeOperation> _mockOperations;

        [SetUp]
        public void Setup()
        {
            _mockOperations = Substitute.For<IIndex<Provider, ISomeOperation>>();
            _someController = new SomeController(_mockOperations);
        }

        [Test]
        public async Task Execute_GivenInvalidProvider_ReturnsProviderNotFoundError()
        {
            // Arrange
            const int providerId = -100;
            var expectedError = Errors.ProviderNotFound(providerId.ToString());
            var inputDto = new SomeInputDto { ProviderId = providerId };

            // Act
            var result = (BadRequestObjectResult)await _someController.Execute(inputDto);

            // Assert
            result.Value.ShouldBeOfType<string>();
            result.Value.ToString().ShouldBe(expectedError);
        }

        [Test]
        public async Task Execute_GivenInvalidOperationForProvider_ReturnsOperationNotFoundError()
        {
            // Arrange
            const Provider provider = Provider.XProvider;
            var expectedError = Errors.OperationNotFound(nameof(ISomeOperation), provider.ToString());
            var inputDto = new SomeInputDto { ProviderId = (int)provider };

            _mockOperations.TryGetValue(provider, out Arg.Any<ISomeOperation>()).Returns(false);

            _someController.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            _someController.ControllerContext.HttpContext.Items.Add(HttpContextExtensions.ProviderKey, provider);

            // Act
            var result = (BadRequestObjectResult)await _someController.Execute(inputDto);

            // Assert
            result.Value.ShouldBeOfType<string>();
            result.Value.ToString().ShouldBe(expectedError);
        }

        [Test]
        public async Task Execute_GivenProperInput_ExecutesOperation()
        {
            // Arrange
            const Provider provider = Provider.XProvider;
            var inputDto = new SomeInputDto { ProviderId = (int)provider };
            var mockOperation = Substitute.For<ISomeOperation>();
            var outputDto = new SomeOutputDto() { TestProperty = "Test" };
            mockOperation.Execute(inputDto).Returns(Result.Success(outputDto));

            _mockOperations.TryGetValue(provider, out Arg.Any<ISomeOperation>()).Returns(x =>
            {
                x[1] = mockOperation;
                return true;
            });

            _someController.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            _someController.ControllerContext.HttpContext.Items.Add(HttpContextExtensions.ProviderKey, provider);

            // Act
            var result = (OkObjectResult)await _someController.Execute(inputDto);

            // Assert
            await mockOperation.Received(1).Execute(inputDto);
            result.Value.ShouldBe(outputDto);
        }
    }
}
