using System.Linq;
using System.Threading.Tasks;
using DDaS.Core.Models;
using Xunit;
using DDaS.Core.Tools;
using DDaS.Tests.Web.Tools;
using DDaS.Tools;
using DDaS.Tools.Web;
using Microsoft.AspNetCore.Mvc;
using static System.Enum;
using ID = DDaS.Core.Assemblers.API.AssembleId;
using C = DDaS.Server.Controllers.AssembleController;

namespace DDaS.Tests.Web
{
    public class AssembleTest
    {
        public static TheoryData<ID> ArgData => new(GetValues<ID>());

        [Fact]
        public void TestAssembleIds()
        {
            var da = WebTool.New<C>();

            var res = da.AllAssembleIds();
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
            var da = WebTool.New<C>();

            var res = await da.Assemble(ID.NSM, null);
            Assert.Equal("BadRequestObjectResult", res.GetType().Name);
        }

        [Theory]
        [InlineData("hello.asm")]
        public async Task TestAssemble(string name)
        {
            var da = WebTool.New<C>();

            var fake = da.FindToaster();
            var ctx = fake.SetHttpCtx(da);

            var (_, bytes) = ResTool.Load(name);
            var res = await da.Assemble(ID.NSM, bytes.AsFile(name));

            var exec = ctx.GetExecuted((FileContentResult)res);
            Assert.Equal("hello.com", exec.File.Name);
            Assert.Equal(26, exec.File.Bytes.Length);
            Assert.Equal(Defaults.Octet, exec.File.Mime);
            Assert.Equal(0, exec.Exit);
            Assert.True(exec.Ms >= 1);
            Assert.Null(exec.Out);
        }
    }
}