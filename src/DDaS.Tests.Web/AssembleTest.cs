using System.Linq;
using System.Threading.Tasks;
using DDaS.Core.Models;
using DDaS.Tests.Web.Tools;
using Xunit;
using DDaS.Core.Tools;
using DDaS.Tests.Tools;
using Microsoft.AspNetCore.Mvc;
using static System.Enum;
using ID = DDaS.Core.Assemblers.API.AssembleId;
using C = DDaS.Server.Controllers.AssembleController;

namespace DDaS.Tests.Web
{
    public class AssembleTest
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
        public async Task TestAssembleFail()
        {
            var res = await Da.Assemble(ID.NSM, null);
            Assert.Equal("BadRequestObjectResult", res.GetType().Name);
        }

        [Theory]
        [InlineData("hello.asm")]
        public async Task TestAssemble(string name)
        {
            var fake = Da.FindToaster();
            var ctx = fake.SetHttpCtx(Da);

            var bytes = ResTool.Load(name);
            var res = await Da.Assemble(ID.NSM, bytes.AsFile(name));

            var exec = ctx.GetExecuted((FileContentResult)res);
            Assert.Equal("hello.com", exec.File.Name);
            Assert.Equal(26, exec.File.Bytes.Length);
            Assert.Equal(Defaults.Octet, exec.File.Mime + "");
            Assert.Equal(0, exec.Exit);
            Assert.True(exec.Ms >= 1);
            Assert.Null(exec.Out);
        }
    }
}