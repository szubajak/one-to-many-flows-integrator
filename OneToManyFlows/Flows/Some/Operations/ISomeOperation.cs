namespace OneToManyFlows.Flows.Some.Operations
{
    using Core.Interfaces;
    using Models;

    public interface ISomeOperation : IOperation<SomeInputDto, SomeOutputDto>
    {
    }
}