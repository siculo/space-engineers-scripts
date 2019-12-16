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
  partial class Program
  {
    class MyContainer : Container
    {
      private IMyTerminalBlock _block;
      public MyContainer(IMyTerminalBlock block) : base(TagsFromConfig(block))
      {
        _block = block;
      }

      private static string TagsFromConfig(IMyTerminalBlock block)
      {
        MyIni _ini = new MyIni();
        MyIniParseResult result;
        if (_ini.TryParse(block.CustomData, out result))
        {
          return _ini.Get("main", "tags").ToString();
        }
        return null;
      }

      override public IEnumerable<Resource> GetResources()
      {
        if (_block.HasInventory)
        {
          List<MyInventoryItem> items = new List<MyInventoryItem>();
          _block.GetInventory().GetItems(items);
          return items.Select(item => ConvertToResource(item)).Where(resource => resource != null);
        }
        return new List<Resource>();
      }

      private Resource ConvertToResource(MyInventoryItem item)
      {
        ResourceType resourceType = new ResourceType(item.Type.TypeId, item.Type.SubtypeId);
        //if (resourceType.TypeName != "Ore")
        //  return null;
        return new Resource(resourceType, item.Amount);
      }
    }
  }
}
