using System;

namespace IngameScript
{
  partial class Program
  {
    public class DisplayContext
    {
      private string _hr;

      public SpinningBar Bar { get; } = new SpinningBar();

      public System.Globalization.CultureInfo EnUS { get; } = new System.Globalization.CultureInfo("en-US");

      public string HR {
        get { return _hr; }
      }

      // TODO: use RowWidth
      public int HRLenght {
        get { return _rowWidth; }
        set { _rowWidth = value; _hr = new string('-', _rowWidth); }
      }

      private int _rowWidth = 18;

      public int RowWidth { get { return _rowWidth; } set { _rowWidth = value; _hr = new string('-', _rowWidth); } }

      // TODO: use RowWidth if you want to calculate bar width from row width
      public int BarWidth { get; set; }

      public DisplayContext()
      {
        HRLenght = 33;
      }
      public string allignToDecimalSeparator(double value)
      {
        string[] parts = string.Format(EnUS, "{0}", Math.Round(value, 2)).Split('.');
        string intPart = parts[0];
        string decPart = parts.Length > 1 ? "." + parts[1] : "";
        return string.Format("{0, 6}{1, -3}", intPart, decPart);
      }
    }
  }
}
