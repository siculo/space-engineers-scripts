using System;

namespace IngameScript
{
  // --------------------------------
  public class EnergyDisplay
  {
    private readonly System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US");
    private readonly int _lineWidth;
    private readonly string _horizontalRow;
    private readonly int _batteryChargeWidth;
    private readonly SpinningBar spinningBar = new SpinningBar();

    public EnergyDisplay(int lineWidth,int batteryChargeWidth) {
      _lineWidth = lineWidth;
      _horizontalRow = new String('-',lineWidth);
      _batteryChargeWidth = batteryChargeWidth;
    }

    public string Show(params BatteryInfo[] batteries) {
      System.Text.StringBuilder result = new System.Text.StringBuilder();
      bool first = true;
      foreach(BatteryInfo battery in batteries) {
        if(first) {
          first = false;
        } else {
          result.AppendLine();
          result.AppendLine();
        }
        result.AppendLine(String.Format("[{0} {1}]",battery.name,spinningBar));
        result.AppendLine(_horizontalRow);
        result.AppendLine(String.Format(enUS,"{0}Mhw | I/O = {1}w",Math.Round(battery.storage,2),Math.Round(battery.balance,2)));
        result.Append(String.Format(enUS,"{0} {1}%, {2}Mhw",BarDisplay(battery.storage,battery.stored,_batteryChargeWidth),Math.Round(100f * battery.stored / battery.storage,0),Math.Round(battery.stored,2)));
      }
      spinningBar.Step();
      return result.ToString();
    }

    private string BarDisplay(float max,float current,int width) {
      int currentWidth = (int)Math.Round(_batteryChargeWidth * current / max);
      return "(" + new String('|',currentWidth) + new String('.',width - currentWidth) + ")";
    }
  }
  // --------------------------------
}