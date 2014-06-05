namespace StrategyDecoratorExercise.Decorator.Price
{
  using Money;
  using Order;

  public class VolumeDiscount : PriceCalculatorDecorator
  {
    public VolumeDiscount(TotalAmountCalculator composite) : base(composite)
    {
    }

    protected override Amount DecorateAmount(Order order, Amount baseAmount)
    {
      if (VolumeDiscountApplies(order))
        return baseAmount * 0.9m;
      return baseAmount;
    }

    private bool VolumeDiscountApplies(Order order)
    {
      return true;
    }
  }
}