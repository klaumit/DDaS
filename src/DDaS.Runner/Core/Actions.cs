using DDaS.Core.Models;
using DDaS.Server.Controllers;

namespace DDaS.Runner.Core
{
    public static class Actions
    {
        public static void RunCompile(Options o)
        {
            var ctrl = ConTool.New<CompileController>();
            DbgTool.Print(ctrl.AllCompileIds().Cast<ToolInfo[]>());
        }

        public static void RunAssemble(Options o)
        {
            var ctrl = ConTool.New<AssembleController>();
            DbgTool.Print(ctrl.AllAssembleIds().Cast<ToolInfo[]>());
        }

        public static void RunDisassemble(Options o)
        {
            var ctrl = ConTool.New<DisassembleController>();
            DbgTool.Print(ctrl.AllDisassembleIds().Cast<ToolInfo[]>());
        }
    }
}