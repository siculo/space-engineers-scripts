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

    private ResourceType Ice = new ResourceType("MyObjectBuilder_Ore", "Ice");
    private ResourceType Gold = new ResourceType("MyObjectBuilder_Ore", "Gold");
    private ResourceType Silver = new ResourceType("MyObjectBuilder_Ore", "Silver");
    private ResourceType Magnesium = new ResourceType("MyObjectBuilder_Ore", "Magnesium");
    private ResourceType MagnesiumIngot = new ResourceType("MyObjectBuilder_Ingot", "Magnesium");

    [TestMethod]
    public void ResourceTypeProperty()
    {
      Assert.AreEqual(Ice, new ResourceStack(Ice).Type);
    }

    [TestMethod]
    public void ResourceAmountProperty()
    {
      Assert.AreEqual(0, new ResourceStack(Ice, 0).Amount);
      Assert.AreEqual(15, new ResourceStack(Ice, 15).Amount);
      Assert.AreEqual((MyFixedPoint)22.3, new ResourceStack(Ice, (MyFixedPoint)22.3).Amount);
    }

    [TestMethod]
    public void ResourceItemDisplay()
    {
      ResourceRendererContext ctx = new ResourceRendererContext();
      ctx.RowWidth = 33;
      ctx.ResourceNameSpace = 9;
      ctx.AmountSpace = 10;
      ctx.MaxAmount = (MyFixedPoint)12000.225;
      ctx.AmountDecimalDigits = 3;
      Assert.AreEqual("Ice       (||||||||||)  12000.225", new ResourceItemRenderer(new ResourceStack(Ice, (MyFixedPoint)12000.225)).Render(ctx));
      Assert.AreEqual("Gold      (||||......)   5021.59 ", new ResourceItemRenderer(new ResourceStack(Gold, (MyFixedPoint)5021.59)).Render(ctx));
      Assert.AreEqual("Ice       (..........)     77.1  ", new ResourceItemRenderer(new ResourceStack(Ice, (MyFixedPoint)77.1)).Render(ctx));
      Assert.AreEqual("Magnesium (|.........)   1034    ", new ResourceItemRenderer(new ResourceStack(Magnesium, 1034)).Render(ctx));
    }

    [TestMethod]
    public void ResourceItemDisplayIntAmount()
    {
      ResourceRendererContext ctx = new ResourceRendererContext();
      ctx.MaxAmount = (MyFixedPoint)100000;
      ctx.AmountDecimalDigits = 0;
      Assert.AreEqual("Silver     (||||||)     100000", new ResourceItemRenderer(new ResourceStack(Silver, (MyFixedPoint)100000)).Render(ctx));
    }

    [TestMethod]
    public void ResourceDisplay()
    {
      ResourceRendererContext ctx = new ResourceRendererContext();
      ctx.RowWidth = 38;
      ctx.ResourceNameSpace = 10;
      ctx.ResourceTypeSpace = 6;
      ctx.AmountSpace = 8;
      ctx.MaxAmount = (MyFixedPoint)10000;
      ctx.AmountDecimalDigits = 2;
      ResourceListRenderer display = new ResourceListRenderer(ctx);
      string result = display.Render(new ResourceItemRenderer[] {
        new ResourceItemRenderer(new ResourceStack(Ice, (MyFixedPoint)5000)),
        new ResourceItemRenderer(new ResourceStack(MagnesiumIngot, (MyFixedPoint)1334.44))
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
