using System;

namespace IngameScript
{
  partial class Program
  {
    public abstract class EnergyItemRenderer : ListItemRenderer<EnergyRendererContext>
    {
      public abstract string Name { get; }
      public abstract bool Enabled { get; }

      protected string RenderHeader(EnergyRendererContext ctx)
      {
        string header = Name + " " + (Enabled ? "[" + SpinningBar.Render() + "]" : "OFF");
        return header;
      }
    }
  }
}
