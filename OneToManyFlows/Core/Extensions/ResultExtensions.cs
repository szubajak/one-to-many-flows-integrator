namespace OneToManyFlows.Core.Extensions
{
    using Base;

    public static class Result
    {
        public static Result<T> Success<T>(T value) => new Result<T>(value);

        public static Result<T> Fail<T>(Error error) => new Result<T>(error);
    }
}