using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  public abstract class GameProgram
  {
    protected IMyGridTerminalSystem GridTerminalSystem = null;
    protected IMyGridProgramRuntimeInfo Runtime = null;
    protected IMyProgrammableBlock Me = null;

    protected void Echo(string message) {

    }
  }
}
