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
      ResourceDisplay display = new ResourceDisplay(36, 14);
      string result = display.Show();
      string expected =
        "[Risorse]" + NL +
        "------------------------------------";
      Assert.AreEqual(expected, result);
    }
  }
}
