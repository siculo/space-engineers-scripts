using System;

namespace IngameScript
{
  partial class Program
  {
    public abstract class EnergyItem : ListDisplayItem
    {
      public abstract string Name { get; }
      public abstract bool Enabled { get; }

      protected string RenderHeader(DisplayObjects obj)
      {
        return Name + " " + (Enabled ? "[" + obj.Bar + "]" : "OFF");
      }

      protected string RenderLevelBar(DisplayObjects obj, float max, float current)
      {
        if (max > 0)
        {
          string percent = string.Format("{0,3:0}%", 100f * current / max);
          int currentWidth = (int)Math.Round(obj.BarWidth * current / max);
          return string.Format("({0}{1}) {2}", new String('|', currentWidth), new String('.', obj.BarWidth - currentWidth), percent);
        }
        else
        {
          return string.Format("({0}) [NA]", new String(' ', obj.BarWidth));
        }
      }
    }
  }
}
