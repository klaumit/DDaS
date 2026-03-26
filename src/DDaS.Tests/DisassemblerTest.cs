using DDaS.Core.Disassemblers;
using DDaS.Core.Disassemblers.API;
using Xunit;
using System.Linq;
using static System.Enum;
using ID = DDaS.Core.Disassemblers.API.DisassembleId;
using AOR = System.ArgumentOutOfRangeException;

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

        public static TheoryData<ID> ArgData => new(GetValues<ID>());

        [Theory]
        [MemberData(nameof(ArgData))]
        public void TestDisassembler(ID id)
        {
            if (id == default)
            {
                Assert.Throws<AOR>(() => Da.GetDisassembler(id));
                return;
            }
            var obj = Da.GetDisassembler(id);
            Assert.NotNull(obj);
        }
    }
}