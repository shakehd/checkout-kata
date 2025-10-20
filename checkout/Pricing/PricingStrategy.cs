using checkout.validation;

namespace checkout.Pricing;

public class UnitPricing (NonNegativeNumber unitPrice) : IPricingStrategy
{
    public int CalculateTotalPrice(NonNegativeNumber itemCount) => unitPrice * itemCount;

    public static UnitPricing Create(int unitPrice) =>
        new UnitPricing(new NonNegativeNumber(unitPrice,
            "Price cannot be negative. (Parameter 'unitPrice')"));
}

public class SpecialPricing(PositiveNumber bundleSize, NonNegativeNumber specialPrice, NonNegativeNumber unitPrice) : IPricingStrategy
{
    public int CalculateTotalPrice(NonNegativeNumber itemCount) =>
        (itemCount / bundleSize) * specialPrice + itemCount % bundleSize * unitPrice;

    public static SpecialPricing Create(int bundleSize, int specialPrice, int unitPrice) =>
        new SpecialPricing(
            new PositiveNumber(bundleSize, 
                "Bundle size must be positive. (Parameter 'bundleSize')"), 
            new NonNegativeNumber(specialPrice,
                "Special price cannot be negative. (Parameter 'specialPrice')"),
            new NonNegativeNumber(unitPrice,
                "Unit price cannot be negative. (Parameter 'unitPrice')")
        );
}


