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
      public int BarWidth { get; set; }

      public ResourceDisplayContext()
      {
        MaxAmount = 0;
      }
    }
  }
}
