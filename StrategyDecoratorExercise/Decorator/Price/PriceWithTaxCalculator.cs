namespace StrategyDecoratorExercise.Decorator.Price
{
  using Order;

  public class PriceWithTaxCalculator : PriceCalculator
  {
    private readonly TaxCalculationStrategy taxCalculator;
    public TotalAmountCalculator TotalAmountCalculator { get; private set; }

    public PriceWithTaxCalculator(TaxCalculationStrategy taxCalculator, TotalAmountCalculator totalAmountCalculator)
    {
      TotalAmountCalculator = totalAmountCalculator;
      this.taxCalculator = taxCalculator;
    }

    public Price CalculateTotal(Order order)
    {
      var total = TotalAmountCalculator.CalculateTotalAmount(order);
      var tax = order.IsB2C
        ? taxCalculator.B2CTax(order, total)
        : taxCalculator.B2BTax(order, total);
      return new Price(total, tax);
    }
  }
}