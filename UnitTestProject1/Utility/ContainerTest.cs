using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        Summary summary = new Summary(new TestContainer(new Resource[] { new Resource(new ResourceType("", "Ice")) }).GetResources());
        Assert.IsTrue(Enumerable.SequenceEqual(new List<Resource>() { new Resource(new ResourceType("", "Ice")) }, summary.GetResources()));
      }

      [TestMethod]
      public void TestSummaryEquals()
      {
        Assert.AreEqual(
          new Summary(new List<Resource>() { new Resource(new ResourceType("Ore", "Ice")), new Resource(new ResourceType("Ingot", "Gold"), 22), new Resource(new ResourceType("Ingot", "Iron"), 7) }),
          new Summary(new List<Resource>() { new Resource(new ResourceType("Ingot", "Iron"), 7), new Resource(new ResourceType("Ore", "Ice")), new Resource(new ResourceType("Ingot", "Gold"), 22) })
          );
      }

      [TestMethod]
      public void TestConsolidateSummary()
      {
        Summary summary = new Summary(new List<Resource>() { new Resource(new ResourceType("", "Ice"), 22), new Resource(new ResourceType("", "Ice"), 14) });
        Assert.IsTrue(Enumerable.SequenceEqual(new List<Resource>() { new Resource(new ResourceType("", "Ice"), 14 + 22) }, summary.GetResources()));
      }

      [TestMethod]
      public void TestSummaryAddition()
      {
        Summary summaryA = new Summary(new List<Resource>() { new Resource(new ResourceType("Ore", "Ice"), 20) });
        Summary summaryB = new Summary(new List<Resource>() { new Resource(new ResourceType("Ore", "Ice"), 50) });
        Summary summaryC = summaryA + summaryB;
        Assert.IsTrue(Enumerable.SequenceEqual(new List<Resource>() { new Resource(new ResourceType("Ore", "Ice"), 70) }, summaryC.GetResources()));
      }

      [TestMethod]
      public void TestFullSummary()
      {
        Container[] containers = new Container[]
        {
          new TestContainer(null),
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ore", "Ice"), 40), new Resource(new ResourceType("Ore", "Iron"), 77) }),
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ingot", "Iron"), 15), new Resource(new ResourceType("Ingot", "Gold"), 25) }),
          new TestContainer(
            new Resource[] {
              new Resource(new ResourceType("Ingot", "Iron"), 5),
              new Resource(new ResourceType("Ore", "Ice"), 8),
              new Resource(new ResourceType("Ingot", "Gold"), 2),
              new Resource(new ResourceType("Ore", "Silver"), 14)
            }
          ) 
        };
        Summary summary = Summary.ContainersSummary(containers);
        Assert.AreEqual(
          new Summary(new Resource[] {
            new Resource(new ResourceType("Ore", "Ice"), 48),
            new Resource(new ResourceType("Ingot", "Iron"), 20),
            new Resource(new ResourceType("Ingot", "Gold"), 27),
            new Resource(new ResourceType("Ore", "Silver"), 14),
            new Resource(new ResourceType("Ore", "Iron"), 77)
          }),
          summary
          );
      }

      [TestMethod]
      public void TestContainersWithOneTagSummary()
      {
        Container[] containers = new Container[]
        {
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ore", "Ice"), 40), new Resource(new ResourceType("Ore", "Iron"), 77) }),
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ingot", "Iron"), 15), new Resource(new ResourceType("Ore", "Iron"), 25) }, "tag"),
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ore", "Gold"), 35) }, "tag")
        };
        Summary summary = Summary.ContainersSummary(containers, "tag");
        Assert.AreEqual(
          new Summary(new Resource[]
          {
            new Resource(new ResourceType("Ingot", "Iron"), 15),
            new Resource(new ResourceType("Ore", "Iron"), 25),
            new Resource(new ResourceType("Ore", "Gold"), 35)
          }
          ),
          summary
          );
      }

      [TestMethod]
      public void TestContainersWithSomeTagsSummary()
      {
        Container[] containers = new Container[]
        {
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ore", "Ice"), 40), new Resource(new ResourceType("Ore", "Iron"), 77) }, "tag2,tag3"),
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ingot", "Iron"), 15), new Resource(new ResourceType("Ore", "Iron"), 25) }, "tag1, tag2,tag3"),
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ore", "Gold"), 35) }, "tag2, tag4")
        };
        Summary summary = Summary.ContainersSummary(containers, "tag1, tag3");
        Assert.AreEqual(new Summary(new Resource[]
          {
            new Resource(new ResourceType("Ore", "Ice"), 40),
            new Resource(new ResourceType("Ore", "Iron"), 77 + 25),
            new Resource(new ResourceType("Ingot", "Iron"), 15)
          }
          ), summary);
      }

      [TestMethod]
      public void TestSummaryFilterByResourceType()
      {
        Container[] containers = new Container[]
        {
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ore", "Ice"), 40), new Resource(new ResourceType("Ore", "Iron"), 77), new Resource(new ResourceType("Ingot", "Gold"), 5) }),
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ingot", "Iron"), 15), new Resource(new ResourceType("Ore", "Iron"), 25) }),
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ingot", "Gold"), 35) })
        };
        Summary summary = Summary.ContainersSummary(containers, null, new ResourceType[] { new ResourceType("Ingot", null) });
        Assert.AreEqual(new Summary(new Resource[]
          {
            new Resource(new ResourceType("Ingot", "Iron"), 15),
            new Resource(new ResourceType("Ingot", "Gold"), 40)
          }
          ), summary);
      }

      [TestMethod]
      public void TestSummaryFilterByResourceSubtype()
      {
        Container[] containers = new Container[]
        {
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ore", "Ice"), 40), new Resource(new ResourceType("Ore", "Iron"), 77), new Resource(new ResourceType("Ingot", "Gold"), 5) }),
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ingot", "Iron"), 15), new Resource(new ResourceType("Ore", "Iron"), 25) }),
          new TestContainer(new Resource[] { new Resource(new ResourceType("Ore", "Gold"), 35) })
        };
        Summary summary = Summary.ContainersSummary(containers, null, new ResourceType[] { new ResourceType(null, "Iron"), new ResourceType(null, "Gold") });
        Assert.AreEqual(new Summary(new Resource[]
          {
            new Resource(new ResourceType("Ore", "Iron"), 77 + 25),
            new Resource(new ResourceType("Ingot", "Iron"), 15),
            new Resource(new ResourceType("Ore", "Gold"), 35),
            new Resource(new ResourceType("Ingot", "Gold"), 5)
          }
          ), summary);
      }

      [TestMethod]
      public void TestSummaryMaximum()
      {
        Summary summary = new Summary(new Resource[] {
            new Resource(new ResourceType("Ore", "Ice"), 48),
            new Resource(new ResourceType("Ingot", "Iron"), 20),
            new Resource(new ResourceType("Ingot", "Gold"), 27),
            new Resource(new ResourceType("Ore", "Silver"), 14),
            new Resource(new ResourceType("Ingot", "Iron"), 66),
            new Resource(new ResourceType("Ore", "Iron"), 12)
          });
        Assert.AreEqual(86, summary.GetMaximum());
      }
    }

    private class TestContainer : Container
    {
      private IEnumerable<Resource> _resources;

      public TestContainer(Resource[] resources, string tags = null): base(tags)
      {
        if (resources == null)
        {
          _resources = new List<Resource>();
        }
        else
        {
          _resources = resources;
        }
      }

      override public IEnumerable<Resource> GetResources()
      {
        return _resources;
      }
    }
  }
}
