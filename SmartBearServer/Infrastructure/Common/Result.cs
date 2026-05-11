using System;

namespace SmartBearServer.Infrastructure.Common
{
    /// <summary>
    /// Represents a standardized error in the system.
    /// </summary>
    public class Error
    {
        public string Code { get; }
        public string Description { get; }

        public Error(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public static readonly Error None = new Error(string.Empty, string.Empty);
        public static readonly Error NullValue = new Error("Error.NullValue", "The specified result value is null.");
    }

    /// <summary>
    /// Represents the result of an operation, containing either a value or an error.
    /// </summary>
    /// <typeparam name="TValue">The type of the result value.</typeparam>
    public class Result<TValue>
    {
        public TValue? Value { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        protected Result(TValue? value, bool isSuccess, Error error)
        {
            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result<TValue> Success(TValue value) => new Result<TValue>(value, true, Error.None);
        public static Result<TValue> Failure(Error error) => new Result<TValue>(default, false, error);

        public static implicit operator Result<TValue>(TValue value) => Success(value);
    }

    /// <summary>
    /// Represents the result of an operation without a return value.
    /// </summary>
    public class Result
    {
        public object? Value => null; // To match the ApiResponse<T> interface on frontend
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, Error.None);
        public static Result Failure(Error error) => new Result(false, error);
    }
}
