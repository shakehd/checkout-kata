using checkout.validation;

namespace checkout;

public interface ICheckout
{
    void Scan(string skuCode);
    
    int GetTotalPrice();
    
    IEnumerable<string> GetSkuCodes();
}