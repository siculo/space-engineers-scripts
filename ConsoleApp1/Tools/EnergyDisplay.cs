namespace IngameScript
{
  public class EnergyDisplay
  {
    private readonly RenderData _renderData = new RenderData();

    public EnergyDisplay(int lineWidth, int batteryChargeWidth) {
      _renderData.HRLenght = lineWidth;
      _renderData.BatteryChargeWidth = batteryChargeWidth;
    }

    public string Show(params EnergyBlock[] blocks) {
      System.Text.StringBuilder result = new System.Text.StringBuilder();
      result.AppendLine("[Energia]");
      result.Append(_renderData.HR);
      foreach(EnergyBlock b in blocks) {
        result.Append(b.Render(_renderData));
      }
      _renderData.Bar.Step();
      return result.ToString();
    }
  }
}