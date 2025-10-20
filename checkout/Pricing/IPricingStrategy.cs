using checkout.validation;

namespace checkout.Pricing;

public interface IPricingStrategy
{
    int CalculateTotalPrice(NonNegativeNumber itemCount);
}