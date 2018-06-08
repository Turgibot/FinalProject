﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;
using System.IO;
using System.Diagnostics;

namespace CSA_Project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string path = @"C:\FinalProject\CSA_Project\CSA_Project\Python\streamer.py";
            //            string expression =
            //                @"import datetime
            //print(datetime.datetime.now())";
            //            var engine = Python.CreateEngine();
            //            var source = engine.CreateScriptSourceFromFile(@"D:\Projects\FinalProject\CSA_Project\CSA_Project\Python\Inference.py");
            //            var compiled = source.Compile();
            //            var scope = engine.CreateScope();
            //            compiled.Execute(scope);
            //var p = PatchParameter("Guy", 1);
            //var s = run_cmd(path, "");
            return View();
        }

        public string PatchParameter(string parameter, int serviceid)
        {
            var engine = Python.CreateEngine(); // Extract Python language engine from their grasp
            var scope = engine.CreateScope(); // Introduce Python namespace (scope)
            var d = new Dictionary<string, object>
            {
                { "serviceid", serviceid},
                { "parameter", parameter}
            }; // Add some sample parameters. Notice that there is no need in specifically setting the object type, interpreter will do that part for us in the script properly with high probability

            scope.SetVariable("params", d); // This will be the name of the dictionary in python script, initialized with previously created .NET Dictionary
            ScriptSource source = engine.CreateScriptSourceFromFile(@"C:\FinalProject\CSA_Project\CSA_Project\Python\streamer.py"); // Load the script
            object result = source.Execute(scope);
            var dr = new Dictionary<string, object>();
            dr = scope.GetVariable("params"); // To get the finally set variable 'parameter' from the python script
            return parameter;
        }
        public string run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Python34\python.exe"; //"PATH_TO_PYTHON_EXE";
            start.Arguments = string.Format("\"{0}\" \"{1}\"", cmd, args);
            start.UseShellExecute = false;// Do not use OS shell
            start.CreateNoWindow = true; // We don't need new window
            start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
            start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    //string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                    string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                    return result;
                }
            }
        }
    }
}
