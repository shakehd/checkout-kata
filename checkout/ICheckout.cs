using checkout.validation;

namespace checkout;

/// <summary>
/// Defines the contract for a checkout system.
/// </summary>
public interface ICheckout
{
    /// <summary>
    /// Scans an item and adds it to the checkout.
    /// </summary>
    /// <param name="skuCode">The SKU code of the item.</param>
    /// <returns>A Result indicating success or failure.</returns>
    Result Scan(NotEmptyAndNullString skuCode);
    
    /// <summary>
    /// Calculates the total price of all scanned items.
    /// </summary>
    /// <returns>The total price.</returns>
    int GetTotalPrice();
    
    /// <summary>
    /// Gets the SKU codes of all scanned items.
    /// </summary>
    /// <returns>A collection of SKU codes.</returns>
    IEnumerable<string> GetSkuCodes();
}