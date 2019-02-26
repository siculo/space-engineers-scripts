using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  // --------------------------------
  public class MyBatteryInfo:BatteryInfo
  {
    private readonly IMyBatteryBlock _battery;
    private readonly string _displayName;

    public static MyBatteryInfo Find(IMyGridTerminalSystem gts,string name) {
      return new MyBatteryInfo(gts.GetBlockWithName(name) as IMyBatteryBlock,name);
    }

    public MyBatteryInfo(IMyBatteryBlock battery,string displayName) {
      _battery = battery;
      _displayName = displayName;
    }

    public override string name {
      get { return _displayName; }
    }

    public override float storage {
      get { return _battery.MaxStoredPower; }
    }

    public override float stored {
      get { return _battery.CurrentStoredPower; }
    }

    public override float balance {
      get { return _battery.CurrentInput - _battery.CurrentOutput; }
    }
  }
}