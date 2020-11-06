using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
  partial class Program
  {
    public class ResourceItemRenderer : ListItemRenderer<ResourceRendererContext>
    {
      private ResourceStack _resource;

      public ResourceItemRenderer(ResourceStack resource)
      {
        _resource = resource;
      }

      public override string Render(ResourceRendererContext ctx)
      {
        int barWidth = ctx.RowWidth - ctx.ResourceNameSpace - ctx.AmountSpace - 4;
        if (ctx.ResourceTypeSpace > 0)
        {
          barWidth = barWidth - 2 - ctx.ResourceTypeSpace;
        }
        return string.Format(
          ctx.EnUS,
          "{0,-" + ctx.ResourceNameSpace + "}{1} {2} {3," + ctx.AmountSpace + "}",
          _resource.Type.SubtypeName.Substring(0, Math.Min(_resource.Type.SubtypeName.Length, ctx.ResourceNameSpace)),
          RenderResourceType(ctx),
          ctx.RenderLevelBar((double)ctx.MaxAmount, (double)_resource.Amount, barWidth),
          ctx.FormatDouble((double)_resource.Amount, ctx.AmountSpace, ctx.AmountDecimalDigits)
        );
      }

      private string RenderResourceType(ResourceRendererContext ctx)
      {
        if (ctx.ResourceTypeSpace > 0)
        {
          string typeName = _resource.Type.TypeName.Substring(0, Math.Min(_resource.Type.TypeName.Length, ctx.ResourceTypeSpace));
          return string.Format("{0,-" + (ctx.ResourceTypeSpace + 2) + "}", "(" + typeName + ")");
        }
        return "";
      }
    }
  }
}
