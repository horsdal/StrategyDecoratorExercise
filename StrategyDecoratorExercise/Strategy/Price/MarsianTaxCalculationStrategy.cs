namespace StrategyDecoratorExercise.Strategy
{
  public class MarisanTaxCalculationStrategy : TaxCalculationStrategy
  {
    public Amount B2CTax(Order order, Amount total)
    {
      return total * 0.22m;
    }

    public Amount B2BTax(Order order, Amount total)
    {
      return total * 0.16m;
    }
  }
}