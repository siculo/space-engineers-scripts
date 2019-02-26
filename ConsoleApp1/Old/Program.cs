#region pre-script
using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  public class Program1: MyGridProgram
  {
    #endregion
    // ++++++++++++++++++++++++++++++++

    private IMyTextPanel outputConsole;
    private DisplayConsole displayConsole;
    private int count = 1;

    public Program1() {
      outputConsole = GridTerminalSystem.GetBlockWithName("Monitor SX") as IMyTextPanel;
      displayConsole = new DisplayConsole(new TextPanelOutput(outputConsole), 8);
    }

    public void Save() {
    }

    public void Main(string argument) {
      count = count + 1;
      displayConsole.WriteLine(argument + " " + count);
    }

    // --------------------------------
    #region post-script
  }

  public class Program2: MyGridProgram
  {
    #endregion
    // ++++++++++++++++++++++++++++++++

    ControlPanel panel;
    IMyTextPanel display;
    IMyProgrammableBlock serverAirlock;
    string[] airlockNames = new string[] { "L0_Airlock", "L1_Airlock", "L8_Airlock_0", "L8_Airlock_1" };

    public Program2() {
      display = GridTerminalSystem.GetBlockWithName("L5_Console_Display") as IMyTextPanel;
      serverAirlock = GridTerminalSystem.GetBlockWithName("L5_Server_Airlocks") as IMyProgrammableBlock;
      Menu menu = new Menu("Main");
      Menu submenu = new Menu("Airlocks");
      foreach(string name in airlockNames) {
        submenu.AddItem(new Command("Exit " + name, serverAirlock, "exit " + name));
        submenu.AddItem(new Command("Enter " + name, serverAirlock, "enter " + name));
      }
      menu.AddItem(submenu);
      panel = new ControlPanel(37, 15);
      panel.SetMenu(menu);
      display.WritePublicText(panel.ToString());
    }

    public void Save() {
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

    // --------------------------------
    #region post-script
  }
}
#endregion