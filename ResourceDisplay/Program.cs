using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
  partial class Program : MyGridProgram
  {
    ResourceDisplay display = new ResourceDisplay(33, 10);
    ListDisplayItem[] resourceInfo;

    public Program()
    {
      resourceInfo = new ListDisplayItem[] { };
      Runtime.UpdateFrequency = UpdateFrequency.Update100;
    }

    public void Main(string argument, UpdateType updateSource)
    {
      System.Collections.Generic.List<IMyTextPanel> displays = new System.Collections.Generic.List<IMyTextPanel>();
      GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(displays, d => MyIni.HasSection(d.CustomData, "resource"));

      Echo(string.Format("resource display found: {0}", displays.Count));

      string report = display.Show(resourceInfo);
      MyIni menuIni = new MyIni();
      MyIniParseResult br;
      foreach (IMyTextPanel d in displays)
      {
        if (menuIni.TryParse(d.CustomData, out br))
        {
          d.WriteText(report);
        }
      }
    }
  }
}
