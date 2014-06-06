namespace Test
{
  using System.Linq;
  using FakeItEasy;
  using Ploeh.AutoFixture.Xunit;
  using StrategyDecoratorExercise.Strategy;
  using Xunit;
  using Xunit.Extensions;

  public class StrategyExercise
  {
    private Amount total;

    public StrategyExercise()
    {
      total = new Amount(Currency.DKK, 100);
    }

    [Fact]
    public void add_tax_to_total()
    {
      // arrange
      var tax = new Amount(Currency.DKK, 25);
      var expected = new Price(total, tax);
      var fakeTaxStrategy = A.Fake<TaxCalculationStrategy>();
      A.CallTo(fakeTaxStrategy).WithReturnType<Amount>().Returns(tax);

      var sut = new PriceCalculator(fakeTaxStrategy);
      var order = CreateOrder(sut, 25, isConsumer: true);

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
      var itemPriceAsDecimal = (decimal)itemPrice;
      var expected = new Price(new Amount(Currency.DKK, itemPriceAsDecimal * 4), new Amount(Currency.DKK, itemPriceAsDecimal));
      var sut = new DanishTaxCalculationStrategy();
      var calculator = new PriceCalculator(sut);
      var order = CreateOrder(calculator, itemPriceAsDecimal, isConsumer);

      // act
      var actual = order.CreateBill().Price;

      // assert
      Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0), InlineData(0.1), InlineData(1), InlineData(10000)]
    [AutoData]
    public void marsian_consumer_tax_is_22_percent(double itemPrice)
    {
      var itemPriceAsDecimal = (decimal)itemPrice;
      // arrange
      var expected = new Price(new Amount(Currency.MAR, itemPriceAsDecimal * 4), new Amount(Currency.MAR, itemPriceAsDecimal * 4) *0.22m);
      var sut = new MarisanTaxCalculationStrategy();
      var calculator = new PriceCalculator(sut);
      var order = CreateOrder(calculator, itemPriceAsDecimal, isConsumer: true, currency: Currency.MAR);

      // act
      var actual = order.CreateBill().Price;

      // assert
      Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0), InlineData(0.1), InlineData(1), InlineData(10000)]
    [AutoData]
    public void marsian_b2b_tax_is_16_percent(double itemPrice)
    {
      var itemPriceAsDecimal = (decimal)itemPrice;
      // arrange
      var expected = new Price(new Amount(Currency.MAR, itemPriceAsDecimal * 4), new Amount(Currency.MAR, itemPriceAsDecimal * 4) * 0.16m);
      var sut = new MarisanTaxCalculationStrategy();
      var calculator = new PriceCalculator(sut);
      var order = CreateOrder(calculator, itemPriceAsDecimal, isConsumer: false, currency: Currency.MAR);

      // act
      var actual = order.CreateBill().Price;

      // assert
      Assert.Equal(expected, actual);
    }

    private Order CreateOrder(PriceCalculator priceCalculator, decimal itemPrice, bool isConsumer, Currency currency = Currency.DKK)
    {
      return new Order(Enumerable.Repeat(new OrderLine(new Sku(), 2, new Amount(currency, itemPrice)), 2), 
                       priceCalculator,
                       new Customer { IsConsumer = isConsumer, Currency = currency});
    }
  }
}
