using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript.EnergyDisplayProgram
{
  class Program: GameProgram
  {
    EnergyDisplay display1 = new EnergyDisplay(66, 10);
    ListDisplayItem[] energyInfo1;
    EnergyDisplay display2 = new EnergyDisplay(66, 10);
    ListDisplayItem[] energyInfo2;

    public Program() {
      energyInfo1 = new ListDisplayItem[] {
            MyPowerProductionItem.Find(GridTerminalSystem, "reattore_1"),
            MyBatteryItem.Find(GridTerminalSystem, "Batteria_1"),
            MyBatteryItem.Find(GridTerminalSystem, "Batteria_2"),
            MyBatteryItem.Find(GridTerminalSystem, "Batteria_3"),
            MyBatteryItem.Find(GridTerminalSystem, "Batteria_4")
            };
      energyInfo2 = new ListDisplayItem[] {
            MyPowerProductionItem.Find(GridTerminalSystem, "pannello_solare_1"),
            MyPowerProductionItem.Find(GridTerminalSystem, "pannello_solare_2"),
            MyPowerProductionItem.Find(GridTerminalSystem, "pannello_solare_3"),
            MyPowerProductionItem.Find(GridTerminalSystem, "pannello_solare_4"),
            MyPowerProductionItem.Find(GridTerminalSystem, "pannello_solare_5"),
            MyPowerProductionItem.Find(GridTerminalSystem, "pannello_solare_6"),
            MyPowerProductionItem.Find(GridTerminalSystem, "pannello_solare_7")
            };
      Runtime.UpdateFrequency = UpdateFrequency.Update100;
    }

    public void Main(string argument, UpdateType updateSource) {
      System.Collections.Generic.List<IMyTextPanel> displays = new System.Collections.Generic.List<IMyTextPanel>();
      GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(displays, d => MyIni.HasSection(d.CustomData, "energy"));
      string report1 = display1.Show(energyInfo1);
      string report2 = display2.Show(energyInfo2);
      MyIni menuIni = new MyIni();
      MyIniParseResult br;
      foreach(IMyTextPanel d in displays) {
        if(menuIni.TryParse(d.CustomData, out br)) {
          string group = menuIni.Get("energy", "group").ToString();
          if(group.Equals("1")) {
            d.WriteText(report1);
          } else if(group.Equals("2")) {
            d.WriteText(report2);
          }
        }
      }
    }
  }
}
