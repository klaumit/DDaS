using System;
using System.Threading.Tasks;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Compilers.API;
using DDaS.Core.Disassemblers.API;
using DDaS.Core.Models;
using DDaS.Server.Controllers;
using DDaS.Tests.Web.Tools;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DDaS.Runner.Core
{
    public static class Actions
    {
        public static void RunCompile(Options o)
        {
            var ctrl = ConTool.New<CompileController>();
            if (Enum.TryParse<CompileId>(o.Kind, true, out var id))
            {

            }
            else
            {
                DbgTool.Print(ctrl.AllCompileIds().Cast<ToolInfo[]>());
            }
        }

        public static async Task RunAssemble(Options o)
        {
            var ctrl = ConTool.New<AssembleController>();
            if (Enum.TryParse<AssembleId>(o.Kind, true, out var id))
            {
                var kind = await ctrl.Assemble(id, null);
                Console.WriteLine(JsonConvert.SerializeObject(kind));
            }
            else
            {
                DbgTool.Print(ctrl.AllAssembleIds().Cast<ToolInfo[]>());
            }
        }

        public static async Task RunDisassemble(Options o)
        {
            var ctrl = ConTool.New<DisassembleController>();
            if (!Enum.TryParse<DisassembleId>(o.Kind, true, out var id))
            {
                DbgTool.Print(ctrl.AllDisassembleIds().Cast<ToolInfo[]>());
                return;
            }
            var file = DbgTool.GetFileObj(o.InputFile).ToFormFile();
            var fake = ctrl.FindToaster();
            var ctx = fake.SetHttpCtx(ctrl);
            var kind = await ctrl.Disassemble(id, file);
            if (!DbgTool.IsOk(kind, out var err))
            {
                Console.WriteLine(err);
                return;
            }
            var res = ctx.GetExecuted((FileContentResult)kind);
            DbgTool.Print(res);
        }
    }
}