using checkout.validation;

namespace checkout.Pricing;

public class UnitPricing (NonNegativeNumber unitPrice) : IPricingStrategy
{
    public int CalculateTotalPrice(NonNegativeNumber itemCount) => unitPrice * itemCount;

    public static UnitPricing Create(int unitPrice) =>
        new UnitPricing(new NonNegativeNumber(unitPrice,
            "Price cannot be negative. (Parameter 'unitPrice')"));
}

public class SpecialPricing(int bundleSize, NonNegativeNumber specialPrice, NonNegativeNumber unitPrice) : IPricingStrategy
{
    public int BundleSize { get; } = bundleSize <= 0
        ? throw new ArgumentException("Bundle size must be positive.", nameof(bundleSize))
        : bundleSize;
    
    public int CalculateTotalPrice(NonNegativeNumber itemCount) =>
        (itemCount / BundleSize) * specialPrice + itemCount % BundleSize * unitPrice;

    public static SpecialPricing Create(int bundleSize, int specialPrice, int unitPrice) =>
        new SpecialPricing(bundleSize,
            new NonNegativeNumber(specialPrice,
                "Special price cannot be negative. (Parameter 'specialPrice')"),
            new NonNegativeNumber(unitPrice,
                "Unit price cannot be negative. (Parameter 'unitPrice')")
        );
}


