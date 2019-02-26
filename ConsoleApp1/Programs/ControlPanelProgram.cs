using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript.ControlPanelProgram
{
  class Program: GameProgram
  {
    ControlPanel panel;
    IMyTextPanel display;
    MyIni _ini = new MyIni();
    SpinningBar _bar = new SpinningBar();

    public Program() {
      MyIniParseResult result;
      if(!_ini.TryParse(Me.CustomData, out result)) {
        Echo("c'è un problema: " + result.Success + " " + result.Error);
      } else {
        display = GridTerminalSystem.GetBlockWithName(_ini.Get("config", "monitor").ToString()) as IMyTextPanel;
        panel = new ControlPanel(33, 12);
        Menu menu = new Menu("Main");

        System.Collections.Generic.List<IMyFunctionalBlock> blocks = new System.Collections.Generic.List<IMyFunctionalBlock>();
        GridTerminalSystem.GetBlocksOfType<IMyFunctionalBlock>(blocks, b => MyIni.HasSection(b.CustomData, "switchable"));
        foreach(IMyFunctionalBlock b in blocks) {
          MyIniParseResult br;
          if (_ini.TryParse(b.CustomData, out br) && _ini.Get("switchable", "spinning").ToBoolean()) {
            menu.AddItem(new BlockSwitcher(b, _bar));
          } else {
            menu.AddItem(new BlockSwitcher(b));
          }
        }
        // menu.AddItem(new BlockSwitcher(GridTerminalSystem.GetBlockWithName("Batteria_1") as IMyFunctionalBlock, _bar));
        panel.SetMenu(menu);
        display.WritePublicText(panel.ToString());
        Runtime.UpdateFrequency = UpdateFrequency.Update100;
      }
    }

    public void Main(string argument) {
      if(argument == "next") {
        panel.SelectNext();
      } else if(argument == "prev") {
        panel.SelectPrevious();
      } else if(argument == "current") {
        panel.ActivateCurrent();
      } else if(argument == "parent") {
        panel.ActivateParent();
      } else {
        _bar.Step();
      }
      display.WritePublicText(panel.ToString());
    }
  }
}
