using DDaS.Core.Assemblers;
using DDaS.Core.Assemblers.API;
using Xunit;
using System.Linq;

namespace DDaS.Tests
{
    public class AssemblerTest
    {
        private static readonly IAssemblers Da = new Assemblers();

        [Fact]
        public void TestInfos()
        {
            var infos = Da.ListAssemblerInfo();
            var array = infos.ToArray();
            Assert.Single(array);
        }
    }
}