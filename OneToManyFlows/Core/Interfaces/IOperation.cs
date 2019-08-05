namespace OneToManyFlows.Core.Interfaces
{
    using System.Threading.Tasks;
    using Base;

    public interface IOperation<in TInput, TOutput>
    {
        Task<Result<TOutput>> Execute(TInput input);
    }
}