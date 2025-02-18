using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OneToManyFlows.Api.Core;
using OneToManyFlows.Api.Flows;

namespace OneToManyFlows.Api.Tests.Flows;

public class GoogleOtherFlowRequestHandlerTests
{
    private readonly ILogger<GoogleOtherFlowRequestHandler> _logger = null!;

    private readonly GoogleOtherFlowRequestHandler _sut;

    public GoogleOtherFlowRequestHandlerTests()
    {
        _logger = Substitute.For<ILogger<GoogleOtherFlowRequestHandler>>();

        _sut = new GoogleOtherFlowRequestHandler(_logger);
    }

    [Theory]
    [InlineData(Provider.Microsoft)]
    [InlineData(Provider.Google)]
    [InlineData(Provider.Aws)]
    public async Task Handle_Success(Provider provider)
    {
        // Arrange
        var ct = new CancellationTokenSource().Token;
        var stringLength = 100;

        var request = new OtherFlowRequestDto
        {
            StringLength = stringLength,
            Provider = provider
        };

        // Act
        var response = await _sut.Handle(request, ct);

        // Assert
        if (provider != Provider.Google)
        {
            response.Should().BeNull();
            return;
        }

        response.Should().NotBeNull();
        response.RandomString.Should().NotBeNullOrEmpty();
        response.RandomString.Length.Should().Be(stringLength);
        response.ProviderName.Should().Be(provider.ToString());
    }
}
