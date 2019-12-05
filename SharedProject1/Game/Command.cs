using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  partial class Program
  {
    public class Command : MenuItem
    {
      private IMyProgrammableBlock target;
      private string param;

      public Command(string name, IMyProgrammableBlock target, string param) : base(name)
      {
        this.target = target;
        this.param = param;
      }

      override public void Activate()
      {
        if (target.TryRun(param))
        {
        }
      }
    }
  }
}
