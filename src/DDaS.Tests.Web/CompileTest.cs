using DDaS.Tests.Web.Tools;
using Xunit;
using System.Threading.Tasks;
using System.Linq;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using Microsoft.AspNetCore.Mvc;
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
            var fake = Da.FindToaster();
            var ctx = fake.SetHttpCtx(Da);

            var bytes = new byte[0x90];
            var res = await Da.CompileAsm(ID.B30, bytes.AsFile("hello.com"));

            var exec = ctx.GetExecuted((FileContentResult)res);
            Assert.Equal("hello.com", exec.File.Name);
            Assert.Equal(0, exec.File.Bytes.Length);
            Assert.Equal(Defaults.Octet, exec.File.Mime + "");
            Assert.Equal(1, exec.Exit);
            Assert.True(exec.Ms >= 1);
            Assert.Null(exec.Out);
        }

        [Fact]
        public async Task TestCompileCom()
        {
            var fake = Da.FindToaster();
            var ctx = fake.SetHttpCtx(Da);

            var bytes = new byte[0x90];
            var res = await Da.CompileCom(ID.B31, bytes.AsFile("hello.com"));

            var exec = ctx.GetExecuted((FileContentResult)res);
            Assert.Equal("hello.com", exec.File.Name);
            Assert.Equal(0, exec.File.Bytes.Length);
            Assert.Equal(Defaults.Octet, exec.File.Mime + "");
            Assert.Equal(1, exec.Exit);
            Assert.True(exec.Ms >= 1);
            Assert.Null(exec.Out);
        }
    }
}