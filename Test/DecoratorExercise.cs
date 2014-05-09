namespace Test
{
  using System.Linq;
  using Ploeh.AutoFixture.Xunit;
  using StrategyDecoratorExercise.Decorator;
  using StrategyDecoratorExercise.Decorator.Money;
  using StrategyDecoratorExercise.Decorator.Order;
  using StrategyDecoratorExercise.Decorator.Price;
  using Xunit;
  using Xunit.Extensions;

  public class DecoratorExercise
  {
    [Theory]
    [InlineData(1), InlineData(10000)]
    [AutoData]
    public void volume_discount_is_10_percent(int itemPrice)
    {
      // arrange
      var expected = new Price(new Amount(Currency.DKK, itemPrice * 40) * 0.9m, new Amount(Currency.DKK, itemPrice * 10) * 0.9m);
      var taxCalculator = new DanishTaxCalculationStrategy();
      var sut = new PriceWithTaxCalculator(taxCalculator, new VolumeDiscount(new SimpleTotalAmountCalculator()));
      var order = CreateOrder(sut, itemPrice, isConsumer: true, numItems: 40);

      // act
      var actual = order.CreateBill().Price;

      // assert
      Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0), InlineData(1), InlineData(9)]
    public void volume_discount_is_not_applied_to_less_than_10_items_percent(int numItems)
    {
      // arrange
      var expected = new Price(new Amount(Currency.DKK, 25 * numItems), new Amount(Currency.DKK, 25 * numItems) * 0.25m);
      var taxCalculator = new DanishTaxCalculationStrategy();
      var sut = new PriceWithTaxCalculator(taxCalculator, new VolumeDiscount(new SimpleTotalAmountCalculator()));
      var order = CreateOrder(sut, 25, isConsumer: true, numItems: numItems);

      // act
      var actual = order.CreateBill().Price;

      // assert
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void blackfriday_discount_is_25_percent()
    {
      // arrange
      var expected = new Price(new Amount(Currency.DKK, 25 * 4 * 0.75m), new Amount(Currency.DKK, 25 * 4 * 0.75m) * 0.25m);
      var taxCalculator = new DanishTaxCalculationStrategy();
      var sut = new PriceWithTaxCalculator(taxCalculator, new BrickFridayDiscount(new SimpleTotalAmountCalculator()));
      var order = CreateOrder(sut, 25, isConsumer: true, numItems: 4);

      // act
      var actual = order.CreateBill().Price;

      // assert
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void blackfriday_and_volumet_discounts_are_both_applied_with_enough_items()
    {
      // arrange
      var expected = new Price(new Amount(Currency.DKK, 25 * 40 * 0.75m * 0.9m), new Amount(Currency.DKK, 25 * 40 * 0.75m * 0.9m) * 0.25m);
      var taxCalculator = new DanishTaxCalculationStrategy();
      var sut = new PriceWithTaxCalculator(taxCalculator, new VolumeDiscount(new BrickFridayDiscount(new SimpleTotalAmountCalculator())));
      var order = CreateOrder(sut, 25, isConsumer: true, numItems: 40);

      // act
      var actual = order.CreateBill().Price;

      // assert
      Assert.Equal(expected, actual);
    }

    [Fact]
    public void blackfriday_but_not_volume_discount_is_applied_with_too_few_items()
    {
      // arrange
      var expected = new Price(new Amount(Currency.DKK, 25 * 4 * 0.75m), new Amount(Currency.DKK, 25 * 4 * 0.75m) * 0.25m);
      var taxCalculator = new DanishTaxCalculationStrategy();
      var sut = new PriceWithTaxCalculator(taxCalculator, new VolumeDiscount(new BrickFridayDiscount(new SimpleTotalAmountCalculator())));
      var order = CreateOrder(sut, 25, isConsumer: true, numItems: 4);

      // act
      var actual = order.CreateBill().Price;

      // assert
      Assert.Equal(expected, actual);
    }


    private Order CreateOrder(PriceCalculator priceCalculator, decimal itemPrice, bool isConsumer, int numItems, Currency currency = Currency.DKK)
    {
      return new Order(Enumerable.Repeat(new OrderLine(new Sku(), 1, new Amount(currency, itemPrice)), numItems), 
                       priceCalculator,
                       new Customer { IsConsumer = isConsumer, Currency = currency});
    }
  }
}
