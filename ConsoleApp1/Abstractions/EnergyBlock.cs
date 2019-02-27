using System;

namespace IngameScript
{
  public abstract class EnergyBlock
  {
    public abstract string Render(RenderData r);

    protected string BarDisplay(float max, float current, int width) {
      int currentWidth = (int)Math.Round(width * current / max);
      return "(" + new String('|', currentWidth) + new String('.', width - currentWidth) + ")";
    }
  }
}
