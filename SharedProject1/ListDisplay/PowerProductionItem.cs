using System;

namespace IngameScript
{
  partial class Program
  {
    public abstract class PowerProductionItem : EnergyItem
    {
      public abstract float MaxOutput { get; }
      public abstract float CurrentOutput { get; }

      public override string Render(DisplayObjects r)
      {
        return string.Format(
          r.EnUS, "{0}" + Environment.NewLine + " {3} {1}MW OUT {2}MW",
          RenderHeader(r),
          Math.Round(MaxOutput, 2),
          Math.Round(CurrentOutput, 2),
          RenderLevelBar(r, MaxOutput, CurrentOutput)
        );
      }
    }
  }
}
