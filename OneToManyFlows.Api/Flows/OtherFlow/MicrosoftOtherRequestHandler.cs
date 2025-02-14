using OneToManyFlows.Api.Core;

namespace OneToManyFlows.Api.Flows;

public class MicrosoftOtherRequestHandler : IOtherFlowHandler
{
    public async Task<OtherFlowResponseDto?> Handle(OtherFlowRequestDto request, CancellationToken cancellationToken)
    {
        if (request.Provider != Provider.Microsoft)
        {
            return await Task.FromResult((OtherFlowResponseDto?)null);
        }

        var response = new OtherFlowResponseDto()
        {
            ProviderName = request.Provider.ToString(),
            RandomIds = Enumerable.Range(0, request.Count).Select(x => Guid.NewGuid()).ToList(),
            
        };

        return await Task.FromResult(response);
    }
       
}
