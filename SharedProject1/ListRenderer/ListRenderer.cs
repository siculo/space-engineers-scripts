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

      public ListRenderer(C ctx)
      {
        _ctx = ctx;
      }

      public string Render(params ListItemRenderer<C>[] rowDisplays)
      {
        return this.Render(rowDisplays.ToList());
      }

      public string Render(IEnumerable<ListItemRenderer<C>> rowDisplays)
      {
        StringBuilder result = new StringBuilder();
        result.AppendLine(string.Format("[{0} {1}]", _ctx.Name, SpinningBar.Render()));
        result.Append(_ctx.HR);
        foreach (ListItemRenderer<C> rd in rowDisplays)
        {
          if (rd != null)
          {
            result.Append(Environment.NewLine + rd.Render(_ctx));
          }
        }
        return result.ToString();
      }
    }
  }
}
