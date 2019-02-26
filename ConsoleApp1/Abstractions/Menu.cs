namespace IngameScript
{
  public class Menu: MenuItem
  {
    private System.Collections.Generic.List<MenuItem> _items =
      new System.Collections.Generic.List<MenuItem>();

    private MenuItem _selectedItem = null;
    private int selectedCommandIndex = 0;
    private int topVisibleCommand = 0;

    private ControlPanel panel = null;

    public Menu(string name) : base(name) {
    }

    public void AddItem(MenuItem item) {
      _items.Add(item);
      if(_selectedItem == null) {
        _selectedItem = item;
      }
      item.Parent = this;
    }

    public void SetControlPanel(ControlPanel p) {
      this.panel = p;
    }

    public ControlPanel GetControlPanel() {
      return Parent != null ? Parent.GetControlPanel() : panel;
    }

    public void SelectNext() {
      selectedCommandIndex++;
      if(selectedCommandIndex >= _items.Count) {
        selectedCommandIndex = 0;
      }
      _selectedItem = _items[selectedCommandIndex];
      if(selectedCommandIndex >= topVisibleCommand + panel.Heigth) {
        topVisibleCommand++;
      } else if(selectedCommandIndex < topVisibleCommand) {
        topVisibleCommand = selectedCommandIndex;
      }
    }

    public void SelectPrevious() {
      selectedCommandIndex--;
      if(selectedCommandIndex < 0) {
        selectedCommandIndex = _items.Count - 1;
      }
      _selectedItem = _items[selectedCommandIndex];
      if(selectedCommandIndex < topVisibleCommand) {
        topVisibleCommand--;
      } else if(selectedCommandIndex >= topVisibleCommand + panel.Heigth) {
        topVisibleCommand = selectedCommandIndex - panel.Heigth + 1;
      }
    }

    public void ActivateCurrent() {
      _items[selectedCommandIndex].Activate();
    }

    override public void Activate() {
      GetControlPanel().SetMenu(this);
    }

    public void ActivateParent() {
      if(Parent != null) {
        GetControlPanel().SetMenu(Parent);
      }
    }

    public override string GetLabel() {
      return " " + this.name + " >";
    }

    public string GetContent() {
      System.Text.StringBuilder sb = new System.Text.StringBuilder();
      sb.Append(GetHeader());
      sb.Append(GetRows());
      sb.Append(GetFooter());
      return sb.ToString();
    }

    private string GetHeader() {
      string row = new string('-', this.panel.Width);
      if(topVisibleCommand > 0) {
        row = row.Remove(2, 2).Insert(2, "/\\");
      }
      System.Text.StringBuilder sb =
          new System.Text.StringBuilder();
      sb.Append("[" + GetPath() + "]\n");
      sb.Append(row);
      sb.Append("\n");
      return sb.ToString();
    }

    private string GetPath() {
      if(Parent != null) {
        return Parent.GetPath() + "/" + this.name;
      } else {
        return this.name;
      }
    }

    private string GetFooter() {
      string row = new string('-', this.panel.Width);
      if(topVisibleCommand + panel.Heigth < _items.Count) {
        row = row.Remove(2, 2).Insert(2, "\\/");
      }
      return row;
    }

    private string GetRows() {
      System.Collections.Generic.List<string> rows =
          new System.Collections.Generic.List<string>();
      long bottomLimit = topVisibleCommand + panel.Heigth;
      for(int i = topVisibleCommand; i < bottomLimit; i++) {
        if(i < _items.Count) {
          rows.Add(formatCommand(_items[i]));
        } else {
          rows.Add("\n");
        }
      }
      return string.Join("\n", rows.ToArray()) + "\n";
    }

    private string formatCommand(MenuItem command) {
      if(command == _selectedItem) {
        return "*" + command.GetLabel();
      } else {
        return " " + command.GetLabel();
      }
    }
  }
}