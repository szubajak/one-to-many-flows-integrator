namespace OneToManyFlows.Api.Flows;

public class OtherFlowResponseDto
{
    public string ProviderName { get; init; }

    public IList<Guid> RandomIds { get; init; }
}