using System;

namespace IngameScript
{
  partial class Program
  {
    public class ListDisplay
    {
      private readonly DisplayContext _ctx = new DisplayContext();
      private readonly string _label;

      public ListDisplay(int lineWidth, int barWidth, string label)
      {
        _ctx.HRLenght = lineWidth;
        _ctx.BarWidth = barWidth;
        _label = label;
      }

      public string Show(params ListDisplayItem[] rowDisplays)
      {
        System.Text.StringBuilder result = new System.Text.StringBuilder();
        result.AppendLine("[" + _label + "]");
        result.Append(_ctx.HR);
        foreach (ListDisplayItem rd in rowDisplays)
        {
          result.Append(Environment.NewLine + rd.Render(_ctx));
        }
        _ctx.Bar.Step();
        return result.ToString();
      }
    }
  }
}
