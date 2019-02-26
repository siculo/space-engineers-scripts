namespace IngameScript
{
  // --------------------------------
  public abstract class BatteryInfo
  {
    public abstract string name { get; }
    public abstract float storage { get; }
    public abstract float stored { get; }
    public abstract float balance { get; }
  }
  // --------------------------------
}
