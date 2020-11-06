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
     * <summary>The type of any ingame resource</summary>
     */
    public class ResourceType : IComparable
    {
      public string TypeId { get; private set; }
      public string SubtypeId { get; private set; }
      public string TypeName { get { return ResourceNames.TypeIdToShortName(TypeId); } }
      public string SubtypeName { get { return ResourceNames.SubtypeIdToShortName(SubtypeId); } }

      public ResourceType(string typeId, string subtypeId)
      {
        TypeId = typeId;
        SubtypeId = subtypeId;
      }

      public bool Match(ResourceType filter)
      {
        if (filter.TypeId != null && !filter.TypeId.Equals(this.TypeId, StringComparison.OrdinalIgnoreCase))
        {
          return false;
        }
        if (filter.SubtypeId != null && !filter.SubtypeId.Equals(this.SubtypeId, StringComparison.OrdinalIgnoreCase))
        {
          return false;
        }
        return true;
      }

      public override string ToString()
      {
        return string.Format("({0}, {1})", TypeName, SubtypeName);
      }

      public override bool Equals(object that)
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
        ResourceType thatResourceType = that as ResourceType;
        return this.TypeId.Equals(thatResourceType.TypeId) && this.SubtypeId.Equals(thatResourceType.SubtypeId);
      }

      public override int GetHashCode()
      {
        return this.TypeId.GetHashCode() * 0x00010000 + this.SubtypeId.GetHashCode(); ;
      }

      public int CompareTo(object obj)
      {
        if (obj == null) return 1;
        ResourceType that = obj as ResourceType;
        if (that != null)
        {
          int typeIdCompare = this.TypeId.CompareTo(that.TypeId);
          if (typeIdCompare == 0)
          {
            return this.SubtypeId.CompareTo(that.SubtypeId);
          }
          return typeIdCompare;
        }
        else throw new ArgumentException("Object is not a ResourceType");
      }
    }
  }
}
