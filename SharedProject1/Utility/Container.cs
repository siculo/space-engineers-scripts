﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IngameScript
{
  partial class Program
  {
    public abstract class Container
    {
      private IEnumerable<string> _tags;
      public Container(string tags = null)
      {
        _tags = Parsers.ParseTags(tags);
      }
      public abstract IEnumerable<ResourceStack> GetResources();

      public bool HasAtLeastOneTag(string tags = null)
      {
        return HasAtLeastOneTag(Parsers.ParseTags(tags));
      }

      public bool HasAtLeastOneTag(IEnumerable<string> tags)
      {
        return (tags != null && tags.Count() > 0) ? tags.Aggregate(false, (current, tag) => current || _tags.Contains(tag)) : true;
      }
    }

  }
}
