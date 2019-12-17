using System;
using System.Collections.Generic;
using System.Text;
using VRage;
using System.Linq;

namespace IngameScript
{
  partial class Program
  {
    public class ResourceType: IComparable {
      public string TypeId { get; private set; }
      public string SubtypeId { get; private set; }
      public string TypeName { get { return GetTypeName(TypeId); } }
      public string SubtypeName { get { return GetSubtypeName(SubtypeId); } }

      private static string _prefix = "MyObjectBuilder_";
      private static Dictionary<string, string> typeNames = new Dictionary<string, string>()
      {
        ["AmmoMagazine"] = "Ammo",
        ["Component"] = "Comp.",
        ["PhysicalCubeBlockObject"] = "Block",
        ["PhysicalGunObject"] = "gun"
      };

      public ResourceType(string typeId, string subtypeId)
      {
        TypeId = typeId;
        SubtypeId = subtypeId;
      }

      public bool Match(ResourceType filter)
      {
        if (filter.TypeId != null && !filter.TypeId.Equals(this.TypeId))
        {
          return false;
        }
        if (filter.SubtypeId != null && !filter.SubtypeId.Equals(this.SubtypeId))
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

      private string GetTypeName(string typeId)
      {
        string typeKey = typeId.StartsWith(_prefix) ? typeId.Substring(_prefix.Length) : typeId;
        if (typeNames.ContainsKey(typeKey)) {
          return typeNames[typeKey];
        }
        return typeKey;
      }
      private string GetSubtypeName(string subtypeId)
      {
        return subtypeId;
      }
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

      public bool Match(IEnumerable<ResourceType> filter)
      {
        if (filter != null)
        {
          return (filter.Count() > 0) ? filter.Aggregate(false, (current, f) => current || this.Type.Match(f)) : true;
        }
        return true;
      }

      public override string ToString()
      {
        return  string.Format("({0}: {1})", Type, Amount);
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
        return (Type.Equals(that.Type)) && (Amount.Equals(that.Amount));
      }

      public override int GetHashCode()
      {
        return Type.GetHashCode() * 0x00010000 + Amount.GetHashCode();
      }
    }
  }
}
