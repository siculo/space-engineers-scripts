namespace IngameScript
{
  public class ControlPanel
  {
    private Menu _menu;

    private readonly int _width, _heigth;

    public int Width {
      get { return _width; }
    }

    public int Heigth {
      get { return _heigth; }
    }

    public ControlPanel(int width, int heigth) {
      _width = width;
      _heigth = heigth;
    }

    public void SetMenu(Menu menu) {
      _menu = menu;
      _menu.SetControlPanel(this);
    }

    public void SelectNext() {
      _menu.SelectNext();
    }

    public void SelectPrevious() {
      _menu.SelectPrevious();
    }

    public override string ToString() {
      return _menu.GetContent();
    }

    public void ActivateCurrent() {
      _menu.ActivateCurrent();
    }

    public void ActivateParent() {
      _menu.ActivateParent();
    }
  }
}
