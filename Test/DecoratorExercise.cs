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
      Assert.True(false, " Exercise: implement this test to match the name and make it pass");
    }

    [Fact]
    public void blackfriday_and_volumet_discounts_are_both_applied_with_enough_items()
    {
      Assert.True(false, " Exercise: implement this test to match the name and make it pass");
    }

    [Fact]
    public void blackfriday_but_not_volume_discount_is_applied_with_too_few_items()
    {
      Assert.True(false, " Exercise: implement this test to match the name and make it pass");
    }


    private Order CreateOrder(PriceCalculator priceCalculator, decimal itemPrice, bool isConsumer, int numItems)
    {
      return new Order(Enumerable.Repeat<OrderLine>(new OrderLine(new Sku(), 1, new Amount(Currency.DKK, itemPrice)), numItems), 
                       priceCalculator,
                       new Customer { IsConsumer = isConsumer});
    }
  }
}
