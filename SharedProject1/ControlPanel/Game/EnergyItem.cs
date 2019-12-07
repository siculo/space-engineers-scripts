using System;

namespace IngameScript
{
  partial class Program
  {
    public abstract class EnergyItem : ListDisplayItem
    {
      public abstract string Name { get; }
      public abstract bool Enabled { get; }

      protected string RenderHeader(DisplayContext ctx)
      {
        return Name + " " + (Enabled ? "[" + ctx.Bar + "]" : "OFF");
      }

      protected string RenderLevelBar(DisplayContext ctx, float max, float current)
      {
        if (max > 0)
        {
          string percent = string.Format("{0,3:0}%", 100f * current / max);
          int currentWidth = (int)Math.Round(ctx.BarWidth * current / max);
          return string.Format("({0}{1}) {2}", new String('|', currentWidth), new String('.', ctx.BarWidth - currentWidth), percent);
        }
        else
        {
          return string.Format("({0}) [NA]", new String(' ', ctx.BarWidth));
        }
      }
    }
  }
}
