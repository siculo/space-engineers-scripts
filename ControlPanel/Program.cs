using Sandbox.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
  partial class Program : MyGridProgram
  {
    ControlPanel panel;
    IMyTextSurface display;
    MyIni _ini = new MyIni();
    SpinningBar _bar = new SpinningBar();

    public Program()
    {
      MyIniParseResult result;
      if (!_ini.TryParse(Me.CustomData, out result))
      {
        Echo("c'è un problema: " + result.Success + " " + result.Error);
      }
      else
      {
        panel = Panel();
        panel.SetMenu(MainMenu());
        display = FindTextSurface();
        if (display != null)
        {
          display.WriteText(panel.ToString());
          Runtime.UpdateFrequency = UpdateFrequency.Update100;
        }
      }
    }

    private IMyTextSurface FindTextSurface()
    {
      string monitorName = _ini.Get("config", "monitor").ToString();
      IMyTextSurface display = null;
      if (monitorName != "")
      {
        display = GridTerminalSystem.GetBlockWithName(monitorName) as IMyTextSurface;
        if (display == null)
        {
          Echo(string.Format("Display '{0}' missing", monitorName));
        }
        else
        {
          Echo(string.Format("Display '{0}' found", monitorName));
        }
      }
      else
      {
        string cockpitName = _ini.Get("config", "cockpit").ToString();
        if (cockpitName != "")
        {
          int surfaceNumber = _ini.Get("config", "surface").ToInt32(0);
          IMyTextSurfaceProvider cockpit = GridTerminalSystem.GetBlockWithName(cockpitName) as IMyTextSurfaceProvider;
          if (cockpit != null)
          {
            display = cockpit.GetSurface(surfaceNumber);
          }
          if (cockpitName == null)
          {
            Echo(string.Format("Display '{0}' of '{1}' missing", surfaceNumber, cockpitName));
          }
          else
          {
            Echo(string.Format("Display '{0}' of '{1}' found", surfaceNumber, cockpitName));
          }
        }
      }
      return display;
    }

    private ControlPanel Panel()
    {
      int rows = _ini.Get("config", "rows").ToInt32(20);
      int columns = _ini.Get("config", "columns").ToInt32(15);
      return new ControlPanel(columns, rows);
    }

    private Menu MainMenu()
    {
      Menu menu = new Menu("Main");
      menu.AddItem(SwitchMenu());
      return menu;
    }

    private Menu SwitchMenu()
    {
      MyIni menuIni = new MyIni();
      Menu menu = new Menu("Sistemi Attivi");
      System.Collections.Generic.List<IMyFunctionalBlock> blocks = new System.Collections.Generic.List<IMyFunctionalBlock>();
      GridTerminalSystem.GetBlocksOfType<IMyFunctionalBlock>(blocks, b => MyIni.HasSection(b.CustomData, "switchable"));
      blocks.Sort(delegate (IMyFunctionalBlock b1, IMyFunctionalBlock b2) {
        return b1.CustomName.CompareTo(b2.CustomName);
      });
      foreach (IMyFunctionalBlock b in blocks)
      {
        MyIniParseResult br;
        if (menuIni.TryParse(b.CustomData, out br) && menuIni.Get("switchable", "spinning").ToBoolean())
        {
          menu.AddItem(new BlockSwitcher(b, _bar));
        }
        else
        {
          menu.AddItem(new BlockSwitcher(b));
        }
      }
      return menu;
    }

    public void Main(string argument)
    {
      if (argument == "next")
      {
        panel.SelectNext();
      }
      else if (argument == "prev")
      {
        panel.SelectPrevious();
      }
      else if (argument == "current")
      {
        panel.ActivateCurrent();
      }
      else if (argument == "parent")
      {
        panel.ActivateParent();
      }
      else
      {
        _bar.Step();
      }
      display.WriteText(panel.ToString());
    }
  }
}
