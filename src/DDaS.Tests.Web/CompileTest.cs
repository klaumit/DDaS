using System;
using DDaS.Tests.Web.Tools;
using Xunit;
using C = DDaS.Server.Controllers.CompileController;

namespace DDaS.Tests.Web
{
    public class CompileTest
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