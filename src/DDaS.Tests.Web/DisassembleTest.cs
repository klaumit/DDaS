using DDaS.Tests.Web.Tools;
using Xunit;
using System.Threading.Tasks;
using System.Linq;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using Microsoft.AspNetCore.Mvc;
using static System.Enum;
using ID = DDaS.Core.Disassemblers.API.DisassembleId;
using C = DDaS.Server.Controllers.DisassembleController;

namespace DDaS.Tests.Web
{
    public class DisassembleTest
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
        public async Task TestDisassemble()
        {
            var fake = Da.FindToaster();
            var ctx = fake.SetHttpCtx(Da);

            byte[] bytes = [0x90, 0x35, 0x73, 0x92];
            var res = await Da.Assemble(ID.NSM, bytes.AsFile("hello.com"));

            var exec = ctx.GetExecuted((FileContentResult)res);
            Assert.Equal("hello.s", exec.File.Name);
            Assert.Equal(78, exec.File.Bytes.Length);
            Assert.Equal(Defaults.Octet, exec.File.Mime + "");
            Assert.Equal(0, exec.Exit);
            Assert.Equal(1, exec.Ms);
            Assert.Null(exec.Out);
        }
    }
}