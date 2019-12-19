using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IngameScript.Program;

namespace IngameScript
{
  [TestClass]
  public class SpinningBarTest
  {
    [TestMethod]
    public void Steps()
    {
      Assert.AreEqual("-", SpinningBar.Render());
      SpinningBar.Step();
      Assert.AreEqual("\\", SpinningBar.Render());
      SpinningBar.Step();
      Assert.AreEqual("|", SpinningBar.Render());
      SpinningBar.Step();
      Assert.AreEqual("/", SpinningBar.Render());
      SpinningBar.Step();
      Assert.AreEqual("-", SpinningBar.Render());
    }
  }
}
