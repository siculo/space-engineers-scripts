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
      Ice, Iron, Gold, Silver, Stone, Magnesium
    }

    public class Resource
    {
      public ResourceType Type { get; private set; }
      public MyFixedPoint Amount {  get; private set; }
      public string Name { get { return Type.ToString(); } }

      public Resource(ResourceType type): this(type, 0)
      {
      }

      public Resource(ResourceType type, MyFixedPoint amout)
      {
        Type = type;
        Amount = amout;
      }

      public override bool Equals(object obj)
      {
        return this.Equals(obj as Resource);
      }

      public bool Equals(Resource that)
      {
        if (Object.ReferenceEquals(that, null))
        {
          return false;
        }
        if (Object.ReferenceEquals(this, that))
        {
          return true;
        }
        if (this.GetType() != that.GetType())
        {
          return false;
        }
        return (Type == that.Type) && (Amount == that.Amount);
      }

      public override int GetHashCode()
      {
        return Type.GetHashCode() * 0x00010000 + Amount.GetHashCode();
      }
    }
  }
}
