namespace StrategyDecoratorExercise.Strategy
{
  using System.Linq;

  public class PriceCalculator
  {
    private readonly TaxCalculationStrategy taxCalculator;

    public PriceCalculator(TaxCalculationStrategy taxCalculator)
    {
      this.taxCalculator = taxCalculator;
    }

    public Price CalculateTotal(Order order)
    {
      var total = order.OrderLines.Aggregate(new Amount(order.Currency), (amount, line) => amount + line.ItemPrice * line.ItemCount);
      var tax = order.IsB2C ?
        taxCalculator.B2CTax(order, total) :
        taxCalculator.B2BTax(order, total);
      return new Price(total, tax);
    }
  }
}