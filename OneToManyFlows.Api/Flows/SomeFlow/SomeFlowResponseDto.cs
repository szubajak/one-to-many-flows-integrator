namespace OneToManyFlows.Api.Flows;

public class SomeFlowResponseDto
{
    public required string ProviderName { get; init; }

    public required IList<string> RandomIds { get; init; }
}