namespace checkout.Pricing;

public class UnitPricing(int unitPrice) : IPricingStrategy
{
    public int UnitPrice { get; } = unitPrice < 0
        ? throw new ArgumentException("Price cannot be negative.", nameof(unitPrice))
        : unitPrice;

    public int CalculateTotalPrice(int itemNum) => UnitPrice * itemNum;
}
