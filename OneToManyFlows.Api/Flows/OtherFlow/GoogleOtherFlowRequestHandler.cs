using OneToManyFlows.Api.Core;

namespace OneToManyFlows.Api.Flows;

public class GoogleOtherFlowRequestHandler(ILogger<GoogleOtherFlowRequestHandler> logger) : IOtherFlowHandler
{
    public async Task<OtherFlowResponseDto?> Handle(OtherFlowRequestDto request, CancellationToken cancellationToken)
    {
        if (request.Provider != Provider.Google)
        {
            return await Task.FromResult((OtherFlowResponseDto?)null);
        }

        logger.LogInformation("Length of string to generate {StringLength}", request.StringLength);

        var chars = "1234567890";
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
