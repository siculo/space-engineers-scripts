﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage;
using static IngameScript.Program;

namespace IngameScript
{
  partial class Program
  {
    [TestClass]
    public class ContainerTest
    {
      [TestMethod]
      public void TestCompareResourceTypes()
      {
        Assert.AreEqual(new ResourceType("Ingot", "Iron"), new ResourceType("Ingot", "Iron"));
        Assert.AreNotEqual(new ResourceType("Ore", "Iron"), new ResourceType("Ingot", "Iron"));
      }

      [TestMethod]
      public void TestCompareContainerResources()
      {
        ResourceInventory inventory = new ResourceInventory(new TestContainer(new ResourceStack[] { new ResourceStack(new ResourceType("", "Ice")) }).GetResources());
        Assert.IsTrue(Enumerable.SequenceEqual(new List<ResourceStack>() { new ResourceStack(new ResourceType("", "Ice")) }, inventory.GetResources()));
      }

      [TestMethod]
      public void TestInventoryEquals()
      {
        Assert.AreEqual(
          new ResourceInventory(new List<ResourceStack>() { new ResourceStack(new ResourceType("Ore", "Ice")), new ResourceStack(new ResourceType("Ingot", "Gold"), 22), new ResourceStack(new ResourceType("Ingot", "Iron"), 7) }),
          new ResourceInventory(new List<ResourceStack>() { new ResourceStack(new ResourceType("Ingot", "Iron"), 7), new ResourceStack(new ResourceType("Ore", "Ice")), new ResourceStack(new ResourceType("Ingot", "Gold"), 22) })
          );
      }

      [TestMethod]
      public void TestConsolidateInventory()
      {
        ResourceInventory inventory = new ResourceInventory(new List<ResourceStack>() { new ResourceStack(new ResourceType("", "Ice"), 22), new ResourceStack(new ResourceType("", "Ice"), 14) });
        Assert.IsTrue(Enumerable.SequenceEqual(new List<ResourceStack>() { new ResourceStack(new ResourceType("", "Ice"), 14 + 22) }, inventory.GetResources()));
      }

      [TestMethod]
      public void TestInventoryAddition()
      {
        ResourceInventory inventoryA = new ResourceInventory(new List<ResourceStack>() { new ResourceStack(new ResourceType("Ore", "Ice"), 20) });
        ResourceInventory inventoryB = new ResourceInventory(new List<ResourceStack>() { new ResourceStack(new ResourceType("Ore", "Ice"), 50) });
        ResourceInventory inventoryC = inventoryA + inventoryB;
        Assert.IsTrue(Enumerable.SequenceEqual(new List<ResourceStack>() { new ResourceStack(new ResourceType("Ore", "Ice"), 70) }, inventoryC.GetResources()));
      }

      [TestMethod]
      public void TestFullInventory()
      {
        Container[] containers = new Container[]
        {
          new TestContainer(null),
          new TestContainer(new ResourceStack[] { new ResourceStack(new ResourceType("Ore", "Ice"), 40), new ResourceStack(new ResourceType("Ore", "Iron"), 77) }),
          new TestContainer(new ResourceStack[] { new ResourceStack(new ResourceType("Ingot", "Iron"), 15), new ResourceStack(new ResourceType("Ingot", "Gold"), 25) }),
          new TestContainer(
            new ResourceStack[] {
              new ResourceStack(new ResourceType("Ingot", "Iron"), 5),
              new ResourceStack(new ResourceType("Ore", "Ice"), 8),
              new ResourceStack(new ResourceType("Ingot", "Gold"), 2),
              new ResourceStack(new ResourceType("Ore", "Silver"), 14)
            }
          ) 
        };
        ResourceInventory inventory = ResourceInventory.Stocktake(containers);
        Assert.AreEqual(
          new ResourceInventory(new ResourceStack[] {
            new ResourceStack(new ResourceType("Ore", "Ice"), 48),
            new ResourceStack(new ResourceType("Ingot", "Iron"), 20),
            new ResourceStack(new ResourceType("Ingot", "Gold"), 27),
            new ResourceStack(new ResourceType("Ore", "Silver"), 14),
            new ResourceStack(new ResourceType("Ore", "Iron"), 77)
          }),
          inventory
          );
      }

