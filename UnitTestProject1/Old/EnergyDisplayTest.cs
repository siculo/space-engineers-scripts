using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IngameScript
{
    public class TestBatteryInfo : BatteryInfo
    {
        private readonly string _name;
        private readonly float _storage;
        private readonly float _stored;
        private readonly float _balance;

        public TestBatteryInfo(string name, float storage, float stored, float balance)
        {
            _name = name;
            _storage = storage;
            _stored = stored;
            _balance = balance;
        }

        public override string name { get { return _name; } }

        public override float storage { get { return _storage; } }

        public override float stored { get { return _stored; } }

        public override float balance { get { return _balance; } }
    }

    [TestClass]
    public class EnergyDisplayTest
    {
        private static readonly string NL = Environment.NewLine;

        [TestMethod]
        public void NothingToDisplay()
        {
            EnergyDisplay display = new EnergyDisplay(34, 18);
            string result = display.Show();
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void SomeBatteries()
        {
            EnergyDisplay display = new EnergyDisplay(34, 18);
            string result = display.Show(new TestBatteryInfo("batteria_1", 3.0f, 2.3f, -317.29f), new TestBatteryInfo("batteria_2", 6.2f, 0f, 0f));
            string expected =
                "[batteria_1 -]" + NL +
                "----------------------------------" + NL +
                "3Mhw | I/O = -317.29w" + NL +
                "(||||||||||||||....) 77%, 2.3Mhw" + NL + NL +
                "[batteria_2 -]" + NL +
                "----------------------------------" + NL +
                "6.2Mhw | I/O = 0w" + NL +
                "(..................) 0%, 0Mhw";
            Assert.AreEqual(expected, result);
        }
    }
}
