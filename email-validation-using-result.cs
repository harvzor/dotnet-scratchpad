#:property TargetFramework=net10.0

Console.Write("Input email: ");
string email = Console.ReadLine() ?? string.Empty;

if (IsEmail(email) is Result<string>.Failure { } failure)
{
    Console.WriteLine($"Invalid - {failure.Reason}");
}
else
{
    Console.WriteLine("Valid");
}

Result<string> IsEmail(string email)
{
    if (string.IsNullOrWhiteSpace(email))
    {
        return Result.Failed("Email cannot be null or empty.");
    }
    
    string[] atSplit = email.Split('@');
    
    if (atSplit.Length != 2
        || string.IsNullOrWhiteSpace(atSplit[0])
        || string.IsNullOrWhiteSpace(atSplit[1]))
    {
        return Result.Failed("Email must contain exactly one '@' character with non-empty parts on both sides.");
    }
    
    string domainPart = atSplit[1];
    string [] domainDotSplit = domainPart.Split('.');
    if (domainDotSplit.Length < 2
        || string.IsNullOrWhiteSpace(domainDotSplit[0])
        || string.IsNullOrWhiteSpace(domainDotSplit[1]))
    {
        return Result.Failed("Domain part must contain at least one '.' character with non-empty parts on both sides.");
    }

    return new Result<string>.Success("");
}

abstract record Result<T> where T : notnull
{
    public sealed record Success(T Value) : Result<T>;

    public sealed record Failure(string Reason = "") : Result<T>;

    public static implicit operator Result<T>(T value) => new Success(value);

    public static implicit operator Result<T> (Result.Failure failure) => new Failure(failure.Reason);

    public static Success Succeeded(T value) => new(value);

    public static Failure Failed(string reason = "") => new(reason);

    public static Failure Failed<U>(Result<U>? result) where U : notnull =>
        result is Result<U>.Failure failure ? new(failure.Reason) : Failed();

    public static Failure Failed(Result? result) =>
        result is Result.Failure failure ? new(failure.Reason) : Failed();
}

abstract record Result
{
    private static readonly Success _succeededInstance = new();
    private static readonly Failure _failedInstance = new();

    public sealed record Success() : Result;

    public sealed record Failure(string Reason = "") : Result;

    public static Success Succeeded() => _succeededInstance;

    public static Failure Failed() => _failedInstance;

    public static Failure Failed(string reason) => new(reason);

    public static Failure Failed<T>(Result<T>? result) where T : notnull =>
        result is Result<T>.Failure failure ? new(failure.Reason) : Failed();
}
