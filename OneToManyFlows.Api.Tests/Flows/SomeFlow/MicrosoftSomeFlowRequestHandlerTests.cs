using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OneToManyFlows.Api.Core;
using OneToManyFlows.Api.Flows;

namespace OneToManyFlows.Api.Tests.Flows;

public class MicrosoftSomeFlowRequestHandlerTests
{
    private readonly ILogger<MicrosoftSomeFlowRequestHandler> _logger = null!;

    private readonly MicrosoftSomeFlowRequestHandler _sut;

    public MicrosoftSomeFlowRequestHandlerTests()
    {
        _logger = Substitute.For<ILogger<MicrosoftSomeFlowRequestHandler>>();

        _sut = new MicrosoftSomeFlowRequestHandler(_logger);
    }

    [Theory]
    [InlineData(Provider.Microsoft)]
    [InlineData(Provider.Google)]
    [InlineData(Provider.Aws)]
    public async Task Handle_WrongProvider_ShouldSkip(Provider provider)
    {
        // Arrange
        var ct = CancellationToken.None;
        var request = new SomeFlowRequestDto
        {
            Provider = provider
        };

        // Act
        var response = await _sut.Handle(request, ct);

        // Assert
        if (provider == Provider.Microsoft)
        {
            response.Should().NotBeNull();
        }
        else
        {
            response.Should().BeNull();
        }
    }

    [Fact]
    public async Task Handle_Success()
    {
        // Arrange
        var ct = CancellationToken.None;
        var idsCount = 100;
        var provider = Provider.Microsoft;

        var request = new SomeFlowRequestDto
        {
            IdsCount = idsCount,
            Provider = provider
        };

        // Act
        var response = await _sut.Handle(request, ct);

        // Assert
        response.Should().NotBeNull();
        response.RandomIds.Should().HaveCount(idsCount);
        response.ProviderName.Should().Be(provider.ToString());
    }
}
