using DDaS.Core.Compilers;
using DDaS.Core.Compilers.API;
using Xunit;
using System.Linq;

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
    }
}