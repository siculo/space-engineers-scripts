using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  class BlockSwitcher: MenuItem
  {
    private readonly IMyFunctionalBlock _block;

    public BlockSwitcher(IMyFunctionalBlock block) : base(block.CustomName) {
      _block = block;
    }

    override public string GetLabel() {
      return " " + this.name + " " + (_block.Enabled ? "[X]" : "[ ]");
    }

    override public void Activate() {
      base.Activate();
      _block.Enabled = !_block.Enabled;
    }
  }
}
