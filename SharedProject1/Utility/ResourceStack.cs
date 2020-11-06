using System;
using System.Collections.Generic;
using System.Text;
using VRage;
using System.Linq;

namespace IngameScript
{
  partial class Program
  {
    /**
     * <summary>A stack of resources with a <c>Type</c> and an <c>Amount</c></summary>
     */
    public class ResourceStack
    {
      public ResourceType Type { get; private set; }
      public MyFixedPoint Amount {  get; private set; }

      public ResourceStack(ResourceType type): this(type, 0)
      {
      }

      public ResourceStack(ResourceType type, MyFixedPoint amout)
      {
        Type = type;
        Amount = amout;
      }

      public bool Match(IEnumerable<ResourceType> filter)
      {
        if (filter == null || filter.Count() == 0)
        {
          return true;
        }
        return filter.Aggregate(false, (current, f) => current || this.Type.Match(f));
      }

      public IEnumerable<ResourceStack> ConsolidateTo(IEnumerable<ResourceStack> collection)
      {
        List<ResourceStack> consolidated = new List<ResourceStack>();
        Boolean found = false;
        foreach (ResourceStack r in collection)
        {
          if (r.Type.Equals(this.Type))
          {
            consolidated.Add(new ResourceStack(r.Type, this.Amount + r.Amount));
            found = true;
          }
          else
          {
            consolidated.Add(r);
          }
        }
        if (!found)
        {
          consolidated.Add(this);
        }
        return consolidated;
      }

      public override string ToString()
      {
        return  string.Format("({0}: {1})", Type, Amount);
      }

      public override bool Equals(object obj)
      {
        return this.Equals(obj as ResourceStack);
      }

      public bool Equals(ResourceStack that)
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
        return (Type.Equals(that.Type)) && (Amount.Equals(that.Amount));
      }

      public override int GetHashCode()
      {
        return Type.GetHashCode() * 0x00010000 + Amount.GetHashCode();
      }
    }
  }
}
