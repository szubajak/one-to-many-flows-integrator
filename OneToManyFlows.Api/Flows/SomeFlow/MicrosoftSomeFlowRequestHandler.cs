using OneToManyFlows.Api.Core;

namespace OneToManyFlows.Api.Flows;

public class MicrosoftSomeFlowRequestHandler(ILogger<MicrosoftSomeFlowRequestHandler> logger) : ISomeFlowHandler
{
    public async Task<SomeFlowResponseDto?> Handle(SomeFlowRequestDto request, CancellationToken cancellationToken)
    {
        if (request.Provider != Provider.Microsoft)
        {
            return await Task.FromResult((SomeFlowResponseDto?)null);
        }

        logger.LogInformation("Ids to generate requested count {IdsCount}", request.IdsCount);

        var response = new SomeFlowResponseDto()
        {
            ProviderName = request.Provider.ToString(),
            RandomIds = Enumerable.Range(0, request.IdsCount).Select(x => $"{Guid.NewGuid()}").ToList(),
        };

        return await Task.FromResult(response);
    }
}
