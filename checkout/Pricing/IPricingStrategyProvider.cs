namespace checkout.Pricing;

/// <summary>
/// Defines a provider for retrieving pricing strategies.
/// </summary>
public interface IPricingStrategyProvider
{
    /// <summary>
    /// Gets the pricing strategy for a given SKU code.
    /// </summary>
    /// <param name="code">The SKU code.</param>
    /// <returns>The pricing strategy, or null if not found.</returns>
    IPricingStrategy? GetPricingStrategy(string code);
}