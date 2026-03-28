using System;
using DDaS.Tests.Web.Tools;
using Xunit;
using System.Threading.Tasks;
using DDaS.Core.Disassemblers.API;
using Microsoft.AspNetCore.Http;
using C = DDaS.Server.Controllers.DisassembleController;

namespace DDaS.Tests.Web
{
    public class DisassembleTest
    {
        private static readonly C Da = LoadTool.New<C>();

        [Fact]
        public void TestGet()
        {
            var res = Da.Get();
            throw new InvalidOperationException($"{res} ?!");
        }

        [Fact]
        public async Task TestDisassemble()
        {
            var res = await Da.Assemble(DisassembleId.NSM,
                new FormFile(null, 0, 0, "?", ""));
            throw new InvalidOperationException($"{res} ?!");
        }
    }
}