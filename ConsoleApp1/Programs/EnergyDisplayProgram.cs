using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript.EnergyDisplayProgram
{
  class Program: GameProgram
  {
    // --------------------------------
    EnergyDisplay display = new EnergyDisplay(34, 16);
    BatteryInfo[] batteryInfo;

    public Program() {
      batteryInfo = new BatteryInfo[] {
                MyBatteryInfo.Find(GridTerminalSystem, "Batteria_1"),
                MyBatteryInfo.Find(GridTerminalSystem, "Batteria_2"),
                MyBatteryInfo.Find(GridTerminalSystem, "Batteria_3"),
                MyBatteryInfo.Find(GridTerminalSystem, "Batteria_4")
            };
      Runtime.UpdateFrequency = UpdateFrequency.Update100;
    }

    public void Main(string argument, UpdateType updateSource) {
      System.Collections.Generic.List<IMyTextPanel> displays = new System.Collections.Generic.List<IMyTextPanel>();
      GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(displays, d => MyIni.HasSection(d.CustomData, "energy"));
      string report = display.Show(batteryInfo);
      foreach(IMyTextPanel d in displays) {
        d.WritePublicText(report);
      }
    }
    // --------------------------------
  }
}