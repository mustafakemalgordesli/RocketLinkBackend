using System.Text.Json.Serialization;

namespace RocketLink.Domain.Common;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }
    protected Result(bool isSuccess, string message)
    {
        if (!isSuccess)
        {
            throw new ArgumentException("Invalid error");
        }

        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Message { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Error? Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Success(string message) => new(true, message);
    public static Result Failure(Error error) => new(false, error);
}

public class Result<T> : Result
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T? Value { get; }

    private Result(T value) : base(true, Error.None)
    {
        Value = value;
    }
    private Result(bool isSuccess, Error error) : base(isSuccess, error)
    {
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(false, error);
}
