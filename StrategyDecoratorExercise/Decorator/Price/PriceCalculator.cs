namespace StrategyDecoratorExercise.Decorator.Price
{
  using Order;

  public interface PriceCalculator
  {
    TotalAmountCalculator TotalAmountCalculator { get; }
    Price CalculateTotal(Order order);
  }
}