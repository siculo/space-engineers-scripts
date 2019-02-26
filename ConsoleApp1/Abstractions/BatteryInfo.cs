namespace IngameScript
{
  // --------------------------------
  public abstract class BatteryInfo
  {
    public abstract string Name { get; }
    public abstract float Storage { get; }
    public abstract float Stored { get; }
    public abstract float Balance { get; }
    public abstract bool Enabled { get; }
    public abstract bool Charging { get; }
  }
  // --------------------------------
}
