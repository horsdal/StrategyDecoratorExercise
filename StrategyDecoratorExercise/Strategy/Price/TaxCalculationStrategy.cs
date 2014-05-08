namespace StrategyDecoratorExercise.Strategy
{
  public interface TaxCalculationStrategy
  {
    Amount B2CTax(Order order, Amount total);
    Amount B2BTax(Order order, Amount total);
  }
}
