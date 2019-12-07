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

    public Program()
    {
      Runtime.UpdateFrequency = UpdateFrequency.Update100;
    }

    public void Main(string argument, UpdateType updateSource)
    {
      MyIni _ini = new MyIni();
      MyIniParseResult result;
      if (!_ini.TryParse(Me.CustomData, out result))
      {
        Echo("c'è un problema: " + result.Success + " " + result.Error);
      }
      else
      {
        List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
        List<IMyTextPanel> displays = new List<IMyTextPanel>();
        string blockName = _ini.Get("entity", "name").ToString();
        GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(displays, d => MyIni.HasSection(d.CustomData, "resource"));
        GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(blocks, v => v.CustomName.Equals(blockName));
        Echo(blockName);
        if (blocks.Count > 0)
        {
          IMyTerminalBlock block = blocks[0];
          if (block.HasInventory)
          {
            List<MyInventoryItem> items = new List<MyInventoryItem>();
            IMyInventory inventory = block.GetInventory();
            inventory.GetItems(items);
            StringBuilder allDescriptions = new StringBuilder();
            foreach (MyInventoryItem item in items)
            {
              allDescriptions.AppendLine(item.Type.TypeId + "," + item.Type.SubtypeId + ":" + item.Amount.ToString());
            }
            foreach (IMyTextPanel display in displays)
            {
              display.WriteText(allDescriptions);
            }
          }
          else
          {
            Echo("Il blocco \"" + blockName + "\" non ha inventario");
          }
        }
        else
        {
          Echo("Nessun blocco \"" + blockName + "\" trovato");
        }
      }
    }
  }
}
