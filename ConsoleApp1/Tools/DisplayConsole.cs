using System;

namespace IngameScript
{
  // --------------------------------
  public class DisplayConsole
  {
    private TextOuput outputConsole;
    private int bufferSize;
    private System.Collections.Generic.LinkedList<string> buffer =
        new System.Collections.Generic.LinkedList<string>();
    private System.Text.StringBuilder lastLine =
        new System.Text.StringBuilder();
    private bool shoudAddNewLine = false;

    public DisplayConsole(TextOuput outputConsole,int bufferSize) {
      this.outputConsole = outputConsole;
      this.bufferSize = bufferSize;
    }

    public void WriteLine(string text) {
      this.Write(text + Environment.NewLine);
    }

    public void Write(string text) {
      var lines = System.Text.RegularExpressions.Regex.Split(text,"\r\n|\r|\n");
      bool first = true;
      foreach(string line in lines) {
        if(first) first = false; else shoudAddNewLine = true;
        AppendText(line);
      }
      refreshConsole();
    }

    private void AppendText(string text) {
      if(text.Length > 0) {
        if(shoudAddNewLine) {
          shoudAddNewLine = false;
          NewLine();
        }
        lastLine.Append(text);
      }
    }

    private void NewLine() {
      buffer.AddLast(lastLine.ToString());
      if(buffer.Count > bufferSize) {
        buffer.RemoveFirst();
      }
      lastLine.Clear();
    }

    private void refreshConsole() {
      System.Text.StringBuilder output =
          new System.Text.StringBuilder();
      foreach(string line in this.buffer) {
        output.AppendLine(line);
      }
      output.Append(lastLine.ToString());
      outputConsole.Set(output.ToString());
    }
  }
  // --------------------------------
}