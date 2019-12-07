using System;

namespace IngameScript
{
  partial class Program
  {
    public abstract class PowerProductionItem : EnergyItem
    {
      public abstract float MaxOutput { get; }
      public abstract float CurrentOutput { get; }

      public override string Render(DisplayContext ctx)
      {
        return string.Format(
          ctx.EnUS, "{0}" + Environment.NewLine + " {3} {1}MW OUT {2}MW",
          RenderHeader(ctx),
          Math.Round(MaxOutput, 2),
          Math.Round(CurrentOutput, 2),
          RenderLevelBar(ctx, MaxOutput, CurrentOutput)
        );
      }
    }
  }
}
