using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using SpaceEngineers.Game.ModAPI.Ingame;

namespace IngameScript
{
  partial class Program
  {
    public class MyBatteryItemRenderer : BatteryItemRenderer
    {
      private readonly IMyBatteryBlock _block;

      public static MyBatteryItemRenderer Find(IMyGridTerminalSystem gts, string name)
      {
        IMyBatteryBlock batteryBlock = gts.GetBlockWithName(name) as IMyBatteryBlock;
        if (batteryBlock == null)
        {
          return null;
        }
        return new MyBatteryItemRenderer(batteryBlock);
      }

      public MyBatteryItemRenderer(IMyBatteryBlock block)
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
