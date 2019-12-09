using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IngameScript.Program;
using VRage;

namespace IngameScript
{
  [TestClass]

  /*
  * TODO:
  *  [-] parametric number of decimal digits in DisplayContext.allignToDecimalSeparator
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
    public void ResourceDisplayItem()
    {
      ResourceDisplayContext ctx = new ResourceDisplayContext();
      ctx.MaxAmount = (MyFixedPoint)12000.22;
      ctx.BarWidth = 11;
      Assert.AreEqual("Ice      (|||||||||||)  12000.22", new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)12000.22)).Render(ctx));
      Assert.AreEqual("Ice      (|||||......)   5021.59", new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)5021.59)).Render(ctx));
      Assert.AreEqual("Ice      (...........)     77.1 ", new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)77.1)).Render(ctx));
      Assert.AreEqual("Ice      (|..........)   1034   ", new ResourceItem(new Resource(ResourceType.Ice, 1034)).Render(ctx));
    }

    [TestMethod]
    public void ResourceDisplay()
    {
      ResourceDisplayContext ctx = new ResourceDisplayContext();
      ctx.RowWidth = 30;
      ctx.MaxAmount = (MyFixedPoint)10000;
      ctx.BarWidth = 8;
      ResourceDisplay display = new ResourceDisplay(ctx);
      Resource resource = new Resource(ResourceType.Ice, (MyFixedPoint)5000);
      ResourceItem resourceItem = new ResourceItem(resource);
      ResourceItem[] items = new ResourceItem[] { resourceItem };
      string result = display.Show(items);
      string expected =
        "[Resources]" + NL +
        "------------------------------" + NL +
        "Ice      (||||....)   5000   ";
      Assert.AreEqual(expected, result);
    }
  }
}
