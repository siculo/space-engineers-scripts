using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        Summary summary = new Summary(new TestContainer(new Resource(new ResourceType("", "Ice"))).GetResources());
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
        Container container1 = new TestContainer();
        Container container2 = new TestContainer(new Resource(new ResourceType("Ore", "Ice"), 40), new Resource(new ResourceType("Ore", "Iron"), 77));
        Container container3 = new TestContainer(new Resource(new ResourceType("Ingot", "Iron"), 15), new Resource(new ResourceType("Ingot", "Gold"), 25));
        Container container4 = new TestContainer(
          new Resource(new ResourceType("Ingot", "Iron"), 5),
          new Resource(new ResourceType("Ore", "Ice"), 8),
          new Resource(new ResourceType("Ingot", "Gold"), 2),
          new Resource(new ResourceType("Ore", "Silver"), 14)
        );
        Container[] containers = new Container[]
        {
          container1, container2, container3, container4
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

      public TestContainer(params Resource[] resources)
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

      public IEnumerable<Resource> GetResources()
      {
        return _resources;
      }
    }
  }
}
