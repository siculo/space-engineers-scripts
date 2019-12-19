using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  partial class Program
  {
    class BlockSwitcher : MenuItem
    {
      private readonly IMyFunctionalBlock _block;
      private readonly bool _showSpinBar;

      public BlockSwitcher(IMyFunctionalBlock block, bool showSpinBar = false) : base(block.CustomName)
      {
        _block = block;
        _showSpinBar = showSpinBar;
      }

      override public string GetLabel()
      {
        string check = (_showSpinBar ? SpinningBar.Render() : "X");
        return " " + this.name + " " + (_block.Enabled ? "[" + check + "]" : "[ ]");
      }

      override public void Activate()
      {
        base.Activate();
        _block.Enabled = !_block.Enabled;
      }
    }
  }
}
