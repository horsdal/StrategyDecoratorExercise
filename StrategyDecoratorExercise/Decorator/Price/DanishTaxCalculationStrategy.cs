﻿namespace StrategyDecoratorExercise.Decorator.Price
{
  using System;
  using Money;
  using Order;

  public class DanishTaxCalculationStrategy : TaxCalculationStrategy
  {
    public Amount B2CTax(Order order, Amount total)
    {
      throw new NotImplementedException();
    }

    public Amount B2BTax(Order order, Amount total)
    {
      throw new NotImplementedException();
    }
  }
}