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
        int rows = _ini.Get("config", "rows").ToInt32(20);
        int columns = _ini.Get("config", "columns").ToInt32(15);
        panel = new ControlPanel(columns, rows);
        panel.SetMenu(SwitchMenu());
        display.WritePublicText(panel.ToString());
        Runtime.UpdateFrequency = UpdateFrequency.Update100;
      }
    }

    private Menu SwitchMenu() {
      Menu menu = new Menu("Switch");
      System.Collections.Generic.List<IMyFunctionalBlock> blocks = new System.Collections.Generic.List<IMyFunctionalBlock>();
      GridTerminalSystem.GetBlocksOfType<IMyFunctionalBlock>(blocks, b => MyIni.HasSection(b.CustomData, "switchable"));
      blocks.Sort(delegate (IMyFunctionalBlock b1, IMyFunctionalBlock b2) {
        return b1.CustomName.CompareTo(b2.CustomName);
      });
      foreach(IMyFunctionalBlock b in blocks) {
        MyIniParseResult br;
        if(_ini.TryParse(b.CustomData, out br) && _ini.Get("switchable", "spinning").ToBoolean()) {
          menu.AddItem(new BlockSwitcher(b, _bar));
        } else {
          menu.AddItem(new BlockSwitcher(b));
        }
      }
      return menu;
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
