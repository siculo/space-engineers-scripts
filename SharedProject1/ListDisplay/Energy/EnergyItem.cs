using System;

namespace IngameScript
{
  partial class Program
  {
    public abstract class EnergyItem : ListDisplayItem<EnergyDisplayContext>
    {
      public abstract string Name { get; }
      public abstract bool Enabled { get; }

      protected string RenderHeader(EnergyDisplayContext ctx)
      {
        return Name + " " + (Enabled ? "[" + ctx.Bar + "]" : "OFF");
      }
    }
  }
}
