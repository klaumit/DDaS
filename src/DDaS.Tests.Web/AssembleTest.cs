using System;
using System.Linq;
using System.Threading.Tasks;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Models;
using DDaS.Tests.Web.Tools;
using Microsoft.AspNetCore.Http;
using Xunit;
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
        public async Task TestAssemble()
        {
            var res = await Da.Assemble(AssembleId.NSM,
                new FormFile(null, 0, 0, "?", ""));
            throw new InvalidOperationException($"{res} ?!");
        }
    }
}