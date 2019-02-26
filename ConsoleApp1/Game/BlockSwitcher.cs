using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  class BlockSwitcher: MenuItem
  {
    private readonly IMyFunctionalBlock _block;
    private readonly SpinningBar _bar;

    public BlockSwitcher(IMyFunctionalBlock block, SpinningBar bar = null) : base(block.CustomName) {
      _block = block;
      _bar = bar;
    }

    override public string GetLabel() {
      string check = (_bar != null ? _bar.ToString() : "X");
      return " " + this.name + " " + (_block.Enabled ? "[" + check + "]" : "[ ]");
    }

    override public void Activate() {
      base.Activate();
      _block.Enabled = !_block.Enabled;
    }
  }
}
