using checkout.validation;

namespace checkout;

public interface ICheckout
{
    void Scan(NotEmptyAndNullString skuCode);
    
    int GetTotalPrice();
    
    IEnumerable<string> GetSkuCodes();
}