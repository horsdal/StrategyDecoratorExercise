namespace StrategyDecoratorExercise.Strategy
{
  using System;

  public class Price
  {
    public Amount BasePrice { get; private set; }
    public Amount Tax { get; private set; }

    public Price(Amount basePrice, Amount tax)
    {
      BasePrice = basePrice;
      Tax = tax;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj))
        return false;
      if (ReferenceEquals(this, obj))
        return true;
      if (obj.GetType() != this.GetType())
        return false;
      return Equals((Price) obj);
    }
    protected bool Equals(Price other)
    {
      return Equals(BasePrice, other.BasePrice) && Equals(Tax, other.Tax);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((BasePrice != null ? BasePrice.GetHashCode() : 0) * 397) ^ (Tax != null ? Tax.GetHashCode() : 0);
      }
    }

  }
}