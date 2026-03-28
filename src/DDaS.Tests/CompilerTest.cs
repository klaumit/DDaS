using DDaS.Core.Compilers;
using DDaS.Core.Compilers.API;
using Xunit;
using System.Linq;
using static System.Enum;
using ID = DDaS.Core.Compilers.API.CompileId;
using AOR = System.ArgumentOutOfRangeException;

namespace DDaS.Tests
{
    public class CompilerTest
    {
        private static readonly ICompilers Da = new Compilers();
        public static TheoryData<ID> ArgData => new(GetValues<ID>());

        [Fact]
        public void TestInfos()
        {
            var infos = Da.ListCompilerInfo()
                .Select(i => Parse<ID>(i.Id!)).ToArray();
            var args = ArgData.Cast<ID>()
                .Except([default]).Select(i => i).ToArray();
            Assert.Equal(infos, args);
        }

        [Theory]
        [MemberData(nameof(ArgData))]
        public void TestCompiler(ID id)
        {
            if (id == default)
            {
                Assert.Throws<AOR>(() => Da.GetCompiler(id));
                return;
            }
            var obj = Da.GetCompiler(id);
            Assert.NotNull(obj);
        }
    }
}