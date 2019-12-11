using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IngameScript.Program;
using VRage;

namespace IngameScript
{
  [TestClass]

  /*
  * TODO:
  *  [-] DisplayContext with constructor for properties initial values (properties should be R/O?)
  *  [-] generic display context that does not belongs to ListDisplay (se è il caso fare ListDisplayContext : DisplayContext)
  *  [?] move label property from ListDisplay to DisplayContext
  */
  public class ResourceDisplayTest
  {
    private static readonly string NL = Environment.NewLine;

    [TestMethod]
    public void ResourceTypeProperty()
    {
      Assert.AreEqual(ResourceType.Ice, new Resource(ResourceType.Ice).Type);
    }


    [TestMethod]
    public void ResourceAmountProperty()
    {
      Assert.AreEqual(0, new Resource(ResourceType.Ice, 0).Amount);
      Assert.AreEqual(15, new Resource(ResourceType.Ice, 15).Amount);
      Assert.AreEqual((MyFixedPoint)22.3, new Resource(ResourceType.Ice, (MyFixedPoint)22.3).Amount);
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
      Assert.AreEqual("Ice       (||||||||||)  12000.225", new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)12000.225)).Render(ctx));
      Assert.AreEqual("Gold      (||||......)   5021.59 ", new ResourceItem(new Resource(ResourceType.Gold, (MyFixedPoint)5021.59)).Render(ctx));
      Assert.AreEqual("Ice       (..........)     77.1  ", new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)77.1)).Render(ctx));
      Assert.AreEqual("Magnesium (|.........)   1034    ", new ResourceItem(new Resource(ResourceType.Magnesium, 1034)).Render(ctx));
    }

    [TestMethod]
    public void ResourceItemDisplayIntAmount()
    {
      ResourceDisplayContext ctx = new ResourceDisplayContext();
      ctx.MaxAmount = (MyFixedPoint)100000;
      ctx.AmountDecimalDigits = 0;
      Assert.AreEqual("Silver     (||||||)     100000", new ResourceItem(new Resource(ResourceType.Silver, (MyFixedPoint)100000)).Render(ctx));
    }

    [TestMethod]
    public void ResourceDisplay()
    {
      ResourceDisplayContext ctx = new ResourceDisplayContext();
      ctx.RowWidth = 30;
      ctx.ResourceNameSpace = 10;
      ctx.AmountSpace = 8;
      ctx.MaxAmount = (MyFixedPoint)10000;
      ctx.AmountDecimalDigits = 2;
      ResourceDisplay display = new ResourceDisplay(ctx);
      string result = display.Show(new ResourceItem[] { new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)5000)) });
      string expected =
        "[Resources]" + NL +
        "------------------------------" + NL +
        "Ice        (||||....)  5000   ";
      Assert.AreEqual(expected, result);
    }
  }
}
