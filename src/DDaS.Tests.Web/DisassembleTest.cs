using System;
using DDaS.Tests.Web.Tools;
using Xunit;
using C = DDaS.Server.Controllers.DisassembleController;

namespace DDaS.Tests.Web
{
    public class DisassembleTest
    {
        private static readonly C Da = LoadTool.New<C>();

        [Fact]
        public void TestInfos()
        {
            var res = Da.Get();
            throw new InvalidOperationException($"{res} ?!");
        }
    }
}