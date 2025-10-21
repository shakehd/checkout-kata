using checkout.Exception;
using checkout.Pricing;
using checkout.validation;
using NSubstitute;

namespace checkout.tests;

[TestFixture]
public class CheckoutTests
{
    private ICheckout _sut;
    private IPricingStrategyProvider _pricingStrategyProvider;

    [SetUp]
    public void Setup()
    {
        _pricingStrategyProvider = Substitute.For<IPricingStrategyProvider>();
        _sut = new Checkout(_pricingStrategyProvider);
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
        Assert.That(() => skuCodes.ForEach(sku => _sut.Scan((NotEmptyAndNullString)sku)), Throws.Nothing);
    }
    
    [Test]
    public void Given_A_Checkout_It_Should_Remember_The_Scanned_Sku_Codes(
        [ValueSource(nameof(SKUCodes))] List<string> skuCodes)
    {
        skuCodes.ForEach(sku => _sut.Scan((NotEmptyAndNullString)sku));
        
        Assert.That(_sut.GetSkuCodes(), Is.EqualTo(skuCodes).AsCollection);
    }
    
    [Test, Sequential]
    public void Given_A_Non_Empty_Checkout_The_Total_Price_Should_Dependent_On_The_Items_Pricing_Strategy(
        [ValueSource(nameof(SKUCodes))] List<string> skuCodes,
        [Values(165, 160, 175)] int expectedTotalPrice
    )
    {
        _pricingStrategyProvider.GetPricingStrategy(Arg.Any<string>())
            .Returns(info => PricingStrategies[info.ArgAt<string>(0)]);
        
        skuCodes.ForEach(sku => _sut.Scan((NotEmptyAndNullString)sku));
    
        int actualTotalPrice = _sut.GetTotalPrice();
        
        var distinctSkuCodes = skuCodes.Distinct().ToList();
        _pricingStrategyProvider.Received().GetPricingStrategy(Arg.Is<string>(c => distinctSkuCodes.Contains(c)));
        
        Assert.That(actualTotalPrice, Is.EqualTo(expectedTotalPrice));
    }

    [Test]
    public void Given_A_Non_Empty_Checkout_It_Should_Fail_If_A_Pricing_Strategy_Is_Not_Found_Computing_Total_Price()
    {
        const string skuCode = "A";
        _pricingStrategyProvider.GetPricingStrategy(Arg.Any<string>())
            .Returns(_ => null!);

        _sut.Scan((NotEmptyAndNullString)skuCode);
        
        Assert.That(() => _sut.GetTotalPrice(), 
            Throws.InstanceOf<PricingStrategyNotFound>().With.Message.EqualTo($"Pricing strategy not found for sku code {skuCode}."));
    }

    private static Dictionary<string, IPricingStrategy> PricingStrategies => new()
    {
        { "A", SpecialPricing.Create(3, 130, 50) },
        { "B", SpecialPricing.Create(2, 45, 30) },
        { "C", UnitPricing.Create(20) },
        { "D", UnitPricing.Create(15) }
    };
    public static IEnumerable<List<string>> SKUCodes => new List<List<string>>
    {
        new() { "A", "A", "B", "C", "D" },
        new() { "A", "A", "B", "B", "D" },
        new() { "A", "A", "A", "B", "B" }
    };
}