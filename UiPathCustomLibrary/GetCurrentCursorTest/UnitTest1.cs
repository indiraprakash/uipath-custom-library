using System;
using System.Activities;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GetCurrentCursorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int counter = 0;
            while (counter < 10)
            {
                counter = counter + 1;
                Thread.Sleep(1000);
                var output = WorkflowInvoker.Invoke(new GetCurrentCursor.GetCursorType());
                Debug.WriteLine(output["CursorType"]);



            }
        }
    }
}
