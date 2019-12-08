using System;

namespace IngameScript
{
  partial class Program
  {
    public class ListDisplay
    {
      private DisplayContext _ctx;
      private string _label;

      public ListDisplay(DisplayContext ctx, string label)
      {
        _ctx = ctx;
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
