namespace checkout;

public abstract record Result
{
    private Result() { }
    
    public sealed record Ok() : Result;
    
    public sealed record Error(string Message) : Result;
};