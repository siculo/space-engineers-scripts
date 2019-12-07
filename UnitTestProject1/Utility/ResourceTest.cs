using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VRage;
using static IngameScript.Program;

namespace UnitTestProject1.Utility
{
  /*
   * TODO:
   *  [-] resource ListDisplayItem
   *  [-] summary of a container with only one resource
   *  [-] summary of some containers with only one resource of the same type
   *  [-] summary of a container with more resources of the same types
   *  
   *  [-] all types of resources
   *  [-] summary of a container with more resources of different types
   *  [-] summary of some containers with resources of different types
   */
  [TestClass]
  public class ResourceTest
  {
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
      // todo: Render should use maximum amount value to calculate bar size
      DisplayContext ctx = new DisplayContext();
      Assert.AreEqual("Ice      (|||||||||||)  12000.22", new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)12000.22)).Render(ctx));
      Assert.AreEqual("Ice      (|||||......)   5021.59", new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)5021.59)).Render(ctx));
      Assert.AreEqual("Ice      (...........)     77.1 ", new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)77.1)).Render(ctx));
      Assert.AreEqual("Ice      (|..........)    234   ", new ResourceItem(new Resource(ResourceType.Ice, 234)).Render(ctx));
    }
  }
}
