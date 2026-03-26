using DDaS.Core.Assemblers;
using DDaS.Core.Assemblers.API;
using Xunit;
using System.Linq;
using static System.Enum;

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

        public static TheoryData<AssembleId> StatusData => new(GetValues<AssembleId>());

        [Theory]
        [MemberData(nameof(StatusData))]
        public void TestAssembler(AssembleId id)
        {
            if (id == default) return;
            var obj = Da.GetAssembler(id);
            Assert.NotNull(obj);
        }
    }
}