namespace IngameScript
{
  partial class Program
  {
    public static class SpinningBar
    {
      private static int _step = 0;
      private static readonly string[] _steps = new string[] { "-", "\\", "|", "/" };

      public static string Render()
      {
        return _steps[_step];
      }

      public static void Step()
      {
        _step = (_step + 1) % _steps.Length;
      }
    }
  }
}
