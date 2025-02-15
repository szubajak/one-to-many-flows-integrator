using OneToManyFlows.Api.Core;

namespace OneToManyFlows.Api.Flows;

public class GoogleSomeFlowRequestHandler(ILogger<GoogleSomeFlowRequestHandler> logger) : ISomeFlowHandler
{
    public async Task<SomeFlowResponseDto?> Handle(SomeFlowRequestDto request, CancellationToken cancellationToken)
    {
        if (request.Provider != Provider.Google)
        {
            return await Task.FromResult((SomeFlowResponseDto?)null);
        }

        logger.LogInformation("Ids to generate requested count {IdsCount}", request.IdsCount);

        var response = new SomeFlowResponseDto()
        {
            ProviderName = request.Provider.ToString(),
            RandomIds = Enumerable.Range(1, request.IdsCount).Select(x => $"{x}".PadLeft(request.IdsCount.ToString().Length, '0')).ToList(),
        };

        return await Task.FromResult(response);
    }
        
}
