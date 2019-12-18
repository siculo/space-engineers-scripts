using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  partial class Program
  {
    public class MyPowerProductionItemRenderer : PowerProductionItem
    {
      private readonly IMyPowerProducer _block;

      public static MyPowerProductionItemRenderer Find(IMyGridTerminalSystem gts, string name)
      {
        IMyPowerProducer powerProducer = gts.GetBlockWithName(name) as IMyPowerProducer;
        if (powerProducer == null)
        {
          return null;
        }
        return new MyPowerProductionItemRenderer(powerProducer);
      }

      public MyPowerProductionItemRenderer(IMyPowerProducer block)
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
