﻿using System;

namespace IngameScript
{
  partial class Program
  {
    public abstract class BatteryItem : EnergyItem
    {
      public abstract float Storage { get; }
      public abstract float Stored { get; }
      public abstract float Balance { get; }
      public abstract bool Charging { get; }

      public override string Render(DisplayObjects r)
      {
        return String.Format(
          r.EnUS, "{0}" + Environment.NewLine + " {4} {1}MWh {2} {3}W {5}MWh",
          RenderHeader(r),
          Math.Round(Storage, 2),
          Charging ? " IN" : "OUT",
          Math.Round(Balance, 2),
          RenderLevelBar(r, Storage, Stored),
          Math.Round(Stored, 2)
        );
      }
    }
  }
}