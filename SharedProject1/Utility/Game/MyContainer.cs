using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using VRageMath;
using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
  partial class Program
  {
    class MyContainer : Container
    {
      public Summary GetResourceSummary()
      {
        return new Summary();
      }
    }
  }
}
