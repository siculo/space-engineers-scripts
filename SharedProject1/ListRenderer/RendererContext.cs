using System;

namespace IngameScript
{
  partial class Program
  {
    public class RendererContext
    {
      private string _hr;

      public SpinningBar Bar { get; } = new SpinningBar();

      public System.Globalization.CultureInfo EnUS { get; } = new System.Globalization.CultureInfo("en-US");

      public string HR {
        get { return _hr; }
      }

      private int _rowWidth = 0;

      public int RowWidth { get { return _rowWidth; } set { _rowWidth = value; _hr = new string('-', _rowWidth); } }

      public RendererContext()
      {
        _rowWidth = 30;
      }

      public string RenderLevelBar(double max, double current, int barWidth, bool showPercent = false)
      {
        if (showPercent)
          return RenderBar(max, current, barWidth) + " " + ((max > 0) ? string.Format("{0,3:0}%", 100f * current / max) : "[NA]");
        return RenderBar(max, current, barWidth);
      }

      private string RenderBar(double max, double current, int barWidth)
      {
        if (max > 0)
        {
          int currentWidth = (int)Math.Round(barWidth * current / max);
          return string.Format("({0}{1})", new String('|', currentWidth), new String('.', barWidth - currentWidth));
        }
        return string.Format("({0})", new String(' ', barWidth));
      }

      public string FormatInt(int value, int space)
      {
        return string.Format("{0," + space + "}", value);
      }

      public string FormatDouble(double value, int space, int decimalDigits = 2)
      {
        if (decimalDigits > 0)
        {
          int intPartSpace = space - decimalDigits - 1;
          string[] parts = string.Format(EnUS, "{0}", Math.Round(value, decimalDigits)).Split('.');
          string intPart = parts[0];
          string decPart = parts.Length > 1 ? "." + parts[1] : "";
          return string.Format("{0," + intPartSpace + "}{1, -" + (decimalDigits + 1) + "}", intPart, decPart);
        }
        else
        {
          return FormatInt((int)value, space);
        }
      }
    }
  }
}
