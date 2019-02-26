using System;

namespace ConsoleApp1
{
  class MainClass
  {
    static int Main(string[] args) {
      string text = "uno-due\ntre";
      var lines = System.Text.RegularExpressions.Regex.Split(text,"\r\n|\r|\n|Y");
      bool first = true;
      foreach(string line in lines) {
        Console.WriteLine("[" + line + "]: " + line.Length);
      }
      return 0;
    }
  }
}
