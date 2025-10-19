namespace checkout.tests;

[TestFixture]
public class UnitPricingStrategyTests
{
    [Test, Sequential]
    public void Given_A_List_Of_SKus_Should_Return_Sum_Of_Unit_Prices(
        [Range(0, 100, 10)] int numSkus,
        [Range(0, 500, 50)] int unitPrice)
    {
        IPricingStrategy sut = new UnitPricing(unitPrice);
        
        int actualTotalPrice =
            sut.CalculateTotalPrice(numSkus);  
        
        Assert.That(actualTotalPrice, Is.EqualTo(numSkus * unitPrice));
    }
}