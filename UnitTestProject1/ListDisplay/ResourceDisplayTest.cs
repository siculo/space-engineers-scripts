using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IngameScript.Program;
using VRage;

namespace IngameScript
{
  [TestClass]
  public class ResourceDisplayTest
  {
    private static readonly string NL = Environment.NewLine;

    private ResourceType Ice = new ResourceType("Ore", "Ice");
    private ResourceType Gold = new ResourceType("Ore", "Gold");
    private ResourceType Silver = new ResourceType("Ore", "Silver");
    private ResourceType Magnesium = new ResourceType("Ore", "Magnesium");
    private ResourceType MagnesiumIngot = new ResourceType("Ingot", "Magnesium");

    [TestMethod]
    public void ResourceTypeProperty()
    {
      Assert.AreEqual(Ice, new Resource(Ice).Type);
    }


    [TestMethod]
    public void ResourceAmountProperty()
    {
      Assert.AreEqual(0, new Resource(Ice, 0).Amount);
      Assert.AreEqual(15, new Resource(Ice, 15).Amount);
      Assert.AreEqual((MyFixedPoint)22.3, new Resource(Ice, (MyFixedPoint)22.3).Amount);
    }

    [TestMethod]
    public void ResourceItemDisplay()
    {
      ResourceDisplayContext ctx = new ResourceDisplayContext();
      ctx.RowWidth = 33;
      ctx.ResourceNameSpace = 9;
      ctx.AmountSpace = 10;
      ctx.MaxAmount = (MyFixedPoint)12000.225;
      ctx.AmountDecimalDigits = 3;
      Assert.AreEqual("Ice       (||||||||||)  12000.225", new ResourceItem(new Resource(Ice, (MyFixedPoint)12000.225)).Render(ctx));
      Assert.AreEqual("Gold      (||||......)   5021.59 ", new ResourceItem(new Resource(Gold, (MyFixedPoint)5021.59)).Render(ctx));
      Assert.AreEqual("Ice       (..........)     77.1  ", new ResourceItem(new Resource(Ice, (MyFixedPoint)77.1)).Render(ctx));
      Assert.AreEqual("Magnesium (|.........)   1034    ", new ResourceItem(new Resource(Magnesium, 1034)).Render(ctx));
    }

    [TestMethod]
    public void ResourceItemDisplayIntAmount()
    {
      ResourceDisplayContext ctx = new ResourceDisplayContext();
      ctx.MaxAmount = (MyFixedPoint)100000;
      ctx.AmountDecimalDigits = 0;
      Assert.AreEqual("Silver     (||||||)     100000", new ResourceItem(new Resource(Silver, (MyFixedPoint)100000)).Render(ctx));
    }

    [TestMethod]
    public void ResourceDisplay()
    {
      ResourceDisplayContext ctx = new ResourceDisplayContext();
      ctx.RowWidth = 38;
      ctx.ResourceNameSpace = 10;
      ctx.ResourceTypeSpace = 6;
      ctx.AmountSpace = 8;
      ctx.MaxAmount = (MyFixedPoint)10000;
      ctx.AmountDecimalDigits = 2;
      ResourceDisplay display = new ResourceDisplay(ctx);
      string result = display.Show(new ResourceItem[] {
        new ResourceItem(new Resource(Ice, (MyFixedPoint)5000)),
        new ResourceItem(new Resource(MagnesiumIngot, (MyFixedPoint)1334.44))
      });
      string expected =
        "[Resources -]" + NL +
        "--------------------------------------" + NL +
        "Ice       (Ore)    (||||....)  5000   " + NL +
        "Magnesium (Ingot)  (|.......)  1334.44";
      Assert.AreEqual(expected, result);
    }
  }
}
