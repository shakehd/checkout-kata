using checkout.validation;

namespace checkout.Pricing;

/// <summary>
/// Represents a pricing strategy based on a unit price.
/// </summary>
/// <param name="unitPrice">The price of a single unit. Must be non-negative.</param>
public class UnitPricing (NonNegativeNumber unitPrice) : IPricingStrategy
{
    /// <summary>
    /// Calculates the total price for a given number of items.
    /// </summary>
    /// <param name="itemCount">The number of items.</param>
    /// <returns>The total price as the product of itemCount and unitPrice.</returns>
    public int CalculateTotalPrice(NonNegativeNumber itemCount) => unitPrice * itemCount;

    /// <summary>
    /// Creates a new instance of the <see cref="UnitPricing"/> class.
    /// </summary>
    /// <param name="unitPrice">The price of a single unit.</param>
    /// <returns>A new <see cref="UnitPricing"/> instance.</returns>
    public static UnitPricing Create(int unitPrice) =>
        new (new NonNegativeNumber(unitPrice,
            "Price cannot be negative. (Parameter 'unitPrice')"));
}

/// <summary>
/// Represents a special pricing strategy with a bundle price.
/// </summary>
/// <param name="bundleSize">The number of items in a bundle. Must be positive.</param>
/// <param name="specialPrice">The price of a bundle. Must be non-negative.</param>
/// <param name="unitPrice">The price of a single unit. Must be non-negative.</param>
public class SpecialPricing(PositiveNumber bundleSize, NonNegativeNumber specialPrice, NonNegativeNumber unitPrice) : IPricingStrategy
{
    /// <summary>
    /// Calculates the total price for a given number of items using the special pricing rules.
    /// </summary>
    /// <param name="itemCount">The number of items.</param>
    /// <returns>The total price as the sum of bundles prices (if any) and units prices.</returns>
    public int CalculateTotalPrice(NonNegativeNumber itemCount) =>
        (itemCount / bundleSize) * specialPrice + itemCount % bundleSize * unitPrice;

    /// <summary>
    /// Creates a new instance of the <see cref="SpecialPricing"/> class.
    /// </summary>
    /// <param name="bundleSize">The number of items in a bundle.</param>
    /// <param name="specialPrice">The price of a bundle.</param>
    /// <param name="unitPrice">The price of a single unit.</param>
    /// <returns>A new <see cref="SpecialPricing"/> instance.</returns>
    public static SpecialPricing Create(int bundleSize, int specialPrice, int unitPrice) =>
        new (
            new PositiveNumber(bundleSize, 
                "Bundle size must be positive. (Parameter 'bundleSize')"), 
            new NonNegativeNumber(specialPrice,
                "Special price cannot be negative. (Parameter 'specialPrice')"),
            new NonNegativeNumber(unitPrice,
                "Unit price cannot be negative. (Parameter 'unitPrice')")
        );
}


