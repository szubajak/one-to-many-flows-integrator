namespace OneToManyFlows.Core.Base
{
    using System;

    public class Error
    {
        internal Error(string message, Exception exception)
        {
            Message = message;
            Exception = exception;
        }

        public string Message { get; }

        public Exception Exception { get; }
    }
}