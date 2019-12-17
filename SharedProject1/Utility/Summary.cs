using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IngameScript
{
  partial class Program
  {
    class Summary
    {
      private readonly Dictionary<ResourceType, Resource> _resources;

      public static Summary ContainersSummary(IEnumerable<Container> containers, string tags = null, IEnumerable<ResourceType> filter = null)
      {
        List<string> tagsList = Tags.SplitToTags(tags);
        IEnumerable<Container> filtered = containers.Where(c => c.HasAtLeastOneTag(tagsList));
        return filtered.Aggregate(new Summary(), (summary, c) => summary + ApplyFilter(c.GetResources(), filter));
      }

      private static IEnumerable<Resource> ApplyFilter(IEnumerable<Resource> resources, IEnumerable<ResourceType> filter)
      {
        return resources.Where(r => r.Match(filter));
      }

      public Summary(IEnumerable<Resource> resources = null)
      {
        if (resources == null)
        {
          _resources = new Dictionary<ResourceType, Resource>();
        }
        else
        {
          _resources = AggregateDuplicates(resources);
        }
      }

      public IEnumerable<Resource> GetResources()
      {
        return _resources.Values;
      }

      public static Summary operator +(Summary a, Summary b)
      {
        return new Summary(a.GetResources().Concat(b.GetResources()));
      }

      public static Summary operator +(Summary a, IEnumerable<Resource> b)
      {
        return a + new Summary(b);
      }

      internal float GetMaximum()
      {
        float max = 0;
        foreach(Resource resource in GetResources())
        {
          max = Math.Max((float)resource.Amount, max);
        }
        return max;
      }

      public override string ToString()
      {
        string msg = "";
        foreach (Resource res in GetResources())
        {
          if (msg.Length == 0)
          {
            msg = res.ToString();
          }
          else
          {
            msg = msg + ", " + res.ToString();
          }
        }
        return "Summary: " + msg;
      }

      public override bool Equals(object obj)
      {
        return this.Equals(obj as Summary);
      }

      public bool Equals(Summary that)
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
        return Enumerable.SequenceEqual(this.GetResources().OrderBy(kvp => kvp.Type), that.GetResources().OrderBy(kvp => kvp.Type));
      }

      public override int GetHashCode()
      {
        return _resources.GetHashCode();
      }

      private static Dictionary<ResourceType, Resource> AggregateDuplicates(IEnumerable<Resource> resources)
      {
        Dictionary<ResourceType, Resource> aggregated = new Dictionary<ResourceType, Resource>();
        foreach (Resource r in resources)
        {
          if (!aggregated.ContainsKey(r.Type))
          {
            aggregated.Add(r.Type, r);
          }
          else
          {
            aggregated[r.Type] = new Resource(r.Type, aggregated[r.Type].Amount + r.Amount);
          }
        }
        return aggregated;
      }
    }
  }
}
