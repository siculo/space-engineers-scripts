using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static IngameScript.Program;

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
    }
}
