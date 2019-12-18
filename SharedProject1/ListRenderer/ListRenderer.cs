using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IngameScript
{
  partial class Program
  {
    public class ListRenderer<C> where C : RendererContext
    {
      private C _ctx;
      private string _label;

      public ListRenderer(C ctx, string label)
      {
        _ctx = ctx;
        _label = label;
      }

      public string Render(params ListItemRenderer<C>[] rowDisplays)
      {
        return this.Render(rowDisplays.ToList());
      }

      public string Render(IEnumerable<ListItemRenderer<C>> rowDisplays)
      {
        System.Text.StringBuilder result = new System.Text.StringBuilder();
        result.AppendLine(string.Format("[{0} {1}]", _label, _ctx.Bar));
        result.Append(_ctx.HR);
        foreach (ListItemRenderer<C> rd in rowDisplays)
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
