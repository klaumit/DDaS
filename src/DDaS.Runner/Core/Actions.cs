using System;
using DDaS.Server.Controllers;
using Newtonsoft.Json;

namespace DDaS.Runner.Core
{
    public static class Actions
    {
        public static void RunCompile(Options o)
        {
            var ctrl = LoadTool.New<CompileController>();
            Console.WriteLine(ctrl);
            Console.WriteLine(JsonConvert.SerializeObject(ctrl.AllCompileIds().Value));
        }
    }
}