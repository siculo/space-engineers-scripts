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
            SpinningBar bar = new SpinningBar();
            Assert.AreEqual("-", bar.ToString());
            bar.Step();
            Assert.AreEqual("\\", bar.ToString());
            bar.Step();
            Assert.AreEqual("|", bar.ToString());
            bar.Step();
            Assert.AreEqual("/", bar.ToString());
            bar.Step();
            Assert.AreEqual("-", bar.ToString());
        }
    }
}
