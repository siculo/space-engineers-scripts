namespace IngameScript
{
  public class Flag: MenuItem
  {
    protected bool on;

    public Flag(string name) : base(name) {
      on = false;
    }

    public Flag(string name, bool on) : base(name) {
      this.on = on;
    }

    override public string GetLabel() {
      return " " + this.name + " " + (on ? "[X]" : "[ ]");
    }

    override public void Activate() {
    }
  }
}
