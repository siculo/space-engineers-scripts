using System;
using VRageMath;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;

namespace IngameScript
{
  // --------------------------------
  public class Airlock
  {
    private IMyDoor inDoor, outDoor;
    private IMyLightingBlock light;
    private IMyAirVent vent;
    private IMyTextPanel inDisplay, outDisplay;

    private static readonly Color depressurizedColor = new Color(255,0,0);
    private static readonly Color pressurizedColor = new Color(0,255,0);

    private static readonly double PRESSURIZATION_UPPER_LIMIT = 0.99;
    private static readonly double PRESSURIZATION_LOWER_LIMIT = 0.01;

    private static readonly TimeSpan DOOR_CHANGE_TIME = TimeSpan.FromSeconds(2);
    private static readonly TimeSpan MAX_OPEN_TIME = TimeSpan.FromSeconds(4);

    private static readonly string S_IDLE = "";

    private static readonly string S_ERR = "error";
    private static readonly string S_SETUP = "setup";
    private static readonly string S_FINISHING_SETUP = "finishing_setup";

    private static readonly string S_EXIT_START = "exit_start";
    private static readonly string S_EXIT_PREPARING = "exit_preparing";
    private static readonly string S_EXIT_FIRST_DOOR_OPEN = "exit_first_door_open";
    private static readonly string S_EXIT_FIRST_DOOR_CLOSING = "exit_first_door_closing";
    private static readonly string S_EXIT_START_CHANGING_PRESSURE = "exit_start_changing_pressure";
    private static readonly string S_EXIT_CHANGING_PRESSURE = "exit_changing_pressure";
    private static readonly string S_EXIT_SECOND_DOOR_OPEN = "exit_second_door_open";
    private static readonly string S_EXIT_SECOND_DOOR_CLOSING = "exit_second_door_closing";

    private static readonly string S_ENTER_START = "enter_start";
    private static readonly string S_ENTER_PREPARING = "enter_preparing";
    private static readonly string S_ENTER_FIRST_DOOR_OPEN = "enter_first_door_open";
    private static readonly string S_ENTER_FIRST_DOOR_CLOSING = "enter_first_door_closing";
    private static readonly string S_ENTER_START_CHANGING_PRESSURE = "enter_start_changing_pressure";
    private static readonly string S_ENTER_CHANGING_PRESSURE = "enter_changing_pressure";
    private static readonly string S_ENTER_SECOND_DOOR_OPEN = "enter_second_door_open";
    private static readonly string S_ENTER_SECOND_DOOR_CLOSING = "enter_second_door_closing";

    private TimeSpan inDoorTime, outDoorTime, setupTime;

    private string _name;
    public string Name { get { return _name; } }

    private string statusText = "";
    private bool enabled = false;

    public Airlock(IMyGridTerminalSystem system,string name) {
      _name = name;
      inDoor = system.GetBlockWithName(name + "_Door_In") as IMyDoor;
      outDoor = system.GetBlockWithName(name + "_Door_Out") as IMyDoor;
      light = system.GetBlockWithName(name + "_Light") as IMyLightingBlock;
      vent = system.GetBlockWithName(name + "_Vent") as IMyAirVent;
      inDisplay = system.GetBlockWithName(name + "_Display_In") as IMyTextPanel;
      outDisplay = system.GetBlockWithName(name + "_Display_Out") as IMyTextPanel;
      if(inDoor == null || outDoor == null) {
        setStatus(S_ERR);
      } else {
        setStatus(S_SETUP);
        enabled = true;
      }
    }

    public void startExit() {
      if(enabled && vent.CustomData == S_IDLE) {
        setStatus(S_EXIT_START);
      }
    }

    public void startEnter() {
      if(enabled && vent.CustomData == S_IDLE) {
        setStatus(S_ENTER_START);
      }
    }

