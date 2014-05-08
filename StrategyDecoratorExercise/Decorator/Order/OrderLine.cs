namespace StrategyDecoratorExercise.Decorator.Order
{
  using Money;

  public class OrderLine
  {
    public OrderLine(Sku sku, int count, Amount itemPrice)
    {
      Sku = sku;
      ItemCount = count;
      ItemPrice = itemPrice;
    }

    public Sku Sku { get; private set; }
    public int ItemCount { get; private set; }
    public Amount ItemPrice { get; private set; }
  }
}