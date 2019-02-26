namespace IngameScript
{
  public class Flag: MenuItem
  {
    private bool on;

    public Flag(string name) : base(name) {
      this.on = false;
    }

    public Flag(string name, bool on) : base(name) {
      this.on = on;
    }

    override public string GetLabel() {
      return " " + this.name + " " + (on ? "[X]" : "[ ]");
    }

    override public void Activate() {
      on = !on;
    }
  }
}