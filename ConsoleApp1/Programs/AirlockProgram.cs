using Sandbox.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript.AirlockProgram
{
  public class Program:GameProgram
  {
    // --------------------------------
    System.Collections.Generic.Dictionary<string,Airlock> airlocks =
        new System.Collections.Generic.Dictionary<string,Airlock>();
    SpinningBar spin = new SpinningBar();
    MyIni _ini = new MyIni();
    public Program()
    {
      MyIniParseResult result;
      if(!_ini.TryParse(Me.CustomData,out result)) {
        Echo("c'è un problema: " + result.Success + " " + result.Error);
      } else {
        System.Collections.Generic.List<IMyAirVent> vents = new System.Collections.Generic.List<IMyAirVent>();
        GridTerminalSystem.GetBlocksOfType<IMyAirVent>(vents,v => v.CustomName.EndsWith("_Vent"));
        foreach(IMyAirVent v in vents) {
          string name = v.CustomName.Substring(0,v.CustomName.Length - 5);
          airlocks.Add(name,new Airlock(GridTerminalSystem,name));
          Echo(string.Format("Airlock: {0}",name));
        }
        Runtime.UpdateFrequency = UpdateFrequency.Update100;
      }
    }

    public void Save()
    {
    }

    public void Main(string argument,UpdateType updateSource)
    {
      string[] runParams = argument.Split(' ');
      bool doIdle = true;
      if(runParams.Length >= 2) {
        string command = runParams[0];
        string name = runParams[1];
        if(airlocks.ContainsKey(name)) {
          if(command == "enter") {
            airlocks[name].startEnter();
            doIdle = false;
          } else if(command == "exit") {
            airlocks[name].startExit();
            doIdle = false;
          }
        }
      }
      if(doIdle) {
        foreach(var item in airlocks) {
          item.Value.update(Runtime.TimeSinceLastRun);
        }
      }
      updateAirlockDisplay();
    }

    private void updateAirlockDisplay()
    {
      System.Text.StringBuilder sb = new System.Text.StringBuilder();
      foreach(var item in airlocks) {
        sb.Append("\n\n" + item.Value.description());
      }
      string displayText = "[ Airlock " + spin + " ]\n---------------------------------------" + sb.ToString();
      System.Collections.Generic.List<IMyTextPanel> displays = new System.Collections.Generic.List<IMyTextPanel>();
      GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(displays,d => MyIni.HasSection(d.CustomData,"airlock"));
      foreach(IMyTextPanel d in displays) {
        d.WritePublicText(displayText);
      }
      spin.Step();
    }
    // --------------------------------
  }
}