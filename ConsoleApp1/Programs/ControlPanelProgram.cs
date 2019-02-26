using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript.ControlPanelProgram
{
  class Program: GameProgram
  {
    ControlPanel panel;
    IMyTextPanel display;
    MyIni _ini = new MyIni();

    public Program() {
      MyIniParseResult result;
      if(!_ini.TryParse(Me.CustomData, out result)) {
        Echo("c'è un problema: " + result.Success + " " + result.Error);
      } else {
        display = GridTerminalSystem.GetBlockWithName(_ini.Get("config", "monitor").ToString()) as IMyTextPanel;
        panel = new ControlPanel(33, 12);
        Menu menu = new Menu("Main");
        menu.AddItem(new BlockSwitcher(GridTerminalSystem.GetBlockWithName("Batteria_1") as IMyFunctionalBlock));
        menu.AddItem(new BlockSwitcher(GridTerminalSystem.GetBlockWithName("Batteria_2") as IMyFunctionalBlock));
        menu.AddItem(new BlockSwitcher(GridTerminalSystem.GetBlockWithName("Batteria_3") as IMyFunctionalBlock));
        menu.AddItem(new BlockSwitcher(GridTerminalSystem.GetBlockWithName("Batteria_4") as IMyFunctionalBlock));
        menu.AddItem(new BlockSwitcher(GridTerminalSystem.GetBlockWithName("reattore_piccolo") as IMyFunctionalBlock));
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
      }
      display.WritePublicText(panel.ToString());
    }
  }
}
