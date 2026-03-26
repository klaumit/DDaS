using DDaS.Core.Compilers;
using DDaS.Core.Compilers.API;
using Xunit;
using System.Linq;
using static System.Enum;

namespace DDaS.Tests
{
    public class CompilerTest
    {
        private static readonly ICompilers Da = new Compilers();

        [Fact]
        public void TestInfos()
        {
            var infos = Da.ListCompilerInfo();
            var array = infos.ToArray();
            Assert.Equal(5, array.Length);
        }

        public static TheoryData<CompileId> StatusData => new(GetValues<CompileId>());

        [Theory]
        [MemberData(nameof(StatusData))]
        public void TestCompiler(CompileId id)
        {
            if (id == default) return;
            var obj = Da.GetCompiler(id);
            Assert.NotNull(obj);
        }
    }
}