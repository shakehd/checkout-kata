using checkout.Pricing;

namespace checkout.tests;

[TestFixture]
public class UnitPricingStrategyTests
{
    [Test, Sequential]
    public void Given_A_List_Of_SKus_Should_Return_Sum_Of_Unit_Prices(
        [Range(0, 100, 10)] int itemCount,
        [Range(0, 500, 50)] int unitPrice)
    {
        IPricingStrategy sut = new UnitPricing(unitPrice);
        
        int actualTotalPrice =
            sut.CalculateTotalPrice(itemCount);  
        
        Assert.That(actualTotalPrice, Is.EqualTo(itemCount * unitPrice));
    }

    [Test]
    public void Given_A_Negative_Price_Unit_Pricing_Creation_Should_Fail(
        [Range(-100, -1, 10)] int unitPrice)
    {
        Assert.That(() => new UnitPricing(unitPrice),
                    Throws.ArgumentException.With.Message.EqualTo("Price cannot be negative. (Parameter 'unitPrice')"));
    }
    
    [Test]
    public void Given_A_Negative_Item_Count_Unit_Pricing_Calculation_Should_Fail(
        [Range(-100, -1, 10)] int itemCount)
    {
        IPricingStrategy sut = new UnitPricing(0);
        Assert.That(() => sut.CalculateTotalPrice(itemCount),
            Throws.ArgumentException.With.Message.EqualTo("Item count cannot be negative. (Parameter 'itemNum')"));
    }
    
}