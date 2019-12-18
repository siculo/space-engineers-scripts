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
        if (string.IsNullOrWhiteSpace(row))
        {
          return Enumerable.Empty<T>();
        }
        return row.Split(',').Select(item => itemParser(item)).Where(f => f != null);
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
            ParseValueAtPosition(p, 0, n => ResourceNames.NameToTypeId(n)),
            ParseValueAtPosition(p, 1, n => n)
            );
        }
        return null;
      }

      private static string ParseValueAtPosition(string[] p, int position, Func<string, string> transformation)
      {
        if (position < p.Length)
        {
          string value = p[position].Trim();
          return value.Length > 0 ? transformation(value) : null;
        }
        return null;
      }
    }
  }
}