    public void update(TimeSpan elapsedTime) {
      bool pressurized = vent.GetOxygenLevel() >= PRESSURIZATION_UPPER_LIMIT;
      if (light != null) {
        light.SetValue("Color", pressurized ? pressurizedColor : depressurizedColor);
      }
      string status = vent.CustomData;
      if(status == S_SETUP) {
        setup();
      } else if(status == S_FINISHING_SETUP) {
        finishingSetup(elapsedTime);
      } else if(status == S_EXIT_START) {
        exitStart();
      } else if(status == S_ENTER_START) {
        enterStart();
      } else if(status == S_EXIT_PREPARING) {
        exitPreparing();
      } else if(status == S_ENTER_PREPARING) {
        enterPreparing();
      } else if(status == S_EXIT_FIRST_DOOR_OPEN) {
        exitFirstDoorOpen(elapsedTime);
      } else if(status == S_ENTER_FIRST_DOOR_OPEN) {
        enterFirstDoorOpen(elapsedTime);
      } else if(status == S_EXIT_FIRST_DOOR_CLOSING) {
        exitFirstDoorClosing(elapsedTime);
      } else if(status == S_ENTER_FIRST_DOOR_CLOSING) {
        enterFirstDoorClosing(elapsedTime);
      } else if(status == S_EXIT_START_CHANGING_PRESSURE) {
        exitStartChangingPressure();
      } else if(status == S_ENTER_START_CHANGING_PRESSURE) {
        enterStartChangingPressure();
      } else if(status == S_EXIT_CHANGING_PRESSURE) {
        exitChangingPressure();
      } else if(status == S_ENTER_CHANGING_PRESSURE) {
        enterChangingPressure();
      } else if(status == S_EXIT_SECOND_DOOR_OPEN) {
        exitSecondDoorOpen(elapsedTime);
      } else if(status == S_ENTER_SECOND_DOOR_OPEN) {
        enterSecondDoorOpen(elapsedTime);
      } else if(status == S_EXIT_SECOND_DOOR_CLOSING) {
        exitClosingSecondDoor(elapsedTime);
      } else if(status == S_ENTER_SECOND_DOOR_CLOSING) {
        enterClosingSecondDoor(elapsedTime);
      } else if(status != S_ERR) {
        idle();
      }
    }

    private void setup() {
      if(inDoor.Status == DoorStatus.Closed && outDoor.Status == DoorStatus.Closed) {
        Action(inDoor,"OnOff_Off");
        Action(outDoor,"OnOff_Off");
        setStatus(S_IDLE);
      } else {
        Action(inDoor,"OnOff_On");
        Action(outDoor,"OnOff_On");
        Action(inDoor,"Open_Off");
        Action(outDoor,"Open_Off");
        setupTime = new TimeSpan(0);
        setStatus(S_FINISHING_SETUP);
      }
    }

    private void finishingSetup(TimeSpan elapsedTime) {
      setupTime = setupTime.Add(elapsedTime);
      if(setupTime >= DOOR_CHANGE_TIME) {
        Action(inDoor,"OnOff_Off");
        Action(outDoor,"OnOff_Off");
        setStatus(S_IDLE);
      }
    }

    private void exitStart() {
      if(vent.GetOxygenLevel() < PRESSURIZATION_UPPER_LIMIT) {
        vent.SetValue<bool>("Depressurize",false);
        setStatus(S_EXIT_PREPARING);
      } else {
        Action(inDoor,"OnOff_On");
        inDoor.SetValue<bool>("Open",true);
        inDoorTime = new TimeSpan(0);
        setStatus(S_EXIT_FIRST_DOOR_OPEN);
      }
    }

    private void enterStart() {
      if(vent.GetOxygenLevel() > PRESSURIZATION_LOWER_LIMIT) {
        vent.SetValue<bool>("Depressurize",true);
        setStatus(S_ENTER_PREPARING);
      } else {
        Action(outDoor,"OnOff_On");
        outDoor.SetValue<bool>("Open",true);
        outDoorTime = new TimeSpan(0);
        setStatus(S_ENTER_FIRST_DOOR_OPEN);
      }
    }