      [TestMethod]
      public void TestInventoryFilteringByTags()
      {
        Container[] containers = new Container[]
        {
          new TestContainer(new ResourceStack[] { new ResourceStack(new ResourceType("Ore", "Ice"), 40), new ResourceStack(new ResourceType("Ore", "Iron"), 77) }, "tag2,tag3"),
          new TestContainer(new ResourceStack[] { new ResourceStack(new ResourceType("Ingot", "Iron"), 15), new ResourceStack(new ResourceType("Ore", "Iron"), 25) }, "tag1, tag2,tag3"),
          new TestContainer(new ResourceStack[] { new ResourceStack(new ResourceType("Ore", "Gold"), 35) }, "tag2, tag4")
        };
        ResourceInventory inventory = ResourceInventory.Stocktake(containers, Parsers.ParseTags("tag1, tag3"));
        Assert.AreEqual(new ResourceInventory(new ResourceStack[]
          {
            new ResourceStack(new ResourceType("Ore", "Ice"), 40),
            new ResourceStack(new ResourceType("Ore", "Iron"), 77 + 25),
            new ResourceStack(new ResourceType("Ingot", "Iron"), 15)
          }
          ), inventory);
      }

      [TestMethod]
      public void TestInventoryFilteringByResourceType()
      {
        Container[] containers = new Container[]
        {
          new TestContainer(new ResourceStack[] { new ResourceStack(new ResourceType("MyObjectBuilder_Ore", "Ice"), 40), 
            new ResourceStack(new ResourceType("MyObjectBuilder_Ore", "Iron"), 77), new ResourceStack(new ResourceType("MyObjectBuilder_Ingot", "Gold"), 5) }),
          new TestContainer(new ResourceStack[] { new ResourceStack(new ResourceType("MyObjectBuilder_Ingot", "Iron"), 15), new ResourceStack(new ResourceType("MyObjectBuilder_Ore", "Iron"), 25) }),
          new TestContainer(new ResourceStack[] { new ResourceStack(new ResourceType("MyObjectBuilder_Ingot", "Gold"), 35) })
        };
        ResourceInventory inventory = ResourceInventory.Stocktake(containers, null, Parsers.ParseResourceFilter("ingot"));
        Assert.AreEqual(new ResourceInventory(new ResourceStack[]
          {
            new ResourceStack(new ResourceType("MyObjectBuilder_Ingot", "Iron"), 15),
            new ResourceStack(new ResourceType("MyObjectBuilder_Ingot", "Gold"), 40)
          }
          ), inventory);
      }
      
      [TestMethod]
      public void TestInventoryFilterByResourceSubtype()
      {
        Container[] containers = new Container[]
        {
          new TestContainer(new ResourceStack[] { new ResourceStack(new ResourceType("MyObjectBuilder_Ore", "Ice"), 40), 
            new ResourceStack(new ResourceType("MyObjectBuilder_Ore", "Iron"), 77), new ResourceStack(new ResourceType("MyObjectBuilder_Ingot", "Gold"), 5) }),
          new TestContainer(new ResourceStack[] { new ResourceStack(new ResourceType("MyObjectBuilder_Ingot", "Iron"), 15), new ResourceStack(new ResourceType("MyObjectBuilder_Ore", "Iron"), 25) }),
          new TestContainer(new ResourceStack[] { new ResourceStack(new ResourceType("MyObjectBuilder_Ore", "Gold"), 35) })
        };
        ResourceInventory inventory = ResourceInventory.Stocktake(containers, null, Parsers.ParseResourceFilter(":Iron,ore: gold, ingot :gold"));
        Assert.AreEqual(new ResourceInventory(new ResourceStack[]
          {
            new ResourceStack(new ResourceType("MyObjectBuilder_Ore", "Iron"), 77 + 25),
            new ResourceStack(new ResourceType("MyObjectBuilder_Ingot", "Iron"), 15),
            new ResourceStack(new ResourceType("MyObjectBuilder_Ore", "Gold"), 35),
            new ResourceStack(new ResourceType("MyObjectBuilder_Ingot", "Gold"), 5)
          }
          ), inventory);
      }

      [TestMethod]
      public void TestInventoryMaximum()
      {
        ResourceInventory inventory = new ResourceInventory(new ResourceStack[] {
            new ResourceStack(new ResourceType("Ore", "Ice"), 48),
            new ResourceStack(new ResourceType("Ingot", "Iron"), 20),
            new ResourceStack(new ResourceType("Ingot", "Gold"), 27),
            new ResourceStack(new ResourceType("Ore", "Silver"), 14),
            new ResourceStack(new ResourceType("Ingot", "Iron"), 66),
            new ResourceStack(new ResourceType("Ore", "Iron"), 12)
          });
        Assert.AreEqual(86, inventory.GetMaximum());
      }
    }

    private class TestContainer : Container
    {
      private IEnumerable<ResourceStack> _resources;

      public TestContainer(ResourceStack[] resources, string tags = null): base(tags)
      {
        if (resources == null)
        {
          _resources = new List<ResourceStack>();
        }
        else
        {
          _resources = resources;
        }
      }

      override public IEnumerable<ResourceStack> GetResources()
      {
        return _resources;
      }
    }
  }
}
