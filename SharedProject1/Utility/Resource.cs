using System;
using System.Collections.Generic;
using System.Text;
using VRage;

namespace IngameScript
{
  partial class Program
  {
    public enum ResourceType
    {
      Ice
    }

    public class Resource
    {
      public ResourceType Type { get; private set; }
      public MyFixedPoint Amount {  get; private set; }

      public Resource(ResourceType type): this(type, 0)
      {
      }

      public Resource(ResourceType type, MyFixedPoint amout)
      {
        Type = type;
        Amount = amout;
      }
    }
  }
}
