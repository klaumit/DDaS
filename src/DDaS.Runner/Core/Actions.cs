using System;
using System.Threading.Tasks;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Compilers.API;
using DDaS.Core.Disassemblers.API;
using DDaS.Core.Models;
using DDaS.Server.Controllers;
using DDaS.Tests.Web.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            await RunThis<AssembleController, AssembleId>(o,
                (c, i, f) => c.Assemble(i, f),
                c => c.AllAssembleIds()
            );
        }

        public static async Task RunDisassemble(Options o)
        {
            await RunThis<DisassembleController, DisassembleId>(o,
                (c, i, f) => c.Disassemble(i, f),
                c => c.AllDisassembleIds()
            );
        }

        private static async Task RunThis<TC, TI>(Options o, Func<TC, TI, IFormFile?, Task<IActionResult>> run,
            Func<TC, OkObjectResult> inf) where TC : ControllerBase where TI : struct, Enum
        {
            var ctrl = ConTool.New<TC>();
            if (!Enum.TryParse<TI>(o.Kind, true, out var id))
            {
                DbgTool.Print(inf(ctrl).Cast<ToolInfo[]>());
                return;
            }
            var file = DbgTool.GetFileObj(o.InputFile).ToFormFile();
            var fake = ctrl.FindToaster();
            var ctx = fake.SetHttpCtx(ctrl);
            var kind = await run(ctrl, id, file);
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