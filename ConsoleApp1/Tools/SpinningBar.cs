namespace IngameScript
{
  // --------------------------------
  public class SpinningBar
  {
    private int step = 0;
    private readonly string[] steps = new string[] { "-","\\","|","/" };

    public override string ToString() {
      return steps[step];
    }

    public SpinningBar Step() {
      step = (step + 1) % steps.Length;
      return this;
    }
  }
  // --------------------------------
}
