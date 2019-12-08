namespace IngameScript
{
  partial class Program
  {
    public abstract class ListDisplayItem<C> where C : DisplayContext
    {
      public abstract string Render(C ctx);
    }
  }
}
