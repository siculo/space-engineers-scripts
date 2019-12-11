using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
  partial class Program
  {
    public class ResourceItem : ListDisplayItem<ResourceDisplayContext>
    {
      private Resource _resource;

      public ResourceItem(Resource resource)
      {
        _resource = resource;
      }

      public override string Render(ResourceDisplayContext ctx)
      {
        int barWidth = ctx.RowWidth - ctx.ResourceNameSpace - ctx.AmountSpace - 4;
        return string.Format(
          ctx.EnUS,
          "{0,-" + ctx.ResourceNameSpace + "} {1} {2," + ctx.AmountSpace + "}",
          _resource.Name.Substring(0, Math.Min(_resource.Name.Length, ctx.ResourceNameSpace)),
          ctx.RenderLevelBar((double)ctx.MaxAmount, (double)_resource.Amount, barWidth),
          ctx.FormatDouble((double)_resource.Amount, ctx.AmountSpace, ctx.AmountDecimalDigits)
        );
      }
    }
  }
}
