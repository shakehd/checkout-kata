namespace checkout.Pricing;

public class UnitPricing(int unitPrice) : IPricingStrategy
{
    public int CalculateTotalPrice(int itemNum) => unitPrice * itemNum;
}
