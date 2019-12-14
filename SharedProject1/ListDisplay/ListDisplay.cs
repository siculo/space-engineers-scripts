using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IngameScript
{
  partial class Program
  {
    public class ListDisplay<C> where C : DisplayContext
    {
      private C _ctx;
      private string _label;

      public ListDisplay(C ctx, string label)
      {
        _ctx = ctx;
        _label = label;
      }

      public string Show(params ListDisplayItem<C>[] rowDisplays)
      {
        return this.Show(rowDisplays.ToList());
      }

      public string Show(IEnumerable<ListDisplayItem<C>> rowDisplays)
      {
        System.Text.StringBuilder result = new System.Text.StringBuilder();
        result.AppendLine(string.Format("[{0} {1}]", _label, _ctx.Bar));
        result.Append(_ctx.HR);
        foreach (ListDisplayItem<C> rd in rowDisplays)
        {
          if (rd != null)
          {
            result.Append(Environment.NewLine + rd.Render(_ctx));
          }
        }
        _ctx.Bar.Step();
        return result.ToString();
      }
    }
  }
}
