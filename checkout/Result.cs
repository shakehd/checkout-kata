namespace checkout;

/// <summary>
/// Represents the result of an operation, which can be either a success (Ok) or a failure (Error).
/// </summary>
public abstract record Result
{
    /// <summary>
    /// Prevents direct instantiation of the abstract Result class.
    /// </summary>
    private Result() { }
    
    /// <summary>
    /// Represents a successful result.
    /// </summary>
    public sealed record Ok() : Result;
    
    /// <summary>
    /// Represents a failed result with an error message.
    /// </summary>
    /// <param name="Message">The error message.</param>
    public sealed record Error(string Message) : Result;
};