namespace StrategyDecoratorExercise.Strategy
{
  using System.Collections.Generic;
  using System.Collections.Immutable;
  using System.Runtime.Remoting;

  public class Order
  {
    public IImmutableList<OrderLine> OrderLines { get; private set; }
    public bool IsB2C { get { return customer.IsConsumer; } }
    public Currency Currency { get { return customer.Currency; } }

    private readonly PriceCalculator priceCalculator;
    private readonly Customer customer;

    public Order(IEnumerable<OrderLine> orderLines, PriceCalculator priceCalculator, Customer customer)
    {
      this.OrderLines = ImmutableList<OrderLine>.Empty.AddRange(orderLines);
      this.priceCalculator = priceCalculator;
      this.customer = customer;
    }

    public Bill CreateBill()
    {
      var finalPrice = priceCalculator.CalculateTotal(this);
      var bill = CreateBill(finalPrice);
      return bill;
    }

    private Bill CreateBill(Price finalPrice)
    {
      return new Bill { Price = finalPrice };
    }
  }
}