using System;
using DDaS.Tests.Web.Tools;
using Xunit;
using System.Threading.Tasks;
using DDaS.Core.Compilers.API;
using Microsoft.AspNetCore.Http;
using C = DDaS.Server.Controllers.CompileController;

namespace DDaS.Tests.Web
{
    public class CompileTest
    {
        private static readonly C Da = LoadTool.New<C>();

        [Fact]
        public void TestGet()
        {
            var res = Da.Get();
            throw new InvalidOperationException($"{res} ?!");
        }

        [Fact]
        public async Task TestCompileAsm()
        {
            var res = await Da.CompileAsm(CompileId.B30,
                new FormFile(null, 0, 0, "?", ""));
            throw new InvalidOperationException($"{res} ?!");
        }

        [Fact]
        public async Task TestCompileCom()
        {
            var res = await Da.CompileCom(CompileId.B31,
                new FormFile(null, 0, 0, "?", ""));
            throw new InvalidOperationException($"{res} ?!");
        }
    }
}