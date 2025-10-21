namespace checkout.Pricing;

public interface IPricingStrategyProvider
{
    IPricingStrategy? GetPricingStrategy(string code);
}