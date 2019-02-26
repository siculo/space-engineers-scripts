using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IngameScript
{
    [TestClass]
    public class SpinningBarTest
    {
        [TestMethod]
        public void steps()
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
