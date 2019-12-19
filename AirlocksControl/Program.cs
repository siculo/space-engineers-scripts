using Sandbox.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
  partial class Program : MyGridProgram
  {
    System.Collections.Generic.Dictionary<string, Airlock> airlocks =
        new System.Collections.Generic.Dictionary<string, Airlock>();
    MyIni _ini = new MyIni();
    string gridPrefix = "";

    public Program()
    {
      MyIniParseResult result;
      if (!_ini.TryParse(Me.CustomData, out result))
      {
        Echo("c'è un problema: " + result.Success + " " + result.Error);
      }
      else
      {
        gridPrefix = _ini.Get("airlock", "gridPrefix").ToString();
        Echo(string.Format("Grid prefix: {0}", gridPrefix != "" ? gridPrefix : "<none>"));
        if (gridPrefix != "")
        {
          gridPrefix = gridPrefix + "_";
        }
        System.Collections.Generic.List<IMyAirVent> vents = new System.Collections.Generic.List<IMyAirVent>();
        GridTerminalSystem.GetBlocksOfType<IMyAirVent>(vents, v => v.CustomName.StartsWith(gridPrefix) && v.CustomName.EndsWith("_Vent"));
        Echo(string.Format("Airlocks found: {0}", vents.Count));
        foreach (IMyAirVent v in vents)
        {
          string name = v.CustomName.Substring(0, v.CustomName.Length - 5);
          Airlock airlock = new Airlock(GridTerminalSystem, name);
          airlocks.Add(name, airlock);
          Echo(string.Format("- {0}", airlock.ToString()));
        }
        Runtime.UpdateFrequency = UpdateFrequency.Update100;
      }
    }

    public void Save()
    {
    }

    public void Main(string argument, UpdateType updateSource)
    {
      string[] runParams = argument.Split(' ');
      bool doIdle = true;
      if (runParams.Length >= 2)
      {
        string command = runParams[0];
        string name = runParams[1];
        Airlock airlock = findAirlock(name);
        if (airlock != null)
        {
          if (command == "enter")
          {
            airlock.startEnter();
            doIdle = false;
          }
          else if (command == "exit")
          {
            airlock.startExit();
            doIdle = false;
          }
        }
      }
      if (doIdle)
      {
        foreach (var item in airlocks)
        {
          item.Value.update(Runtime.TimeSinceLastRun);
        }
      }
      updateAirlockDisplay();
      SpinningBar.Step();
    }

    private Airlock findAirlock(string name)
    {
      if (airlocks.ContainsKey(name))
      {
        return airlocks[name];
      }
      else
      {
        if (gridPrefix != "" && airlocks.ContainsKey(gridPrefix + name))
        {
          return airlocks[gridPrefix + name];
        }
        else
        {
          return null;
        }
      }
    }

    private void updateAirlockDisplay()
    {
      System.Text.StringBuilder sb = new System.Text.StringBuilder();
      foreach (var item in airlocks)
      {
        sb.Append("\n\n" + item.Value.description());
      }
      string displayText = "[Airlock " + SpinningBar.Render() + "]\n---------------------------------------" + sb.ToString();
      System.Collections.Generic.List<IMyTextPanel> displays = new System.Collections.Generic.List<IMyTextPanel>();
      GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(displays, d =>
      {
        MyIni displayIni;
        if (MyIni.HasSection(d.CustomData, "airlock"))
        {
          displayIni = new MyIni();
          string prefix = "";
          if (displayIni.TryParse(d.CustomData))
          {
            prefix = displayIni.Get("airlock", "gridPrefix").ToString();
            if (prefix != "")
            {
              prefix = prefix + "_";
            }
            return gridPrefix == prefix;
          }
        }
        return false;
      });
      foreach (IMyTextPanel d in displays)
      {
        d.WriteText(displayText);
      }
      SpinningBar.Step();
    }
  }
}