    private void exitPreparing() {
      if(vent.GetOxygenLevel() >= PRESSURIZATION_UPPER_LIMIT) {
        Action(inDoor,"OnOff_On");
        inDoor.SetValue<bool>("Open",true);
        setStatus(S_EXIT_FIRST_DOOR_OPEN);
        inDoorTime = new TimeSpan(0);
      }
    }

    private void enterPreparing() {
      if(vent.GetOxygenLevel() <= PRESSURIZATION_LOWER_LIMIT) {
        Action(outDoor,"OnOff_On");
        outDoor.SetValue<bool>("Open",true);
        setStatus(S_ENTER_FIRST_DOOR_OPEN);
        outDoorTime = new TimeSpan(0);
      }
    }

    private void exitFirstDoorOpen(TimeSpan elapsedTime) {
      inDoorTime = inDoorTime.Add(elapsedTime);
      if(inDoorTime >= MAX_OPEN_TIME) {
        inDoor.SetValue<bool>("Open",false);
        inDoorTime = new TimeSpan(0);
        setStatus(S_EXIT_FIRST_DOOR_CLOSING);
      }
    }

    private void enterFirstDoorOpen(TimeSpan elapsedTime) {
      outDoorTime = outDoorTime.Add(elapsedTime);
      if(outDoorTime >= MAX_OPEN_TIME) {
        outDoor.SetValue<bool>("Open",false);
        outDoorTime = new TimeSpan(0);
        setStatus(S_ENTER_FIRST_DOOR_CLOSING);
      }
    }

    private void exitFirstDoorClosing(TimeSpan elapsedTime) {
      inDoorTime = inDoorTime.Add(elapsedTime);
      if(inDoorTime >= DOOR_CHANGE_TIME) {
        Action(inDoor,"OnOff_Off");
        setStatus(S_EXIT_START_CHANGING_PRESSURE);
      }
    }

    private void enterFirstDoorClosing(TimeSpan elapsedTime) {
      outDoorTime = outDoorTime.Add(elapsedTime);
      if(outDoorTime >= DOOR_CHANGE_TIME) {
        Action(outDoor,"OnOff_Off");
        setStatus(S_ENTER_START_CHANGING_PRESSURE);
      }
    }

    private void exitStartChangingPressure() {
      vent.SetValue<bool>("Depressurize",true);
      setStatus(S_EXIT_CHANGING_PRESSURE);
    }

    private void enterStartChangingPressure() {
      vent.SetValue<bool>("Depressurize",false);
      setStatus(S_ENTER_CHANGING_PRESSURE);
    }

    private void exitChangingPressure() {
      if(vent.GetOxygenLevel() <= PRESSURIZATION_LOWER_LIMIT) {
        Action(inDoor,"OnOff_Off");
        Action(outDoor,"OnOff_On");
        outDoor.SetValue<bool>("Open",true);
        setStatus(S_EXIT_SECOND_DOOR_OPEN);
        outDoorTime = new TimeSpan(0);
      }
    }

    private void enterChangingPressure() {
      if(vent.GetOxygenLevel() >= PRESSURIZATION_UPPER_LIMIT) {
        Action(outDoor,"OnOff_Off");
        Action(inDoor,"OnOff_On");
        inDoor.SetValue<bool>("Open",true);
        setStatus(S_ENTER_SECOND_DOOR_OPEN);
        inDoorTime = new TimeSpan(0);
      }
    }

    private void exitSecondDoorOpen(TimeSpan t) {
      outDoorTime = outDoorTime.Add(t);
      if(outDoorTime >= MAX_OPEN_TIME) {
        outDoor.SetValue<bool>("Open",false);
        outDoorTime = new TimeSpan(0);
        setStatus(S_EXIT_SECOND_DOOR_CLOSING);
      }
    }

