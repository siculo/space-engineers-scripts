namespace IngameScript
{
  partial class Program
  {
    public class MenuItem
    {
      protected string name;

      public Menu Parent { get; set; }

      public MenuItem(string name)
      {
        this.name = name;
      }

      public virtual string GetLabel()
      {
        return " " + name + "  ";
      }

      public virtual void Activate()
      {
      }
    }

  }
}
