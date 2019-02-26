using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IngameScript
{
  public class TestBatteryInfo: BatteryInfo
  {
    private readonly string _name;
    private readonly float _storage;
    private readonly float _stored;
    private readonly float _balance;
    private readonly bool _enabled;
    private readonly bool _charging;

    public TestBatteryInfo(string name, float storage, float stored, float balance, bool enabled, bool charging) {
      _name = name;
      _storage = storage;
      _stored = stored;
      _balance = balance;
      _enabled = enabled;
      _charging = charging;
    }

    public override string Name { get { return _name; } }

    public override float Storage { get { return _storage; } }

    public override float Stored { get { return _stored; } }

    public override float Balance { get { return _balance; } }

    public override bool Enabled { get { return _enabled; } }

    public override bool Charging { get { return _charging; } }
  }

  [TestClass]
  public class EnergyDisplayTest
  {
    private static readonly string NL = Environment.NewLine;

    [TestMethod]
    public void NothingToDisplay() {
      EnergyDisplay display = new EnergyDisplay(34, 18);
      string result = display.Show();
      string expected =
        "[Energia]" + NL +
        "----------------------------------";
      Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SomeBatteries() {
      EnergyDisplay display = new EnergyDisplay(34, 18);
      string result = display.Show(
        new TestBatteryInfo("batteria_1", 3.0f, 2.3f, -317.29f, true, false),
        new TestBatteryInfo("batteria_2", 6.2f, 0f, 0f, false, true)
      );
      string expected =
        "[Energia]" + NL +
        "----------------------------------" + NL +
        "batteria_1 [-] 3Mhw OUT -317.29w" + NL +
        " (||||||||||||||....) 77% | 2.3Mhw" + NL +
        "batteria_2 OFF 6.2Mhw IN 0w" + NL +
        " (..................) 0% | 0Mhw";
      Assert.AreEqual(expected, result);
    }
  }
}
