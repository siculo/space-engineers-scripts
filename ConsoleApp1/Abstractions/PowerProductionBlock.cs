using System;

namespace IngameScript
{
  public abstract class PowerProductionBlock: EnergyBlock
  {
    public abstract float MaxOutput { get; }
    public abstract float CurrentOutput { get; }

    public override string Render(RenderData r) {
      return string.Format(
        r.EnUS, "{0}" + Environment.NewLine + " {3} {1}MW OUT {2}MW",
        RenderHeader(r),
        Math.Round(MaxOutput, 2),
        Math.Round(CurrentOutput, 2),
        BarDisplay(r, MaxOutput, CurrentOutput)
      );
    }
  }
}
