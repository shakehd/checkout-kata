using checkout.Pricing;
using checkout.validation;

namespace checkout;

/// <summary>
/// Represents a checkout system that calculates the total price of a collection of items based
/// on their pricing strategies.
/// </summary>
/// <param name="pricingStrategyProvider">The provider for pricing strategies.</param>
public class Checkout(IPricingStrategyProvider pricingStrategyProvider) : ICheckout
{
    private readonly ICollection<string> _skuCodes = [];
    private readonly Dictionary<string,IPricingStrategy> _pricingStrategies = new();

    /// <summary>
    /// Scans an item and adds it to the checkout.
    /// </summary>
    /// <param name="skuCode">The SKU code of the item.</param>
    /// <returns>A Result indicating success or failure.</returns>
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

    /// <summary>
    /// Calculates the total price of all scanned items.
    /// </summary>
    /// <returns>The total price.</returns>
    public int GetTotalPrice()
    {
        ILookup<string, string> skuCounts = _skuCodes.ToLookup(c => c);

        return skuCounts
            .Sum(gr =>
                _pricingStrategies[gr.Key]
                    .CalculateTotalPrice((NonNegativeNumber)gr.Count()));
    }

    /// <summary>
    /// Gets the SKU codes of all scanned items.
    /// </summary>
    /// <returns>A collection of SKU codes.</returns>
    public IEnumerable<string> GetSkuCodes()
    {
        return _skuCodes;
    }
}