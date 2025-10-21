using checkout.validation;

namespace checkout;

public interface ICheckout
{
    Result Scan(NotEmptyAndNullString skuCode);
    
    int GetTotalPrice();
    
    IEnumerable<string> GetSkuCodes();
}