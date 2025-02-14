using OneToManyFlows.Api.Core;

namespace OneToManyFlows.Api.Flows;

public class SomeFlowRequestDto : HaveProvider
{
    public int IdsCount { get; init; }
}
