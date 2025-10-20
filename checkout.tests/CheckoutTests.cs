namespace checkout.tests;

[TestFixture]
public class CheckoutTests
{
    private ICheckout _sut;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _sut = new Checkout();
    }

    [Test]
    public void Given_An_Empty_Checkout_The_Total_Price_Should_Be_Zero()
    {
        int actualTotalPrice = _sut.GetTotalPrice();
        
        Assert.That(actualTotalPrice, Is.Zero);
    }
}