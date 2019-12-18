namespace IngameScript
{
  partial class Program
  {
    public abstract class ListItemRenderer<C> where C : RendererContext
    {
      public abstract string Render(C ctx);
    }
  }
}
