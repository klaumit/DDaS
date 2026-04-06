using Xunit;
using System.Linq;
using System.Threading.Tasks;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using DDaS.IO.API;
using DDaS.IO.Tools;
using DDaS.Tests.Tools;
using DDaS.Tools;
using static System.Enum;
using ID = DDaS.Core.Compilers.API.CompileId;
using AOR = System.ArgumentOutOfRangeException;
using TU = DDaS.Core.Compilers.Compilers;

namespace DDaS.Tests
{
    public class CompilerTest
    {
        public static TheoryData<ID> ArgData => new(GetValues<ID>());

        [Fact]
        public void TestInfos()
        {
            var da = ObjTool.New<TU>();

            var infos = da.ListCompilerInfo()
                .Select(i => Parse<ID>(i.Id!)).ToArray();
            var args = ArgData.Cast<ID>()
                .Except([default]).Select(i => i).ToArray();
            Assert.Equal(infos, args);
        }

        [Theory]
        [MemberData(nameof(ArgData))]
        public async Task TestCompileAsm(ID id)
        {
            var da = ObjTool.New<TU>();

            if (id == default)
            {
                Assert.Throws<AOR>(() => da.GetCompiler(id));
                return;
            }

            var name = id switch { ID.FPC => "hello.pas", _ => "hello.c" };
            var obj = da.GetCompiler(id);
            var (path, bytes) = ResTool.Load(name);
            using var td = Files.NewTmpDir();
            using var input = Files.NewMemFile(path, bytes, Defaults.Octet, td);

            var exec = await obj.CompileToAsm(input);

            Assert.True(exec.File.Name is "hello.asm" or "hello.s");
            Assert.True(exec.File.Bytes.Length >= 0);
            Assert.Equal(Mimes.AsmFile, exec.File.Mime);
            Assert.True(exec.Exit is 0 or 1);
            Assert.True(exec.Ms >= 1);
            // Assert.NotNull(exec.Out.TrimOrNull());
        }

        [Theory]
        [MemberData(nameof(ArgData))]
        public async Task TestCompileCom(ID id)
        {
            var da = ObjTool.New<TU>();

            if (id == default)
            {
                Assert.Throws<AOR>(() => da.GetCompiler(id));
                return;
            }

            var name = id switch { ID.FPC => "hello.pas", _ => "hello.c" };
            var obj = da.GetCompiler(id);
            var (path, bytes) = ResTool.Load(name);
            using var td = Files.NewTmpDir();
            using var input = Files.NewMemFile(path, bytes, Defaults.Octet, td);

            var exec = await obj.CompileToCom(input);

            Assert.Equal("hello.com", exec.File.Name);
            Assert.True(exec.File.Bytes.Length >= 0);
            Assert.Equal(Mimes.ComFile, exec.File.Mime);
            Assert.True(exec.Exit is 0 or 1);
            Assert.True(exec.Ms >= 1);
            // Assert.Null(exec.Out.TrimOrNull());
        }
    }
}