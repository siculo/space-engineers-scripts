using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IngameScript
{
  partial class Program
  {
    public static class Tags
    {
      public static List<string> SplitToTags(string tags)
      {
        if (tags == null)
        {
          return new List<String>();
        }
        return tags.Split(',').Select(x => x.Trim()).ToList();
      }
    }
  }
}
