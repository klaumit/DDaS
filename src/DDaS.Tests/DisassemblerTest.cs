using DDaS.Core.Disassemblers;
using DDaS.Core.Disassemblers.API;
using Xunit;
using System.Linq;
using static System.Enum;

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

        public static TheoryData<DisassembleId> StatusData => new(GetValues<DisassembleId>());

        [Theory]
        [MemberData(nameof(StatusData))]
        public void TestDisassembler(DisassembleId id)
        {
            if (id == default) return;
            var obj = Da.GetDisassembler(id);
            Assert.NotNull(obj);
        }
    }
}