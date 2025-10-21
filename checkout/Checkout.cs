using checkout.Exception;
using checkout.Pricing;
using checkout.validation;

namespace checkout;

public class Checkout(IPricingStrategyProvider pricingStrategyProvider) : ICheckout
{
    private readonly ICollection<string> _skuCodes = [];
    private readonly Dictionary<string,IPricingStrategy> _pricingStrategies = new();

    public Result Scan(NotEmptyAndNullString skuCode)
    {
        if (_pricingStrategies.ContainsKey(skuCode))
        {
            _skuCodes.Add(skuCode);
            return new Result.Ok();
        }
        
        var pricingStrategy = pricingStrategyProvider.GetPricingStrategy(skuCode);
        if (pricingStrategy == null)
            return new Result.Error(
                $"Pricing strategy not found for sku code {skuCode}.");

        _pricingStrategies[skuCode] = pricingStrategy;
        _skuCodes.Add(skuCode);
        return new Result.Ok();
    }

    public int GetTotalPrice()
    {
        ILookup<string, string> skuCounts = _skuCodes.ToLookup(c => c);

        return skuCounts
            .Sum(gr =>
                _pricingStrategies[gr.Key]
                    .CalculateTotalPrice((NonNegativeNumber)gr.Count()));

    }

    public IEnumerable<string> GetSkuCodes()
    {
        return _skuCodes;
    }
}