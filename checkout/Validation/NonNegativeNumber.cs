namespace checkout.validation;

public record NonNegativeNumber(int Value, string ErrorMessage)
{
    public int Value { get;} =
        Value < 0
            ? throw new ArgumentException(ErrorMessage)
            : Value;
    
    public static implicit operator int(NonNegativeNumber number) => number.Value;
    
    public static explicit operator NonNegativeNumber(int value) =>
        new (value, "Value cannot be negative.");
}