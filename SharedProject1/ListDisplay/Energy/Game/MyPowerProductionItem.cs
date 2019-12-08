using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  partial class Program
  {
    public class MyPowerProductionItem : PowerProductionItem
    {
      private readonly IMyPowerProducer _block;

      public static MyPowerProductionItem Find(IMyGridTerminalSystem gts, string name)
      {
        return new MyPowerProductionItem(gts.GetBlockWithName(name) as IMyPowerProducer);
      }

      public MyPowerProductionItem(IMyPowerProducer block)
      {
        _block = block;
      }

      public override string Name { get { return _block.CustomName; } }
      public override bool Enabled { get { return _block.Enabled; } }
      public override float MaxOutput { get { return _block.MaxOutput; } }
      public override float CurrentOutput { get { return _block.CurrentOutput; } }
    }
  }
}
