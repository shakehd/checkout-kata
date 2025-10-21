using checkout.Exception;
using checkout.Pricing;
using checkout.validation;

namespace checkout;

public class Checkout(IPricingStrategyProvider pricingStrategyProvider) : ICheckout
{
    private readonly ICollection<string> _skuCodes = [];

    public void Scan(NotEmptyAndNullString skuCode)
    {
        pricingStrategyProvider.GetPricingStrategy(skuCode);
        _skuCodes.Add(skuCode);
    }

    public int GetTotalPrice()
    {
        ILookup<string, string> skuCounts = _skuCodes.ToLookup(c => c);

        return skuCounts
            .Select(gr =>
                (ps: pricingStrategyProvider.GetPricingStrategy(gr.Key) ?? throw new PricingStrategyNotFound($"Pricing strategy not found for sku code {gr.Key}."),
                    count: gr.Count()))
            .Sum(item => item.ps.CalculateTotalPrice((NonNegativeNumber)item.count));
    }

    public IEnumerable<string> GetSkuCodes()
    {
        return _skuCodes;
    }
}