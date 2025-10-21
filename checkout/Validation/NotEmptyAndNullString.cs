namespace checkout.validation;

/// <summary>
/// Represents a string that is not null or empty.
/// </summary>
/// <param name="Value">The string value.</param>
/// <param name="ErrorMessage">The error message to display if the value is null or empty.</param>
public record NotEmptyAndNullString(string Value, string ErrorMessage)
{
    /// <summary>
    /// Gets the string value.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if the value is null or empty.</exception>
    public string Value { get;} =
        string.IsNullOrEmpty(Value)
            ? throw new ArgumentException(ErrorMessage)
            : Value;
    
    /// <summary>
    /// Implicitly converts a <see cref="NotEmptyAndNullString"/> to a <see cref="string"/>.
    /// </summary>
    /// <param name="value">The <see cref="NotEmptyAndNullString"/> to convert.</param>
    public static implicit operator string(NotEmptyAndNullString value) => value.Value;
    
    /// <summary>
    /// Explicitly converts a <see cref="string"/> to a <see cref="NotEmptyAndNullString"/>.
    /// </summary>
    /// <param name="value">The <see cref="string"/> to convert.</param>
    public static explicit operator NotEmptyAndNullString(string value) =>
        new (value, "Value cannot be null or empty.");
}