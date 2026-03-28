using System;
using DDaS.Tests.Web.Tools;
using Xunit;
using System.Threading.Tasks;
using DDaS.Core.Compilers.API;
using Microsoft.AspNetCore.Http;
using System.Linq;
using DDaS.Core.Models;
using static System.Enum;
using ID = DDaS.Core.Compilers.API.CompileId;
using C = DDaS.Server.Controllers.CompileController;

namespace DDaS.Tests.Web
{
    public class CompileTest
    {
        private static readonly C Da = LoadTool.New<C>();
        public static TheoryData<ID> ArgData => new(GetValues<ID>());

        [Fact]
        public void TestGet()
        {
            var res = Da.Get();
            Assert.Equal(200, res.StatusCode);

            var infos = ((ToolInfo[])res.Value!)
                .Select(i => Parse<ID>(i.Id!)).ToArray();
            var args = ArgData.Cast<ID>()
                .Except([default]).Select(i => i).ToArray();
            Assert.Equal(infos, args);
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