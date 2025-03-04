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
    public async Task Handle_Success(Provider provider)
    {
        // Arrange
        var ct = new CancellationTokenSource().Token;
        var idsCount = 100;

        var request = new SomeFlowRequestDto
        {
            IdsCount = idsCount,
            Provider = provider
        };

        // Act
        var response = await _sut.Handle(request, ct);

        // Assert
        if (provider != Provider.Microsoft)
        {
            response.Should().BeNull();
            return;
        }

        response.Should().NotBeNull();
        response.RandomIds.Should().HaveCount(idsCount);
        response.ProviderName.Should().Be(provider.ToString());
    }
}
