using Sandbox.ModAPI.Ingame;
using System.Collections.Generic;
using System.Linq;
using VRage.Game.ModAPI;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
  partial class Program : MyGridProgram
  {
    IEnumerable<ResourceDisplay> _resourceDisplays;

    public Program()
    {
      Runtime.UpdateFrequency = UpdateFrequency.Update100;
    }

    private void Initialize()
    {
      _resourceDisplays = InitResourceDisplays(InitContainers());
      Echo(string.Format("Resource display found: {0}", _resourceDisplays.Count()));
    }

    private IEnumerable<Container> InitContainers()
    {
      List<IMyTerminalBlock> containerBlocks = new List<IMyTerminalBlock>();
      GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(containerBlocks, b => (b.HasInventory && this.Me.CubeGrid == b.CubeGrid));
      return containerBlocks.Select(b => new MyContainer(b) as Container);
    }

    private IEnumerable<ResourceDisplay> InitResourceDisplays(IEnumerable<Container> containers)
    {
      List<IMyTextPanel> panels = new List<IMyTextPanel>();
      GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(panels, panel => MyIni.HasSection(panel.CustomData, ResourceDisplay.resources) && this.Me.CubeGrid == panel.CubeGrid);
      List<ResourceDisplay> resourceDisplays = new List<ResourceDisplay>();
      foreach (IMyTextPanel panel in panels)
      {
        resourceDisplays.Add(new ResourceDisplay(panel, containers));
      }
      return resourceDisplays;
    }

    public void Main(string argument, UpdateType updateSource)
    {
      Initialize();
      foreach (ResourceDisplay display in _resourceDisplays)
      {
        display.ShowSummary();
      }
      SpinningBar.Step();
    }
  }
}
