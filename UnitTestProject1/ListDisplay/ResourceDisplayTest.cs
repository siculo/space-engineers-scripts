using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IngameScript.Program;

/*
 * TODO:
 * [x] Singola classe Display
 * [-] Rimuovere duplicazione NL nel test
 */
namespace IngameScript
{
  [TestClass]
  public class ResourceDisplayTest
  {
    private static readonly string NL = Environment.NewLine;

    [TestMethod]
    public void NoResourcesToDisplay() {
      DisplayContext ctx = new ResourceDisplayContext();
      ctx.BarWidth = 14;
      ctx.RowWidth = 36;
      ResourceDisplay display = new ResourceDisplay(ctx);
      string result = display.Show();
      string expected =
        "[Resources]" + NL +
        "------------------------------------";
      Assert.AreEqual(expected, result);
    }
  }
}
