using System;
using DDaS.Server.Controllers;
using Newtonsoft.Json;

namespace DDaS.Runner.Core
{
    public static class Actions
    {
        public static void RunCompile(Options o)
        {
            var ctrl = ConTool.New<CompileController>();
            Console.WriteLine(ctrl);
            Console.WriteLine(JsonConvert.SerializeObject(ctrl.AllCompileIds().Value));
        }

        public static void RunAssemble(Options o)
        {
            var ctrl = ConTool.New<AssembleController>();
            Console.WriteLine(ctrl);
            Console.WriteLine(JsonConvert.SerializeObject(ctrl.AllAssembleIds().Value));
        }

        public static void RunDisassemble(Options o)
        {
            var ctrl = ConTool.New<DisassembleController>();
            Console.WriteLine(ctrl);
            Console.WriteLine(JsonConvert.SerializeObject(ctrl.AllDisassembleIds().Value));
        }
    }
}