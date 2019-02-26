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

    public EnergyDisplay(int lineWidth, int batteryChargeWidth) {
      _lineWidth = lineWidth;
      _horizontalRow = new String('-', lineWidth);
      _batteryChargeWidth = batteryChargeWidth;
    }

    public string Show(params BatteryInfo[] batteries) {
      System.Text.StringBuilder result = new System.Text.StringBuilder();
      result.AppendLine("[Energia]");
      result.Append(_horizontalRow);
      foreach(BatteryInfo battery in batteries) {
        string runStatus = battery.Enabled ? "[" + spinningBar.ToString() + "]" : "OFF";
        string charging = battery.Charging ? "IN" : "OUT";
        result.AppendLine();
        result.AppendLine(String.Format(enUS, "{0} {1} {2}Mhw {3} {4}w", battery.Name, runStatus, Math.Round(battery.Storage, 2), charging, Math.Round(battery.Balance, 2)));
        result.Append(String.Format(enUS, " {0} {1}% | {2}Mhw", BarDisplay(battery.Storage, battery.Stored, _batteryChargeWidth), Math.Round(100f * battery.Stored / battery.Storage, 0), Math.Round(battery.Stored, 2)));
      }
      spinningBar.Step();
      return result.ToString();
    }

    private string BarDisplay(float max, float current, int width) {
      int currentWidth = (int)Math.Round(_batteryChargeWidth * current / max);
      return "(" + new String('|', currentWidth) + new String('.', width - currentWidth) + ")";
    }
  }
  // --------------------------------
}