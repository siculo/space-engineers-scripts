using System;
using System.Collections.Generic;
using System.Text;
using VRage;

namespace IngameScript
{
  partial class Program
  {
    public class ResourceRendererContext : RendererContext
    {
      public MyFixedPoint MaxAmount { get; set; }
      public int ResourceNameSpace { get; set; }
      public int AmountSpace { get; set; }
      public int AmountDecimalDigits { get; set; }
      public int ResourceTypeSpace { get; set; }

      public ResourceRendererContext()
      {
        Name = "Resources";
        MaxAmount = 0;
        ResourceNameSpace = 10;
        AmountSpace = 10;
        AmountDecimalDigits = 2;
        ResourceTypeSpace = 0;
      }
    }
  }
}
