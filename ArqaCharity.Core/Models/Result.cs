namespace ArqaCharity.Core.Models
{
    public class Result
    {
        public bool IsSuccess { get; }
        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error.Code != string.Empty)
                throw new InvalidOperationException("Cannot create success result with an error");
            if (!isSuccess && error.Code == string.Empty)
                throw new InvalidOperationException("Cannot create failure result without an error");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, Error.None);
        public static Result<T> Success<T>(T value) => new Result<T>(value, Error.None);
        public static Result Failure(Error error) => new Result(false, error);
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        protected internal Result(T value, Error error)
            : base(error.Code == string.Empty, error)
        {
            Value = value;
        }

        public static new Result<T> Success(T value) => new Result<T>(value, Error.None);
        public static new Result<T> Failure(Error error) => new Result<T>(default!, error);
    }
}
