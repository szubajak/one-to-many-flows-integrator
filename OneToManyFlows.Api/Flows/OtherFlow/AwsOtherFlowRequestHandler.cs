using OneToManyFlows.Api.Core;

namespace OneToManyFlows.Api.Flows;

public class AwsOtherFlowRequestHandler(ILogger<AwsOtherFlowRequestHandler> logger) : IOtherFlowHandler
{
    public async Task<OtherFlowResponseDto?> Handle(OtherFlowRequestDto request, CancellationToken cancellationToken)
    {
        if (request.Provider != Provider.Aws)
        {
            return await Task.FromResult((OtherFlowResponseDto?)null);
        }

        logger.LogInformation("Length of string to generate {StringLength}", request.StringLength);

        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var random = new Random();

        var response = new OtherFlowResponseDto()
        {
            ProviderName = request.Provider.ToString(),
            RandomString = Enumerable.Range(0, request.StringLength).Aggregate("", (acc, _) =>
            {
                return acc += $"{chars[random.Next(chars.Length)]}";
            })
        };

        return await Task.FromResult(response);
    }
}
