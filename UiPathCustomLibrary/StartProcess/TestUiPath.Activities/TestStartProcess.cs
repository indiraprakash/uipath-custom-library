using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Activities;
using StartProcess;
using System.Collections.Generic;

namespace TestUiPath.Activities
{
    [TestClass]
    public class TestStartProcess
    {
        [TestMethod]
        public void TestMethod1()
        {
            Dictionary<string, object> wfParams = new Dictionary<string, object>();
            wfParams.Add("FileName", @"C:\Users\TAASUTH5\Downloads\UiPathStudio20.10.6.msi");
            wfParams.Add("Arguments", "/?");
            var output = WorkflowInvoker.Invoke(new StartProcess.StartProcess());
        }
    }
}
