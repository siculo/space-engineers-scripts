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
    ResourceRendererContext ctx;
    MyIni _ini;
    ResourceListRenderer display;
    List<IMyTextPanel> displays;
    List<IMyTerminalBlock> blocks;
    IEnumerable<Container> containers;
    string resources = "resources";

    public Program()
    {
      ctx = new ResourceRendererContext();
      ctx.RowWidth = 32;
      ctx.ResourceNameSpace = 8;
      ctx.ResourceTypeSpace = 5;
      ctx.AmountSpace = 7;
      ctx.AmountDecimalDigits = 0;
      display = new ResourceListRenderer(ctx);
      Runtime.UpdateFrequency = UpdateFrequency.Update100;
      Initialize();
    }

    private bool Initialize()
    {
      _ini = new MyIni();
      MyIniParseResult result;
      if (_ini.TryParse(Me.CustomData, out result))
      {
        InitializeContainers();
        InitializeDisplays();
        return true;
      }
      else
      {
        Echo("An error occurs: <" + result.Success + "> " + result.Error);
        return false;
      }
    }

    private void InitializeContainers()
    {
      blocks = new List<IMyTerminalBlock>();
      GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(blocks, b => b.HasInventory);
      containers = blocks.Select(b => new MyContainer(b) as Container);
    }

    private void InitializeDisplays()
    {
      displays = new List<IMyTextPanel>();
      GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(displays, d => MyIni.HasSection(d.CustomData, resources));
      Echo(string.Format("resource display found: {0}",  displays.Count));
    }

    public void Main(string argument, UpdateType updateSource)
    {
      foreach (IMyTextPanel d in displays)
      {
        MyIni displayIni = new MyIni();
        MyIniParseResult result;
        if (displayIni.TryParse(d.CustomData, out result))
        {
          String tags = displayIni.Get(resources, "tags").ToString();
          String allow = displayIni.Get(resources, "allow").ToString();
          Summary summary = Summary.ContainersSummary(containers, Parsers.ParseTags(tags), Parsers.ParseResourceFilter(allow));
          ctx.MaxAmount = (MyFixedPoint)summary.GetMaximum();
          string report = display.Render(summary.GetResources().Select(resource => new ResourceItemRenderer(resource)));
          d.WriteText(report);
        }
        else
        {
          Echo("An error occurs: <" + result.Success + "> " + result.Error);
        }
      }
    }
  }
}
