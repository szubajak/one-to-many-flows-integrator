namespace OneToManyFlows.Api.Flows;

public class GoogleOtherFlowRequestHandler(ILogger<GoogleOtherFlowRequestHandler> logger) : IOtherFlowHandler
{
    public async Task<OtherFlowResponseDto> Handle(OtherFlowRequestDto request, CancellationToken cancellationToken)
    {
        logger.LogInformation("TEST");
        return null;
       // return await Task.FromResult(new SomeFlowResponseDto($"I'm response from {nameof(GoogleSomeFlowRequestHandler)}, provider {request.Provider.ToString()}"));
    }
        
}
