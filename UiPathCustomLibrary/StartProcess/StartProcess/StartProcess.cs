using System;
using System.Activities;
using System.Diagnostics;

namespace StartProcess
{   /// <summary>
    /// Start the process and get the output from standard output 
    /// </summary>
    public class StartProcess : CodeActivity
    {
        public InArgument<String> FileName { get; set; }
        public InArgument<String> Arguments { get; set; }
        public OutArgument<String> Result { get; set; }

        protected override void Execute(CodeActivityContext context)
        {


            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = FileName.Get(context);
                    process.StartInfo.Arguments = Arguments.Get(context);
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    // To store error message 
                    String eOut = null;
                    process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) => { eOut += e.Data; });
                    //Start the process
                    process.Start();
                    // Synchronously read the standard output of the spawned process.
                    process.BeginErrorReadLine();
                    // Store output of the process
                    String output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    if (String.IsNullOrEmpty(eOut))
                    {
                        Result.Set(context, output);
                    }
                    else
                    {
                        Result.Set(context, eOut);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