    private void enterSecondDoorOpen(TimeSpan t) {
      inDoorTime = inDoorTime.Add(t);
      if(inDoorTime >= MAX_OPEN_TIME) {
        inDoor.SetValue<bool>("Open",false);
        inDoorTime = new TimeSpan(0);
        setStatus(S_ENTER_SECOND_DOOR_CLOSING);
      }
    }

    private void exitClosingSecondDoor(TimeSpan t) {
      inDoorTime = inDoorTime.Add(t);
      if(inDoorTime >= DOOR_CHANGE_TIME) {
        Action(outDoor,"OnOff_Off");
        setStatus(S_IDLE);
      }
    }

    private void enterClosingSecondDoor(TimeSpan t) {
      outDoorTime = outDoorTime.Add(t);
      if(outDoorTime >= DOOR_CHANGE_TIME) {
        Action(inDoor,"OnOff_Off");
        setStatus(S_IDLE);
      }
    }

    private void idle() {
    }

    private void Action(IMyTerminalBlock block,string actionName) {
      var action = block.GetActionWithName(actionName);
      action.Apply(block);
    }

    private void setStatus(string newStatus) {
      vent.CustomData = newStatus;
      statusText = getTextForStatus(newStatus);
      if(inDisplay != null) {
        inDisplay.WriteText(statusText);
      }
      if(outDisplay != null) {
        outDisplay.WriteText(statusText);
      }
    }

    public string description() {
      return " " + _name + "\n     Status: " + statusText + "\n   Pressure: " + Math.Floor(vent.GetOxygenLevel() * 100) + "%";
    }

    private string getTextForStatus(string status) {
      if(status == S_ERR) {
        return "Error! " + this.ToString();
      } else if(status == S_SETUP) {
        return "Setup...";
      } else if(status == S_FINISHING_SETUP) {
        return "Starting...";
      } else if(status == S_EXIT_START) {
        return "Starting exit process...";
      } else if(status == S_ENTER_START) {
        return "Starting enter process...";
      } else if(status == S_EXIT_PREPARING) {
        return "Pressurizing...";
      } else if(status == S_ENTER_PREPARING) {
        return "Depressurizing...";
      } else if(status == S_EXIT_FIRST_DOOR_OPEN) {
        return "Inner door open";
      } else if(status == S_ENTER_FIRST_DOOR_OPEN) {
        return "Outer door open";
      } else if(status == S_EXIT_FIRST_DOOR_CLOSING) {
        return "Closing inner door...";
      } else if(status == S_ENTER_FIRST_DOOR_CLOSING) {
        return "Closing outer door...";
      } else if(status == S_EXIT_START_CHANGING_PRESSURE) {
        return "Ready to depressurize";
      } else if(status == S_ENTER_START_CHANGING_PRESSURE) {
        return "Ready to pressurize";
      } else if(status == S_EXIT_CHANGING_PRESSURE) {
        return "Depressurizing...";
      } else if(status == S_ENTER_CHANGING_PRESSURE) {
        return "Pressurizing...";
      } else if(status == S_EXIT_SECOND_DOOR_OPEN) {
        return "Outer door open";
      } else if(status == S_ENTER_SECOND_DOOR_OPEN) {
        return "Inner door open";
      } else if(status == S_EXIT_SECOND_DOOR_CLOSING) {
        return "Closing outer door...";
      } else if(status == S_ENTER_SECOND_DOOR_CLOSING) {
        return "Closing inner door...";
      } else {
        return "Ready";
      }
    }

    public override string ToString() {
      string descr = _name;
      string notFound = "";
      if (inDoor == null) { notFound = notFound + "Door_In "; }
      if (outDoor == null) { notFound = notFound + "Door_Out "; }
      if (light == null) { notFound = notFound + "Light "; }
      if (inDisplay == null) { notFound = notFound + "Display_In "; }
      if (outDisplay == null) { notFound = notFound + "Display_Out "; }
      string name = _name;
      if(!enabled) {
        name = name + " (disabled)";
      }
      if (notFound != "") {
        return name + " - not found components: " + notFound;
      }
      return name;
    }
  }
  // --------------------------------
}