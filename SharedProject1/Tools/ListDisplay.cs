using System;

namespace IngameScript
{
  partial class Program
  {
    public class ListDisplay
    {
      private readonly DisplayObjects _dataDisplayObjects = new DisplayObjects();
      private readonly string _label;

      public ListDisplay(int lineWidth, int barWidth, string label)
      {
        _dataDisplayObjects.HRLenght = lineWidth;
        _dataDisplayObjects.BarWidth = barWidth;
        _label = label;
      }

      public string Show(params ListDisplayItem[] rowDisplays)
      {
        System.Text.StringBuilder result = new System.Text.StringBuilder();
        result.AppendLine("[" + _label + "]");
        result.Append(_dataDisplayObjects.HR);
        foreach (ListDisplayItem rd in rowDisplays)
        {
          result.Append(Environment.NewLine + rd.Render(_dataDisplayObjects));
        }
        _dataDisplayObjects.Bar.Step();
        return result.ToString();
      }
    }
  }
}
