using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  partial class Program
  {
    public class MyBatteryItem : BatteryItem
    {
      private readonly IMyBatteryBlock _block;

      public static MyBatteryItem Find(IMyGridTerminalSystem gts, string name)
      {
        return new MyBatteryItem(gts.GetBlockWithName(name) as IMyBatteryBlock);
      }

      public MyBatteryItem(IMyBatteryBlock block)
      {
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
}
