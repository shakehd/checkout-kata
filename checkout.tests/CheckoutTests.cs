namespace checkout.tests;

[TestFixture]
public class CheckoutTests
{
    private ICheckout _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new Checkout(); 
    }

    [Test]
    public void Given_An_Empty_Checkout_The_Total_Price_Should_Be_Zero()
    {
        int actualTotalPrice = _sut.GetTotalPrice();
        
        Assert.That(actualTotalPrice, Is.Zero);
    }
    
    [Test]
    public void Given_A_Checkout_Scanning_Multiple_SKUs_Should_Not_Raise_Errors(
        [ValueSource(nameof(SKUCodes))] List<string> skuCodes)
    {
        Assert.That(() => skuCodes.ForEach(sku => _sut.Scan(sku)), Throws.Nothing);
    }
    
    [Test]
    public void Given_A_Checkout_It_Should_Remember_The_Scanned_Sku_Codes(
        [ValueSource(nameof(SKUCodes))] List<string> skuCodes)
    {
        skuCodes.ForEach(sku => _sut.Scan(sku));
        
        Assert.That(_sut.GetSkuCodes(), Is.EqualTo(skuCodes).AsCollection);
    }
    
    public static IEnumerable<List<string>> SKUCodes => new List<List<string>>
    {
        new() { "A", "B", "C", "D" },
        new() { "A", "A", "B", "D" },
        new() { "A", "A", "B", "B" }
    };
}