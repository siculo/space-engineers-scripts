using Sandbox.ModAPI.Ingame;

namespace IngameScript
{
  // --------------------------------
  public class TextPanelOutput:TextOuput
  {
    private IMyTextPanel panel;

    public TextPanelOutput(IMyTextPanel p) {
      this.panel = p;
    }

    public void Set(string t) {
      panel.WritePublicText(t);
    }
  }
  // --------------------------------
}
