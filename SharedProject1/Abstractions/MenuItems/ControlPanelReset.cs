using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
  partial class Program
  {

    public class ControlPanelReset : MenuItem
    {

      private ControlPanel _controlPanel;
      private Func<Menu> _menuFactory;

      public ControlPanelReset(ControlPanel controlPanel, Func<Menu> menuFactory) : base("Refresh Menu")
      {
        _controlPanel = controlPanel;
        _menuFactory = menuFactory;
      }

      override public void Activate()
      {
        _controlPanel.SetMenu(_menuFactory());
      }
    }
  }
}
