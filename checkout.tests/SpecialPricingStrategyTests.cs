using checkout.Pricing;
using NUnit.Framework.Internal;

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
    
    [Test]
    public void Given_A_List_Of_SKUs_Should_Apply_Unit_Pricing_When_SKUs_Are_Less_Than_Bundle_Size(
        [Range(2, 10, 2)] int bundleSize,
        [Range(1, 10, 2)] int unitPrice,
        [Range(20, 100, 20)] int specialPrice)
    {
        IPricingStrategy sut = new SpecialPricing(bundleSize, specialPrice, unitPrice);
        
        Randomizer randomizer = new Randomizer(42);
        int itemCount = bundleSize - randomizer.Next(1, bundleSize);
        
        int actualTotalPrice = sut.CalculateTotalPrice(itemCount);
        
        Assert.That(actualTotalPrice, Is.EqualTo(itemCount * unitPrice));
    }
    
    [Test]
    public void Given_A_List_Of_SKUs_Should_Apply_Special_Pricing_For_Each_Bundle_Of_SKU_And_Unit_Price_For_Remaining_SKUs(
        [Range(2, 10, 2)] int bundleSize,
        [Range(1, 10, 2)] int unitPrice,
        [Range(20, 100, 20)] int specialPrice)
    {
        IPricingStrategy sut = new SpecialPricing(bundleSize, specialPrice, unitPrice);
        
        var randomizer = new Randomizer(42);
        int itemCount = bundleSize + randomizer.Next(10);
        
        int actualTotalPrice = sut.CalculateTotalPrice(itemCount);
        
        int expectedTotalPrice = (itemCount / bundleSize) * specialPrice + itemCount % bundleSize * unitPrice;
        
        Assert.That(actualTotalPrice, Is.EqualTo(expectedTotalPrice));
    }
}