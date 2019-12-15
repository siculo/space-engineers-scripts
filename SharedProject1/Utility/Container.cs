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

      public bool HasTags(string tags = null)
      {
        return this.HasTags(Tags.SplitToTags(tags));
      }

      public bool HasTags(IEnumerable<string> tags)
      {
        return tags.Aggregate(true, (current, tag) => current && _tags.Contains(tag));
      }
    }

  }
}
