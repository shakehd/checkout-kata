namespace checkout.Pricing;

public interface IPricingStrategy
{
    int CalculateTotalPrice(int itemNum);
}