using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  public class MyPowerProductionBlock: PowerProductionBlock
  {
    private readonly IMyPowerProducer _block;

    public static MyPowerProductionBlock Find(IMyGridTerminalSystem gts, string name) {
      return new MyPowerProductionBlock(gts.GetBlockWithName(name) as IMyPowerProducer);
    }

    public MyPowerProductionBlock(IMyPowerProducer block) {
      _block = block;
    }

    public override string Name { get { return _block.CustomName; } }
    public override bool Enabled { get { return _block.Enabled; } }
    public override float MaxOutput { get { return _block.MaxOutput; } }
    public override float CurrentOutput { get { return _block.CurrentOutput; } }
  }
}
