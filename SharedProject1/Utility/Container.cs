using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
  partial class Program
  {
    interface Container
    {
      Summary GetResourceSummary();
    }

    static Summary ContainersSummary(IEnumerable<Container> containers)
    {
      Summary accumulated = new Summary();
      foreach (Container c in containers)
      {
        accumulated = c.GetResourceSummary() + accumulated;
      }
      return accumulated;
    }
  }
}
