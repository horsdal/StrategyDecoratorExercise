namespace StrategyDecoratorExercise.Decorator.Price
{
  using Money;
  using Order;

  public abstract class PriceCalculatorDecorator : TotalAmountCalculator
  {
    private readonly TotalAmountCalculator composite;

    public PriceCalculatorDecorator(TotalAmountCalculator composite)
    {
      this.composite = composite;
    }

    public Amount CalculateTotalAmount(Order order)
    {
      var baseAmount = composite.CalculateTotalAmount(order);
      return DecorateAmount(order, baseAmount);
    }

    protected abstract Amount DecorateAmount(Order order, Amount baseAmount);
  }
}