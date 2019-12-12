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
      public void TestResources()
      {
        Container container = new TestContainer(new Resource(ResourceType.Ice));
        Summary summary = new Summary(container.GetResources());
        Assert.IsTrue(Enumerable.SequenceEqual(new List<Resource>() { new Resource(ResourceType.Ice) }, summary.GetResources()));
      }

      [TestMethod]
      public void TestSummaryEquals()
      {
        Assert.AreEqual(
          new Summary(new List<Resource>() { new Resource(ResourceType.Ice), new Resource(ResourceType.Gold, 22), new Resource(ResourceType.Iron, 7) }),
          new Summary(new List<Resource>() { new Resource(ResourceType.Iron, 7), new Resource(ResourceType.Ice), new Resource(ResourceType.Gold, 22) })
          );
      }

      [TestMethod]
      public void TestConsolidateSummary()
      {
        Summary summary = new Summary(new List<Resource>() { new Resource(ResourceType.Ice, 22), new Resource(ResourceType.Ice, 14) });
        Assert.IsTrue(Enumerable.SequenceEqual(new List<Resource>() { new Resource(ResourceType.Ice, 14 + 22) }, summary.GetResources()));
      }

      [TestMethod]
      public void TestSummaryAddition()
      {
        Summary summaryA = new Summary(new List<Resource>() { new Resource(ResourceType.Ice, 20) });
        Summary summaryB = new Summary(new List<Resource>() { new Resource(ResourceType.Ice, 50) });
        Summary summaryC = summaryA + summaryB;
        Assert.IsTrue(Enumerable.SequenceEqual(new List<Resource>() { new Resource(ResourceType.Ice, 70) }, summaryC.GetResources()));
      }

      [TestMethod]
      public void TestFullSummary()
      {
        Container empty = new TestContainer();
        Container oneResource = new TestContainer(new Resource(ResourceType.Ice, 40));
        Container twoResources = new TestContainer(new Resource(ResourceType.Iron, 15), new Resource(ResourceType.Gold, 25));
        Container someResources = new TestContainer(
          new Resource(ResourceType.Iron, 5),
          new Resource(ResourceType.Ice, 8),
          new Resource(ResourceType.Gold, 2),
          new Resource(ResourceType.Silver, 14)
        );
        Container[] containers = new Container[]
        {
          empty, oneResource, twoResources, someResources
        };

        Summary summary = Summary.ContainersSummary(containers);
        Assert.AreEqual(
          new Summary(new Resource[] {
            new Resource(ResourceType.Ice, 48),
            new Resource(ResourceType.Iron, 20),
            new Resource(ResourceType.Gold, 27),
            new Resource(ResourceType.Silver, 14)
          }),
          summary
          );
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
