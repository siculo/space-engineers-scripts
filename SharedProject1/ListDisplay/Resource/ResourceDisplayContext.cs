using System;
using System.Collections.Generic;
using System.Text;
using VRage;

namespace IngameScript
{
  partial class Program
  {
    public class ResourceDisplayContext : DisplayContext
    {
      public MyFixedPoint MaxAmount { get; set; }
      public int ResourceNameSpace { get; set; }
      public int AmountSpace { get; set; }
      public int AmountDecimalDigits { get; set; }

      public ResourceDisplayContext()
      {
        MaxAmount = 0;
        ResourceNameSpace = 10;
        AmountSpace = 10;
        AmountDecimalDigits = 2;
      }
    }
  }
}
