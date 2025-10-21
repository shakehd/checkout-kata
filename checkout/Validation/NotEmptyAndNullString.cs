namespace checkout.validation;

public record NotEmptyAndNullString(string Value, string ErrorMessage)
{
    public string Value { get;} =
        string.IsNullOrEmpty(Value)
            ? throw new ArgumentException(ErrorMessage)
            : Value;
    
    public static implicit operator string(NotEmptyAndNullString value) => value.Value;
    
    public static explicit operator NotEmptyAndNullString(string value) =>
        new (value, "Value cannot be null or empty.");
}