using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;
using System.IO;
namespace CSA_Project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string expression =
                @"import datetime
print(datetime.datetime.now())";
            var engine = Python.CreateEngine();
            var source = engine.CreateScriptSourceFromFile(@"D:\Projects\FinalProject\CSA_Project\CSA_Project\Python\Inference.py");
            var compiled = source.Compile();
            var scope = engine.CreateScope();
            compiled.Execute(scope);

            return View();
        }
    }
}
