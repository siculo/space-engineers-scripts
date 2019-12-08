using System;

namespace IngameScript
{
  partial class Program
  {
    public abstract class EnergyItem : ListDisplayItem<DisplayContext>
    {
      public abstract string Name { get; }
      public abstract bool Enabled { get; }

      protected string RenderHeader(DisplayContext ctx)
      {
        return Name + " " + (Enabled ? "[" + ctx.Bar + "]" : "OFF");
      }
    }
  }
}
