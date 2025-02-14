namespace OneToManyFlows.Api.Core;

public interface IHandler<in TInput, TOutput>
{
    Task<TOutput> Handle(TInput input, CancellationToken cancellationToken);
}