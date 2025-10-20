namespace checkout.Pricing;

public class UnitPricing(int unitPrice) : IPricingStrategy
{
    public int UnitPrice { get; } = unitPrice < 0
        ? throw new ArgumentException("Price cannot be negative.", nameof(unitPrice))
        : unitPrice;

    public int CalculateTotalPrice(int itemCount) =>
        itemCount < 0 ? throw new ArgumentException("Item count cannot be negative.", nameof(itemCount))
                    : UnitPrice * itemCount;
}

public class SpecialPricing(int bundleSize, int specialPrice, int unitPrice) : IPricingStrategy
{
    public int BundleSize { get; } = bundleSize <= 0
        ? throw new ArgumentException("Bundle size must be positive.", nameof(bundleSize))
        : bundleSize;
    
    public int SpecialPrice { get; } = specialPrice < 0
        ? throw new ArgumentException("Special price cannot be negative.", nameof(specialPrice))
        : specialPrice;
    
    public int UnitPrice { get; } = unitPrice < 0
        ? throw new ArgumentException("Unit price cannot be negative.", nameof(unitPrice))
        : unitPrice;
    
    public int CalculateTotalPrice(int itemCount) =>
        (itemCount / BundleSize) * SpecialPrice + itemCount % bundleSize * UnitPrice;
}
