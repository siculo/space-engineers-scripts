using System;
using System.Collections.Generic;
namespace IngameScript
{
  public class ControlPanel
  {
    private Menu menu;

    private readonly int width, heigth;

    public int Width {
      get { return width; }
    }

    public int Heigth {
      get { return heigth; }
    }

    public ControlPanel(int width, int heigth) {
      this.width = width;
      this.heigth = heigth;
    }

    public void SetMenu(Menu menu) {
      this.menu = menu;
      menu.SetControlPanel(this);
    }

    public void SelectNext() {
      menu.SelectNext();
    }

    public void SelectPrevious() {
      menu.SelectPrevious();
    }

    public override string ToString() {
      return this.menu.GetContent();
    }

    public void ActivateCurrent() {
      menu.ActivateCurrent();
    }

    public void ActivateParent() {
      menu.ActivateParent();
    }
  }
}
