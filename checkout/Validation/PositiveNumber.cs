namespace checkout.validation;

/// <summary>
/// Represents a positive integer value.
/// </summary>
/// <param name="Value">The integer value.</param>
/// <param name="ErrorMessage">The error message to display if the value is not positive.</param>
public record PositiveNumber(int Value, string ErrorMessage)
{
    /// <summary>
    /// Gets the positive integer value.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if the value is not positive.</exception>
    public int Value { get; } =
        Value <= 0
            ? throw new ArgumentException(ErrorMessage)
            : Value;
    
    /// <summary>
    /// Implicitly converts a <see cref="PositiveNumber"/> to an <see cref="int"/>.
    /// </summary>
    /// <param name="number">The <see cref="PositiveNumber"/> to convert.</param>
    public static implicit operator int(PositiveNumber number) => number.Value;
    
    /// <summary>
    /// Explicitly converts an <see cref="int"/> to a <see cref="PositiveNumber"/>.
    /// </summary>
    /// <param name="value">The <see cref="int"/> to convert.</param>
    public static explicit operator PositiveNumber(int value) =>
        new (value, "Value must be positive number.");
}