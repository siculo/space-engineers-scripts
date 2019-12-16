using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IngameScript
{
  partial class Program
  {
    abstract class Container
    {
      private List<string> _tags;
      public Container(string tags = null)
      {
        _tags = Tags.SplitToTags(tags);
      }
      public abstract IEnumerable<Resource> GetResources();

      public bool HasAtLeastOneTag(string tags = null)
      {
        return this.HasAtLeastOneTag(Tags.SplitToTags(tags));
      }

      public bool HasAtLeastOneTag(IEnumerable<string> tags)
      {
        return (tags.Count() > 0) ? tags.Aggregate(false, (current, tag) => current || _tags.Contains(tag)) : true;
      }
    }

  }
}
