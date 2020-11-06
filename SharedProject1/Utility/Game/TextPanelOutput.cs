using Sandbox.ModAPI.Ingame;
using System.Collections.Generic;
using System.Linq;
using VRage;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
  partial class Program
  {
    public class ResourceDisplay
    {
      public static readonly string resources = "resources";

      private IMyTextPanel _panel;
      private IEnumerable<Container> _containers;
      private ResourceRendererContext _ctx;
      private ResourceListRenderer _renderer;
      private IniProps _ini;
      private string _tags;
      private string _allow;

      public ResourceDisplay(IMyTextPanel panel, IEnumerable<Container> containers)
      {
        _panel = panel;
        _containers = containers;
        _ctx = new ResourceRendererContext();
        _renderer = new ResourceListRenderer(_ctx);
        _ini = new IniProps(_panel.CustomData);
        _tags = _ini.GetString(resources, "tags");
        _allow = _ini.GetString(resources, "allow");
        _ctx.Name = _ini.GetString(resources, "name", "Resources");
        _ctx.RowWidth = _ini.GetInt(resources, "rowWidth", 32);
        _ctx.ResourceNameSpace = _ini.GetInt(resources, "nameSpace", 8);
        _ctx.ResourceTypeSpace = _ini.GetInt(resources, "typeSpace", 5);
        _ctx.AmountSpace = _ini.GetInt(resources, "amountSpace", 7);
        _ctx.AmountDecimalDigits = _ini.GetInt(resources, "decimalDigits", 0);
      }

      public void ShowSummary()
      {
        ResourceInventory inventory = ResourceInventory.ContainersInventory(_containers, Parsers.ParseTags(_tags), Parsers.ParseResourceFilter(_allow));
        _ctx.MaxAmount = (MyFixedPoint)inventory.GetMaximum();
        _panel.WriteText(_renderer.Render(inventory.GetResources().Select(resource => new ResourceItemRenderer(resource))));
      }
    }
  }
}
