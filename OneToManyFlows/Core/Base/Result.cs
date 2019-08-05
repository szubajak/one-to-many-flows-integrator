namespace OneToManyFlows.Core.Base
{
    public struct Result<T>
    {
        internal Result(T value)
        {
            Value = value;
            Error = default;
            IsSuccess = true;
        }

        internal Result(Error error)
        {
            Value = default;
            Error = error;
            IsSuccess = false;
        }

        public T Value { get; }

        public Error Error { get; }

        public bool IsSuccess { get; }

        public bool IsError => !IsSuccess;
    }
}