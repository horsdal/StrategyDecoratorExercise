﻿namespace StrategyDecoratorExercise.Decorator.Price
{
  using System.Linq;
  using Money;
  using Order;

  public class SimpleTotalAmountCalculator : TotalAmountCalculator
  {
    public Amount CalculateTotalAmount(Order order)
    {
      return order.OrderLines.Aggregate(new Amount(order.Currency), (amount, line) => amount + line.ItemPrice * line.ItemCount);
    }
  }
}