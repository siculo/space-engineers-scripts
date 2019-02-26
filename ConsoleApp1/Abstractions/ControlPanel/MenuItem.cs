namespace IngameScript
{
  public class MenuItem
  {
    protected string name;

    public MenuItem(string name) {
      this.name = name;
    }

    public virtual string GetLabel() {
      return " " + this.name + "  ";
    }

    public virtual void Activate() {
    }
  }
}