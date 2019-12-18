using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
  partial class Program : MyGridProgram
  {
    EnergyRendererContext ctx1;
    EnergyListRenderer display1;
    EnergyItemRenderer[] energyInfo1;

    EnergyRendererContext ctx2;
    EnergyListRenderer display2;
    EnergyItemRenderer[] energyInfo2;

    public Program()
    {
      ctx1 = new EnergyRendererContext();
      ctx1.BarWidth = 10;
      ctx1.RowWidth = 66;
      display1 = new EnergyListRenderer(ctx1);

      ctx2 = new EnergyRendererContext();
      ctx2.BarWidth = 10;
      ctx2.RowWidth = 66;
      display2 = new EnergyListRenderer(ctx2);

      energyInfo1 = new EnergyItemRenderer[] {
            MyPowerProductionItemRenderer.Find(GridTerminalSystem, "BA1_reattore_1"),
            MyBatteryItemRenderer.Find(GridTerminalSystem, "BA1_Batteria_1"),
            MyBatteryItemRenderer.Find(GridTerminalSystem, "BA1_Batteria_2"),
            MyBatteryItemRenderer.Find(GridTerminalSystem, "BA1_Batteria_3"),
            MyBatteryItemRenderer.Find(GridTerminalSystem, "BA1_Batteria_4")
            };
      energyInfo2 = new EnergyItemRenderer[] {
            MyPowerProductionItemRenderer.Find(GridTerminalSystem, "BA1_pannello_solare_1"),
            MyPowerProductionItemRenderer.Find(GridTerminalSystem, "BA1_pannello_solare_2"),
            MyPowerProductionItemRenderer.Find(GridTerminalSystem, "BA1_pannello_solare_3"),
            MyPowerProductionItemRenderer.Find(GridTerminalSystem, "BA1_pannello_solare_4"),
            MyPowerProductionItemRenderer.Find(GridTerminalSystem, "BA1_pannello_solare_5"),
            MyPowerProductionItemRenderer.Find(GridTerminalSystem, "BA1_pannello_solare_6"),
            MyPowerProductionItemRenderer.Find(GridTerminalSystem, "BA1_pannello_solare_7")
            };
      Runtime.UpdateFrequency = UpdateFrequency.Update100;
    }

    public void Main(string argument, UpdateType updateSource)
    {
      System.Collections.Generic.List<IMyTextPanel> displays = new System.Collections.Generic.List<IMyTextPanel>();
      GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(displays, d => MyIni.HasSection(d.CustomData, "energy"));
      string report1 = display1.Render(energyInfo1);
      string report2 = display2.Render(energyInfo2);
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
