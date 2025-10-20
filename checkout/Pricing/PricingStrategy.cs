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

public class SpecialPricing(int bundleSize, int specialPrice, int UnitPrice) : IPricingStrategy
{
    public int CalculateTotalPrice(int itemCount)
    {
        return specialPrice;
    }
}
