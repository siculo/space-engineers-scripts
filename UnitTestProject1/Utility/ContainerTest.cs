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
      private ResourceType Ice = new ResourceType("", "Ice");
      private ResourceType Iron = new ResourceType("", "Iron");
      private ResourceType Gold = new ResourceType("", "Gold");
      private ResourceType Silver = new ResourceType("", "Silver");

      [TestMethod]
      public void TestResources()
      {
        Container container = new TestContainer(new Resource(Ice));
        Summary summary = new Summary(container.GetResources());
        Assert.IsTrue(Enumerable.SequenceEqual(new List<Resource>() { new Resource(Ice) }, summary.GetResources()));
      }

      [TestMethod]
      public void TestSummaryEquals()
      {
        Assert.AreEqual(
          new Summary(new List<Resource>() { new Resource(Ice), new Resource(Gold, 22), new Resource(Iron, 7) }),
          new Summary(new List<Resource>() { new Resource(Iron, 7), new Resource(Ice), new Resource(Gold, 22) })
          );
      }

      [TestMethod]
      public void TestConsolidateSummary()
      {
        Summary summary = new Summary(new List<Resource>() { new Resource(Ice, 22), new Resource(Ice, 14) });
        Assert.IsTrue(Enumerable.SequenceEqual(new List<Resource>() { new Resource(Ice, 14 + 22) }, summary.GetResources()));
      }

      [TestMethod]
      public void TestSummaryAddition()
      {
        Summary summaryA = new Summary(new List<Resource>() { new Resource(Ice, 20) });
        Summary summaryB = new Summary(new List<Resource>() { new Resource(Ice, 50) });
        Summary summaryC = summaryA + summaryB;
        Assert.IsTrue(Enumerable.SequenceEqual(new List<Resource>() { new Resource(Ice, 70) }, summaryC.GetResources()));
      }

      [TestMethod]
      public void TestFullSummary()
      {
        Container empty = new TestContainer();
        Container oneResource = new TestContainer(new Resource(Ice, 40));
        Container twoResources = new TestContainer(new Resource(Iron, 15), new Resource(Gold, 25));
        Container someResources = new TestContainer(
          new Resource(Iron, 5),
          new Resource(Ice, 8),
          new Resource(Gold, 2),
          new Resource(Silver, 14)
        );
        Container[] containers = new Container[]
        {
          empty, oneResource, twoResources, someResources
        };

        Summary summary = Summary.ContainersSummary(containers);
        Assert.AreEqual(
          new Summary(new Resource[] {
            new Resource(Ice, 48),
            new Resource(Iron, 20),
            new Resource(Gold, 27),
            new Resource(Silver, 14)
          }),
          summary
          );
        Assert.AreEqual(48, summary.GetMaximum());
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
