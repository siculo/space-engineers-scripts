namespace IngameScript
{
  public class Menu: MenuItem
  {
    private System.Collections.Generic.List<MenuItem> commands =
        new System.Collections.Generic.List<MenuItem>();

    private MenuItem selectedCommand = null;
    private int selectedCommandIndex = 0;
    private int topVisibleCommand = 0;

    private readonly Menu parent;

    private ControlPanel panel = null;

    public Menu(string name, Menu parent) : base(name) {
      this.parent = parent;
    }

    public Menu(string name) : this(name, null) {
    }

    public void AddCommand(MenuItem command) {
      commands.Add(command);
      if(selectedCommand == null) {
        selectedCommand = command;
      }
    }

    public void SetControlPanel(ControlPanel p) {
      this.panel = p;
    }

    public ControlPanel GetControlPanel() {
      return parent != null ? parent.GetControlPanel() : panel;
    }

    public void SelectNext() {
      selectedCommandIndex++;
      if(selectedCommandIndex >= commands.Count) {
        selectedCommandIndex = 0;
      }
      selectedCommand = commands[selectedCommandIndex];
      if(selectedCommandIndex >= topVisibleCommand + panel.Heigth) {
        topVisibleCommand++;
      } else if(selectedCommandIndex < topVisibleCommand) {
        topVisibleCommand = selectedCommandIndex;
      }
    }

    public void SelectPrevious() {
      selectedCommandIndex--;
      if(selectedCommandIndex < 0) {
        selectedCommandIndex = commands.Count - 1;
      }
      selectedCommand = commands[selectedCommandIndex];
      if(selectedCommandIndex < topVisibleCommand) {
        topVisibleCommand--;
      } else if(selectedCommandIndex >= topVisibleCommand + panel.Heigth) {
        topVisibleCommand = selectedCommandIndex - panel.Heigth + 1;
      }
    }

    public void ActivateCurrent() {
      commands[selectedCommandIndex].Activate();
    }

    override public void Activate() {
      GetControlPanel().SetMenu(this);
    }

    public void ActivateParent() {
      if(parent != null) {
        GetControlPanel().SetMenu(parent);
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
      string row = new string('=', this.panel.Width);
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
      if(parent != null) {
        return parent.GetPath() + "/" + this.name;
      } else {
        return this.name;
      }
    }

    private string GetFooter() {
      string row = new string('=', this.panel.Width);
      if(topVisibleCommand + panel.Heigth < commands.Count) {
        row = row.Remove(2, 2).Insert(2, "\\/");
      }
      return row;
    }

    private string GetRows() {
      System.Collections.Generic.List<string> rows =
          new System.Collections.Generic.List<string>();
      long bottomLimit = topVisibleCommand + panel.Heigth;
      for(int i = topVisibleCommand; i < bottomLimit; i++) {
        if(i < commands.Count) {
          rows.Add(formatCommand(commands[i]));
        } else {
          rows.Add("\n");
        }
      }
      return string.Join("\n", rows.ToArray()) + "\n";
    }

    private string formatCommand(MenuItem command) {
      if(command == selectedCommand) {
        return "*" + command.GetLabel();
      } else {
        return " " + command.GetLabel();
      }
    }
  }
}