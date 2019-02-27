using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  public class MyBatteryBlock: BatteryBlock
  {
    private readonly IMyBatteryBlock _block;

    public static MyBatteryBlock Find(IMyGridTerminalSystem gts, string name) {
      return new MyBatteryBlock(gts.GetBlockWithName(name) as IMyBatteryBlock);
    }

    public MyBatteryBlock(IMyBatteryBlock block) {
      _block = block;
    }

    public override string Name { get { return _block.CustomName; } }
    public override bool Enabled { get { return _block.Enabled; } }
    public override float Storage { get { return _block.MaxStoredPower; } }
    public override float Stored { get { return _block.CurrentStoredPower; } }
    public override float Balance { get { return _block.CurrentInput - _block.CurrentOutput; } }
    public override bool Charging { get { return _block.IsCharging; } }
  }
}