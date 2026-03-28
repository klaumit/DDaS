using System;
using System.Threading.Tasks;
using DDaS.Core.Assemblers.API;
using DDaS.Tests.Web.Tools;
using Microsoft.AspNetCore.Http;
using Xunit;
using C = DDaS.Server.Controllers.AssembleController;

namespace DDaS.Tests.Web
{
    public class AssembleTest
    {
        private static readonly C Da = LoadTool.New<C>();

        [Fact]
        public void TestGet()
        {
            var res = Da.Get();
            throw new InvalidOperationException($"{res} ?!");
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