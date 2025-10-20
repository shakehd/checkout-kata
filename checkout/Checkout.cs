namespace checkout;

public class Checkout : ICheckout
{
    private readonly ICollection<string> _skuCodes = [];
    public void Scan(string skuCode)
    {
        _skuCodes.Add(skuCode);
    }

    public int GetTotalPrice()
    {
        return 0;
    }

    public IEnumerable<string> GetSkuCodes()
    {
        return _skuCodes;
    }
}