namespace StrategyDecoratorExercise.Decorator.Price
{
  using Money;
  using Order;

  public class LoyaltyClubDiscount : PriceCalculatorDecorator
  {
    public LoyaltyClubDiscount(TotalAmountCalculator composite) : base(composite)
    {
    }

    protected override Amount DecorateAmount(Order order, Amount baseAmount)
    {
      throw new System.NotImplementedException();
    }
  }
}