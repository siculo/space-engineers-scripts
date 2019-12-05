namespace IngameScript
{
  partial class Program
  {
    public class DisplayObjects
    {
      private string _hr;
      private int _hrLength;

      public SpinningBar Bar { get; } = new SpinningBar();
      public System.Globalization.CultureInfo EnUS { get; } = new System.Globalization.CultureInfo("en-US");
      public string HR {
        get { return _hr; }
      }
      public int HRLenght {
        get { return _hrLength; }
        set { _hrLength = value; _hr = new string('-', _hrLength); }
      }
      public int BarWidth { get; set; } = 18;

      public DisplayObjects()
      {
        HRLenght = 33;
      }
    }
  }
}
