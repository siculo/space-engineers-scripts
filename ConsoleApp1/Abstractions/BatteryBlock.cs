using System;

namespace IngameScript
{
  public abstract class BatteryBlock: EnergyBlock
  {
    private static int _batteryChargeWidth = 18;

    public abstract string Name { get; }
    public abstract float Storage { get; }
    public abstract float Stored { get; }
    public abstract float Balance { get; }
    public abstract bool Enabled { get; }
    public abstract bool Charging { get; }

    public override string Render(RenderData r) {
      return String.Format(
        r.EnUS, Environment.NewLine + "{0} {1} {2}Mhw {3} {4}w" + Environment.NewLine + " {5} {6}% | {7}Mhw",
        Name, Enabled ? "[" + r.Bar + "]" : "OFF", Math.Round(Storage, 2), Charging ? "IN" : "OUT", Math.Round(Balance, 2),
        BarDisplay(Storage, Stored, _batteryChargeWidth), Math.Round(100f * Stored / Storage, 0), Math.Round(Stored, 2)
      );
    }
  }
}
