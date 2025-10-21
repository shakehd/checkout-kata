namespace checkout.validation;

/// <summary>
/// Represents a non-negative integer value.
/// </summary>
/// <param name="Value">The integer value.</param>
/// <param name="ErrorMessage">The error message to display if the value is negative.</param>
public record NonNegativeNumber(int Value, string ErrorMessage)
{
    /// <summary>
    /// Gets the non-negative integer value.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if the value is negative.</exception>
    public int Value { get;} =
        Value < 0
            ? throw new ArgumentException(ErrorMessage)
            : Value;
    
    /// <summary>
    /// Implicitly converts a <see cref="NonNegativeNumber"/> to an <see cref="int"/>.
    /// </summary>
    /// <param name="number">The <see cref="NonNegativeNumber"/> to convert.</param>
    public static implicit operator int(NonNegativeNumber number) => number.Value;
    
    /// <summary>
    /// Explicitly converts an <see cref="int"/> to a <see cref="NonNegativeNumber"/>.
    /// </summary>
    /// <param name="value">The <see cref="int"/> to convert.</param>
    public static explicit operator NonNegativeNumber(int value) =>
        new (value, "Value cannot be negative.");
}