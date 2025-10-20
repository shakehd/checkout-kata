namespace checkout.validation;

public record PositiveNumber(int Value, string ErrorMessage)
{
    public int Value { get; } =
        Value <= 0
            ? throw new ArgumentException(ErrorMessage)
            : Value;
    
    public static implicit operator int(PositiveNumber number) => number.Value;
    
    public static explicit operator PositiveNumber(int value) =>
        new (value, "Value must be positive number.");
}