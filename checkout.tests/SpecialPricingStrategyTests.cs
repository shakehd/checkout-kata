using checkout.Pricing;

namespace checkout.tests;

[TestFixture]
public class SpecialPricingStrategyTests
{
    [Test]
    public void Given_A_List_Of_SKUs_Should_Apply_Special_Price_When_SKU_Count_Match_Bundle_Size(
        [Range(1, 10, 2)] int bundleSize,
        [Range(20, 100, 20)] int specialPrice)
    {
        IPricingStrategy sut = new SpecialPricing(bundleSize, specialPrice, 0);
        
        int actualTotalPrice =
            sut.CalculateTotalPrice(bundleSize);
        
        Assert.That(actualTotalPrice, Is.EqualTo(specialPrice));
    }
    
    [Test]
    public void Given_A_List_Of_SKUs_Should_Apply_Special_Pricing_For_Each_Bundle_Of_SKUs(
        [Range(1, 10, 2)] int bundleSize,
        [Range(20, 100, 20)] int specialPrice,
        [Random(0, 10, 5)] int multiFactor)
    {
        IPricingStrategy sut = new SpecialPricing(bundleSize, specialPrice, 0);
        
        int actualTotalPrice =
            sut.CalculateTotalPrice(bundleSize * multiFactor);
        
        Assert.That(actualTotalPrice,
            Is.AtLeast(multiFactor * specialPrice));
    }
}