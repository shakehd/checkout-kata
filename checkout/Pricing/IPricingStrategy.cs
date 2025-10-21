using checkout.validation;

namespace checkout.Pricing;

/// <summary>
/// Defines a strategy for calculating the total price of a set of items.
/// </summary>
public interface IPricingStrategy
{
    /// <summary>
    /// Calculates the total price for a given number of items.
    /// </summary>
    /// <param name="itemCount">The number of items.</param>
    /// <returns>The total price.</returns>
    int CalculateTotalPrice(NonNegativeNumber itemCount);
}
