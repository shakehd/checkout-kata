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
}