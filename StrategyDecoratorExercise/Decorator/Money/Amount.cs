namespace StrategyDecoratorExercise.Decorator.Money
{
  using System;

  public class Amount
  {
    private readonly decimal amount = 0;
    private readonly Currency currency;

    public Amount(Currency currency)
    {
      this.currency = currency;
    }

    public Amount(Currency currency, decimal amount)
    {
      this.currency = currency;
      this.amount = amount;
    }

    public static Amount operator +(Amount lhs, Amount rhs)
    {
      CheckCurrencies(lhs, rhs);
      return new Amount(lhs.currency, lhs.amount + rhs.amount);
    }

    public static Amount operator *(Amount lhs, Amount rhs)
    {
      CheckCurrencies(lhs, rhs);
      return new Amount(lhs.currency, lhs.amount * rhs.amount);
    }

    public static Amount operator *(decimal lhs, Amount rhs)
    {
      return new Amount(rhs.currency, lhs * rhs.amount);
    }

    public static Amount operator *(Amount lhs, decimal rhs)
    {
      return new Amount(lhs.currency, lhs.amount * rhs);
    }

    private static void CheckCurrencies(Amount lhs, Amount rhs)
    {
      if (lhs.currency != rhs.currency)
        throw new Exception("incompatible currencies");
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj))
        return false;
      if (ReferenceEquals(this, obj))
        return true;
      if (obj.GetType() != this.GetType())
        return false;
      return Equals((Amount)obj);
    }

    protected bool Equals(Amount other)
    {
      return amount.Equals(other.amount) && currency == other.currency;
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (amount.GetHashCode() * 397) ^ (int)currency;
      }
    }

    public override string ToString()
    {
      return amount.ToString();
    }
  }
}