using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
  partial class Program
  {
    public class EnergyRendererContext : RendererContext
    { 
      public int BarWidth { get; set; }

      public EnergyRendererContext()
      {
        Name = "Energy";
      }
    }
  }
}
