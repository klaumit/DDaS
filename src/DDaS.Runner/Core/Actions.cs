using System;
using System.Threading.Tasks;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Compilers.API;
using DDaS.Core.Disassemblers.API;
using DDaS.Core.Models;
using DDaS.Server.Controllers;
using DDaS.Tools.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDaS.Runner.Core
{
    public static class Actions
    {
        public static async Task RunCompile(Options o)
        {
            Enum.TryParse<CompKind>(o.Mode, true, out var mod);
            await RunThis<CompileController, CompileId>(o,
                (c, i, f) =>
                {
                    switch (mod)
                    {
                        case CompKind.Asm: return c.CompileAsm(i, f);
                        default: return c.CompileCom(i, f);
                    }
                },
                c => c.AllCompileIds()
            );
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