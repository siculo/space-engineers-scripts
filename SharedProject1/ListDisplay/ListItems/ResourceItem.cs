using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
  partial class Program
  {
    public class ResourceItem : ListDisplayItem
    {
      private Resource _resource;

      public ResourceItem(Resource resource)
      {
        _resource = resource;
      }

      public override string Render(DisplayContext ctx)
      {
        return string.Format(
          ctx.EnUS,
          "Ice      (|||||||||||) {0, 9}",
          ctx.allignToDecimalSeparator((double)_resource.Amount)
        );
      }
    }
  }
}
