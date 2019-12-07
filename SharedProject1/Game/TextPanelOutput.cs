using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  partial class Program
  {
    public class TextPanelOutput : TextOuput
    {
      private IMyTextPanel panel;

      public TextPanelOutput(IMyTextPanel p)
      {
        this.panel = p;
      }

      public void Set(string t)
      {
        panel.WriteText(t);
      }
    }
  }
}
