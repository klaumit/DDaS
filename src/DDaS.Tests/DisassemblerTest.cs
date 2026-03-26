using System.Linq;
using DDaS.Core.Disassemblers;
using DDaS.Core.Disassemblers.API;
using Xunit;

namespace DDaS.Tests
{
    public class DisassemblerTest
    {
        private static readonly IDisassemblers Da = new Disassemblers();

        [Fact]
        public void TestInfos()
        {
            var infos = Da.ListDisassemblerInfo();
            var array = infos.ToArray();
            Assert.Single(array);
        }
    }
}