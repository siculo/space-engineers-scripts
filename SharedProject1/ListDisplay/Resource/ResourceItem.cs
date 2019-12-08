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
        return string.Format(
          ctx.EnUS,
          "Ice      {0} {1, 9}",
          ctx.RenderLevelBar((double)ctx.MaxAmount, (double)_resource.Amount, 11),
          ctx.AllignToDecimalSeparator((double)_resource.Amount)
        );
      }
    }
  }
}
