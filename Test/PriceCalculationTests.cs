namespace Test
{
  using System.Linq;
  using FakeItEasy;
  using Ploeh.AutoFixture.Xunit;
  using StrategyDecoratorExercise.Strategy;
  using Xunit;
  using Xunit.Extensions;

  public class PriceCalculationTests
  {
    private Amount total;

    public PriceCalculationTests()
    {
      total = new Amount(Currency.DKK, 100d);
    }

    [Fact]
    public void add_tax_to_total()
    {
      // arrange
      var tax = new Amount(Currency.DKK, 25d);
      var expected = new Price(total, tax);
      var fakeTaxStrategy = A.Fake<TaxCalculationStrategy>();
      A.CallTo(fakeTaxStrategy).WithReturnType<Amount>().Returns(tax);

      var sut = new PriceCalculator(fakeTaxStrategy);
      var order = CreateOrder(sut, 25d, isConsumer: true);

      // act
      var actual = order.CreateBill().Price;

      // assert
      Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0, true), InlineData(0.1, true), InlineData(1, true), InlineData(10000, true)]
    [InlineData(0, false), InlineData(0.1, false), InlineData(1, false), InlineData(10000, false)]
    [AutoData]
    public void danish_tax_is_25_percent(double itemPrice, bool isConsumer)
    {
      // arrange
      var expected = new Price(total, total * 0.25);
      var sut = new DanishTaxCalculationStrategy();
      var calculator = new PriceCalculator(sut);
      var order = CreateOrder(calculator, itemPrice, isConsumer);

      // act
      var actual = order.CreateBill().Price;

      // assert
      Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0), InlineData(0.1), InlineData(1), InlineData(10000)]
    [AutoData]
    public void marsian_consumer_tax_is_22_percent(double itemPrice, bool isConsumer)
    {
      Assert.True(false, "implement this test to match the name and make it pass");
    }

    [Theory]
    [InlineData(0), InlineData(0.1), InlineData(1), InlineData(10000)]
    [AutoData]
    public void marsian_b2b_tax_is_16_percent(double itemPrice, bool isConsumer)
    {
      Assert.True(false, "implement this test to match the name and make it pass");
    }

    private Order CreateOrder(PriceCalculator priceCalculator, double itemPrice, bool isConsumer)
    {
      return new Order(Enumerable.Repeat<OrderLine>(new OrderLine(new Sku(), 2, new Amount(Currency.DKK, itemPrice)), 2), 
                       priceCalculator,
                       new Customer { IsConsumer = isConsumer});
    }
  }
}
