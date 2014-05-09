namespace StrategyDecoratorExercise.Decorator.Price
{
  using Money;
  using Order;

  public class BrickFridayDiscount : PriceCalculatorDecorator
  {
    public BrickFridayDiscount(TotalAmountCalculator composite) : base(composite)
    {
    }
    protected override Amount DecorateAmount(Order order, Amount baseAmount)
    {
      return baseAmount * 0.75m;
    }

  }
}
