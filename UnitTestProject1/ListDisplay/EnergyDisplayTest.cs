using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IngameScript.Program;

namespace IngameScript
{
  [TestClass]
  public class EnergyDisplayTest
  {
    private static readonly string NL = Environment.NewLine;

    [TestMethod]
    public void NothingToDisplay() {
      EnergyRendererContext ctx = new EnergyRendererContext();
      ctx.BarWidth = 18;
      ctx.RowWidth = 34;
      EnergyListRenderer display = new EnergyListRenderer(ctx);
      string result = display.Render();
      string expected =
        "[Energy -]" + NL +
        "----------------------------------";
      Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SomeBlocks()
    {
      EnergyRendererContext ctx = new EnergyRendererContext();
      ctx.BarWidth = 10;
      ctx.RowWidth = 66;
      EnergyListRenderer display = new EnergyListRenderer(ctx);
      string result = display.Render(
        new TestBatteryBlock("batteria_1", 3.0f, 2.3f, -317.29f, true, false),
        new TestBatteryBlock("batteria_2", 6.2f, 0f, 0f, false, true),
        new TestPowerProductionBlock("reattore_1", 15.0f, 1.45f, true),
        new TestPowerProductionBlock("reattore_2", 15.00011111f, 0f, false),
        new TestPowerProductionBlock("pannello_solare", 0f, 0f, true)
      );
      string expected =
        "[Energy -]" + NL +
        "------------------------------------------------------------------" + NL +
        "batteria_1 [-]" + NL +
        " (||||||||..)  77% 3MWh OUT -317.29W 2.3MWh" + NL +
        "batteria_2 OFF" + NL +
        " (..........)   0% 6.2MWh  IN 0W 0MWh" + NL +
        "reattore_1 [-]" + NL +
        " (|.........)  10% 15MW OUT 1.45MW" + NL +
        "reattore_2 OFF" + NL +
        " (..........)   0% 15MW OUT 0MW" + NL +
        "pannello_solare [-]" + NL +
        " (          ) [NA] 0MW OUT 0MW";
      Assert.AreEqual(expected, result);
    }
  }

  class TestBatteryBlock: BatteryItemRenderer
  {
    private readonly string _name;
    private readonly float _storage, _stored, _balance;
    private readonly bool _enabled, _charging;

    public TestBatteryBlock(string name, float storage, float stored, float balance, bool enabled, bool charging) {
      _name = name;
      _storage = storage;
      _stored = stored;
      _balance = balance;
      _enabled = enabled;
      _charging = charging;
    }

    public override string Name { get { return _name; } }
    public override bool Enabled { get { return _enabled; } }
    public override float Storage { get { return _storage; } }
    public override float Stored { get { return _stored; } }
    public override float Balance { get { return _balance; } }
    public override bool Charging { get { return _charging; } }
  }

  class TestPowerProductionBlock: PowerProductionItem
  {
    private string _name;
    private bool _enabled;
    private float _maxOutput, _curOutput;

    public TestPowerProductionBlock(string name, float maxOutput, float curOutput, bool enabled) {
      _name = name;
      _maxOutput = maxOutput;
      _curOutput = curOutput;
      _enabled = enabled;
    }

    public override string Name { get { return _name; } }
    public override bool Enabled { get { return _enabled; } }
    public override float MaxOutput { get { return _maxOutput; } }
    public override float CurrentOutput { get { return _curOutput; } }
  }
}
