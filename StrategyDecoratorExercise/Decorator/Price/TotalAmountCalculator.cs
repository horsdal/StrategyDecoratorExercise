namespace StrategyDecoratorExercise.Decorator.Price
{
  using Money;
  using Order;

  public interface TotalAmountCalculator
  {
    Amount CalculateTotalAmount(Order order);
  }
}
