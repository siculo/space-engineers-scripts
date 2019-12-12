using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
  partial class Program
  {
    interface Container
    {
      IEnumerable<Resource> GetResources();
    }

  }
}
