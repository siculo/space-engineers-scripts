﻿using System;
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
   *  [x] resource ListDisplayItem
   *    [x] DisplayContext param in ListDisplay constructor
   *    [x] set maximum value for graph bar
   *  [x] EnergyDisplayContext
   *  
   *  [-] summary of a container with only one resource
   *  [-] summary of some containers with only one resource of the same type
   *  [-] summary of a container with more resources of the same types
   *  [-] in-game display of single resource summary
   *  
   *  [-] parametric number of decimal digits in DisplayContext.allignToDecimalSeparator
   *  [-] summary of a container with more resources of different types
   *  [-] summary of some containers with resources of different types
   *  [-] all types of resources
   *  
   *  [-] DisplayContext with constructor for properties initial values (properties should be R/O?)
   *  [-] generic display context that does not belongs to ListDisplay (se è il caso fare ListDisplayContext : DisplayContext)
   *  [?] move label property from ListDisplay to DisplayContext
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
      ResourceDisplayContext ctx = new ResourceDisplayContext();
      ctx.MaxAmount = (MyFixedPoint)12000.22;
      ctx.BarWidth = 11;
      Assert.AreEqual("Ice      (|||||||||||)  12000.22", new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)12000.22)).Render(ctx));
      Assert.AreEqual("Ice      (|||||......)   5021.59", new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)5021.59)).Render(ctx));
      Assert.AreEqual("Ice      (...........)     77.1 ", new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)77.1)).Render(ctx));
      Assert.AreEqual("Ice      (|..........)   1034   ", new ResourceItem(new Resource(ResourceType.Ice, 1034)).Render(ctx));
    }

    [TestMethod]
    public void ResourceDisplayItem2()
    {
      ResourceDisplayContext ctx = new ResourceDisplayContext();
      ctx.MaxAmount = (MyFixedPoint)10000;
      ctx.BarWidth = 8;
      Assert.AreEqual("Ice      (||||....)   5000   ", new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)5000)).Render(ctx));
      Assert.AreEqual("Ice      (||......)   2500   ", new ResourceItem(new Resource(ResourceType.Ice, (MyFixedPoint)2500)).Render(ctx));
    }

    /*
    [TestMethod]
    public void ResourceDisplay()
    {
      ResourceDisplayContext ctx = new ResourceDisplayContext();

    }
    */
  }
}
