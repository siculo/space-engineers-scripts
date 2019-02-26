using System;
using IngameScript;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sandbox.ModAPI;
using SpaceEngineers.Game.ModAPI;
using VRage.Game.ModAPI.Ingame.Utilities;

namespace IngameScript
{
    public class TestOutput : TextOuput
    {
        public string currentText = "";

        public void Set(string text)
        {
            currentText = text;
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestOutput output = new TestOutput();
            DisplayConsole displayConsole = new DisplayConsole(output, 15);
            displayConsole.Write("[uno-");
            displayConsole.Write("due\n");
            displayConsole.Write("tre]");

            string expected = "[uno-due" + Environment.NewLine + "tre]";
            string actual = output.currentText;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            TestOutput output = new TestOutput();
            DisplayConsole displayConsole = new DisplayConsole(output, 15);
            displayConsole.WriteLine("uno-due");
            displayConsole.WriteLine("tre");

            string expected = "uno-due" + Environment.NewLine + "tre";
            string actual = output.currentText;

            Assert.AreEqual(expected, actual);
        }

        private void Echo(string message)
        {

        }

        [TestMethod]
        public void TestMethod3()
        {
            IMyGridTerminalSystem GridTerminalSystem = null;
            IMyProgrammableBlock Me = null;

            MyIni _ini = new MyIni();
            MyIniParseResult result;
            if (!_ini.TryParse(Me.CustomData, out result))
            {
                Echo("c'è un problema: " + result.Success + " " + result.Error);
            }
            else
            {
                System.Collections.Generic.List<IMyAirVent> airlocks = new System.Collections.Generic.List<IMyAirVent>();
                Echo(_ini.Get("main", "sample").ToString());
                GridTerminalSystem.GetBlocksOfType<IMyAirVent>(airlocks, airlock => MyIni.HasSection(airlock.CustomData, "airlock"));
            }
        }
    }
}
