using OneToManyFlows.Api.Core;

namespace OneToManyFlows.Api.Flows;

public class AwsSomeFlowRequestHandler(ILogger<AwsSomeFlowRequestHandler> logger) : ISomeFlowHandler
{
    public async Task<SomeFlowResponseDto?> Handle(SomeFlowRequestDto request, CancellationToken cancellationToken)
    {
        if (request.Provider != Provider.Aws)
        {
            return await Task.FromResult((SomeFlowResponseDto?)null);
        }

        logger.LogInformation("Ids to generate requested count {IdsCount}", request.IdsCount);

        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var random = new Random();

        var response = new SomeFlowResponseDto()
        {
            ProviderName = request.Provider.ToString(),
            RandomIds = Enumerable.Range(1, request.IdsCount).Select(x =>
            {
                return Enumerable.Range(0, request.IdsCount.ToString().Length).Aggregate("", (acc, _) =>
                {
                    return acc += $"{chars[random.Next(chars.Length)]}";
                });
            }).ToList()
        };

        return await Task.FromResult(response);
    }
}
