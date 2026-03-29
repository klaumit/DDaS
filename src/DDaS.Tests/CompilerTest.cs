using DDaS.Core.Compilers;
using DDaS.Core.Compilers.API;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using DDaS.Tests.Tools;
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
        public async Task TestCompileAsm(ID id)
        {
            if (id == default)
            {
                Assert.Throws<AOR>(() => Da.GetCompiler(id));
                return;
            }

            var name = id switch { ID.FPC => "hello.pas", _ => "hello.c" };
            var obj = Da.GetCompiler(id);
            var (path, bytes) = ResTool.Load(name);
            var input = new MemFile(path, bytes, Defaults.Octet);

            var exec = await obj.CompileToAsm(input);

            Assert.True(exec.File.Name is "hello.asm" or "hello.s");
            Assert.True(exec.File.Bytes.Length >= 135);
            Assert.Equal(Defaults.Octet, exec.File.Mime);
            Assert.True(exec.Exit is 0 or 1);
            Assert.True(exec.Ms >= 1);
            // Assert.NotNull(exec.Out.TrimOrNull());
        }

        [Theory]
        [MemberData(nameof(ArgData))]
        public async Task TestCompileCom(ID id)
        {
            if (id == default)
            {
                Assert.Throws<AOR>(() => Da.GetCompiler(id));
                return;
            }

            var name = id switch { ID.FPC => "hello.pas", _ => "hello.c" };
            var obj = Da.GetCompiler(id);
            var (path, bytes) = ResTool.Load(name);
            var input = new MemFile(path, bytes, Defaults.Octet);

            var exec = await obj.CompileToCom(input);

            Assert.Equal("hello.com", exec.File.Name);
            Assert.True(exec.File.Bytes.Length >= 6046);
            Assert.Equal(Defaults.Octet, exec.File.Mime);
            Assert.True(exec.Exit is 0 or 1);
            Assert.True(exec.Ms >= 1);
            // Assert.Null(exec.Out.TrimOrNull());
        }
    }
}