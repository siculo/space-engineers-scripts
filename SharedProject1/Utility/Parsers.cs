using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IngameScript
{
  partial class Program
  {
    public static class Parsers
    {
      public static IEnumerable<string> ParseTags(string tags)
      {
        return ParseRow<string>(tags, item => ParseTag(item));
      }

      public static IEnumerable<ResourceType> ParseResourceFilter(string filter)
      {
        return ParseRow<ResourceType>(filter, item => ParseRule(item));
      }

      private static IEnumerable<T> ParseRow<T>(string row, Func<string, T> itemParser)
      {
        if (IsEmpty(row))
        {
          return null;
        }
        return row.Split(',').Select(item => itemParser(item)).Where(f => f != null);
      }
      private static bool IsEmpty(string p)
      {
        return p == null || p.Trim().Length == 0;
      }

      private static string ParseTag(string tag)
      {
        string result = tag.Trim();
        return result.Length > 0 ? result : null;
      }

      private static ResourceType ParseRule(string rule)
      {
        string[] p = rule.Split(':');
        if (p.Length > 0)
        {
          return new ResourceType(
            ParseValueAtPosition(p, 0),
            ParseValueAtPosition(p, 1)
            );
        }
        return null;
      }

      private static string ParseValueAtPosition(string[] p, int position)
      {
        if (position < p.Length)
        {
          string value = p[position].Trim();
          return value.Length > 0 ? value : null;
        }
        return null;
      }
    }
  }
}
