namespace OneToManyFlows.Core.Clients
{
    using System.Threading.Tasks;
    using Base;

    public interface IServiceClient
    {
        Task<Result<TResult>> GetAsync<TResult>(string path)
            where TResult : class;
    }
}