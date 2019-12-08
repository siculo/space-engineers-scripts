using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
  partial class Program : MyGridProgram
  {
    DisplayContext ctx1;
    EnergyDisplay display1;
    ListDisplayItem<DisplayContext>[] energyInfo1;

    DisplayContext ctx2;
    EnergyDisplay display2;
    ListDisplayItem<DisplayContext>[] energyInfo2;

    public Program()
    {
      ctx1 = new DisplayContext();
      ctx1.BarWidth = 10;
      ctx1.RowWidth = 66;
      display1 = new EnergyDisplay(ctx1);

      ctx2 = new DisplayContext();
      ctx2.BarWidth = 10;
      ctx2.RowWidth = 66;
      display2 = new EnergyDisplay(ctx2);

      energyInfo1 = new ListDisplayItem<DisplayContext>[] {
            MyPowerProductionItem.Find(GridTerminalSystem, "reattore_1"),
            MyBatteryItem.Find(GridTerminalSystem, "Batteria_1"),
            MyBatteryItem.Find(GridTerminalSystem, "Batteria_2"),
            MyBatteryItem.Find(GridTerminalSystem, "Batteria_3"),
            MyBatteryItem.Find(GridTerminalSystem, "Batteria_4")
            };
      energyInfo2 = new ListDisplayItem<DisplayContext>[] {
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

    public void Main(string argument, UpdateType updateSource)
    {
      System.Collections.Generic.List<IMyTextPanel> displays = new System.Collections.Generic.List<IMyTextPanel>();
      GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(displays, d => MyIni.HasSection(d.CustomData, "energy"));
      string report1 = display1.Show(energyInfo1);
      string report2 = display2.Show(energyInfo2);
      MyIni menuIni = new MyIni();
      MyIniParseResult br;
      foreach (IMyTextPanel d in displays)
      {
        if (menuIni.TryParse(d.CustomData, out br))
        {
          string group = menuIni.Get("energy", "group").ToString();
          if (group.Equals("1"))
          {
            d.WriteText(report1);
          }
          else if (group.Equals("2"))
          {
            d.WriteText(report2);
          }
        }
      }
    }
  }
}
