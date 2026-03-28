using DDaS.Core.Assemblers;
using DDaS.Core.Assemblers.API;
using Xunit;
using System.Linq;
using static System.Enum;
using ID = DDaS.Core.Assemblers.API.AssembleId;
using AOR = System.ArgumentOutOfRangeException;

namespace DDaS.Tests
{
    public class AssemblerTest
    {
        private static readonly IAssemblers Da = new Assemblers();
        public static TheoryData<ID> ArgData => new(GetValues<ID>());

        [Fact]
        public void TestInfos()
        {
            var infos = Da.ListAssemblerInfo()
                .Select(i => Parse<ID>(i.Id!)).ToArray();
            var args = ArgData.Cast<ID>()
                .Except([default]).Select(i => i).ToArray();
            Assert.Equal(infos, args);
        }

        [Theory]
        [MemberData(nameof(ArgData))]
        public void TestAssembler(ID id)
        {
            if (id == default)
            {
                Assert.Throws<AOR>(() => Da.GetAssembler(id));
                return;
            }
            var obj = Da.GetAssembler(id);
            Assert.NotNull(obj);
        }
    }
}