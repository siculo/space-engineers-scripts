﻿using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript.EnergyDisplayProgram
{
  class Program: GameProgram
  {
    EnergyDisplay display = new EnergyDisplay(66, 10);
    EnergyBlock[] batteryInfo;

    public Program() {
      batteryInfo = new EnergyBlock[] {
            MyBatteryBlock.Find(GridTerminalSystem, "Batteria_1"),
            MyBatteryBlock.Find(GridTerminalSystem, "Batteria_2"),
            MyBatteryBlock.Find(GridTerminalSystem, "Batteria_3"),
            MyBatteryBlock.Find(GridTerminalSystem, "Batteria_4"),
            MyPowerProductionBlock.Find(GridTerminalSystem, "reattore_1"),
            MyPowerProductionBlock.Find(GridTerminalSystem, "pannello_solare_1"),
            MyPowerProductionBlock.Find(GridTerminalSystem, "pannello_solare_2"),
            MyPowerProductionBlock.Find(GridTerminalSystem, "pannello_solare_3"),
            MyPowerProductionBlock.Find(GridTerminalSystem, "pannello_solare_4")
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
  }
}