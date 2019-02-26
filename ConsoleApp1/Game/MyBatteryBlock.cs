using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  public class MyBatteryBlock: BatteryBlock
  {
    private readonly IMyBatteryBlock _battery;
    private readonly string _displayName;

    public static MyBatteryBlock Find(IMyGridTerminalSystem gts, string name) {
      return new MyBatteryBlock(gts.GetBlockWithName(name) as IMyBatteryBlock, name);
    }

    public MyBatteryBlock(IMyBatteryBlock battery, string displayName) {
      _battery = battery;
      _displayName = displayName;
    }

    public override string Name {
      get { return _displayName; }
    }

    public override float Storage {
      get { return _battery.MaxStoredPower; }
    }

    public override float Stored {
      get { return _battery.CurrentStoredPower; }
    }

    public override float Balance {
      get { return _battery.CurrentInput - _battery.CurrentOutput; }
    }

    public override bool Enabled {
      get { return _battery.Enabled; }
    }

    public override bool Charging {
      get { return _battery.IsCharging; }
    }
  }
}