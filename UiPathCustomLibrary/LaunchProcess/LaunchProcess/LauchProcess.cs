using System;
using System.Activities;
using System.Diagnostics;

namespace Swisscom.UiPath.Activities
{   /// <summary>
    /// Synchronously start the process and read the ouput stream
    /// </summary>
    public class LauchProcess : CodeActivity
    {
        public InArgument<string> FileName { get; set; }
        public InArgument<string> Arguments { get; set; }
        public OutArgument<string> Result { get; set; }

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
                    string eOut = null;
                    process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) => { eOut += e.Data; });
                    //Start the process
                    process.Start();
                    // Synchronously read the standard output of the spawned process.
                    process.BeginErrorReadLine();
                    // Store output of the process
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    if (string.IsNullOrEmpty(eOut))
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
