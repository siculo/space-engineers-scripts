using System;

namespace IngameScript
{
  public abstract class EnergyBlock
  {
    public abstract string Name { get; }
    public abstract bool Enabled { get; }

    public abstract string Render(RenderData r);

    protected string RenderHeader(RenderData r) {
      return Name + " " + (Enabled ? "[" + r.Bar + "]" : "OFF");
    }

    protected string BarDisplay(RenderData r, float max, float current) {
      if(max > 0) {
        string percent = string.Format("{0,3:0}%", 100f * current / max);
        int currentWidth = (int)Math.Round(r.BarWidth * current / max);
        return string.Format("({0}{1}) {2}", new String('|', currentWidth), new String('.', r.BarWidth - currentWidth), percent);
      } else {
        return string.Format("({0}) [NA]", new String(' ', r.BarWidth));
      }
    }
  }
}
