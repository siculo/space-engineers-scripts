using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;

namespace IngameScript
{
  partial class Program : MyGridProgram
  {
    ResourceDisplayContext ctx;
    ResourceDisplay display;
    List<IMyTextPanel> displays;
    List<IMyTerminalBlock> blocks;
    List<Container> containers;
    string groupName;

    public Program()
    {
      ctx = new ResourceDisplayContext();
      ctx.RowWidth = 32;
      ctx.ResourceNameSpace = 8;
      ctx.ResourceTypeSpace = 5;
      ctx.AmountSpace = 7;
      ctx.AmountDecimalDigits = 0;
      display = new ResourceDisplay(ctx);
      Runtime.UpdateFrequency = UpdateFrequency.Update100;
      Initialize();
    }

    private bool Initialize()
    {
      MyIni _ini = new MyIni();
      MyIniParseResult result;
      if (_ini.TryParse(Me.CustomData, out result))
      {
        groupName = _ini.Get("resource", "group").ToString();
        blocks = new List<IMyTerminalBlock>();
        GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(blocks, b => MyIni.HasSection(b.CustomData, groupName));
        containers = new List<Container>();
        foreach (IMyTerminalBlock block in blocks)
        {
          containers.Add(new MyContainer(block));
        }

        displays = new List<IMyTextPanel>();
        GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(displays, d => MyIni.HasSection(d.CustomData, "resource"));

        Echo(string.Format("resource display found: {0}", displays.Count));
        Echo(string.Format("blocks found for group {0}: {1}", groupName, blocks.Count));

        return true;
      }
      else
      {
        Echo("c'è un problema: " + result.Success + " " + result.Error);
        return false;
      }
    }

    public void Main(string argument, UpdateType updateSource)
    {
      Summary summary = Summary.ContainersSummary(containers);
      ctx.MaxAmount = (MyFixedPoint)summary.GetMaximum();
      string report = display.Show(summary.GetResources().Select(resource => new ResourceItem(resource)));
      foreach (IMyTextPanel d in displays)
      {
        d.WriteText(report);
      }
    }
  }
}
