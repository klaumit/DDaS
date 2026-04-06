using Xunit;
using System.Linq;
using System.Threading.Tasks;
using DDaS.Core.Tools;
using DDaS.IO.API;
using DDaS.IO.Tools;
using DDaS.Tests.Tools;
using DDaS.Tools;
using static System.Enum;
using ID = DDaS.Core.Assemblers.API.AssembleId;
using AOR = System.ArgumentOutOfRangeException;
using TU = DDaS.Core.Assemblers.Assemblers;

namespace DDaS.Tests
{
    public class AssemblerTest
    {
        public static TheoryData<ID> ArgData => new(GetValues<ID>());

        [Fact]
        public void TestInfos()
        {
            var da = ObjTool.New<TU>();

            var infos = da.ListAssemblerInfo()
                .Select(i => Parse<ID>(i.Id!)).ToArray();
            var args = ArgData.Cast<ID>()
                .Except([default]).Select(i => i).ToArray();
            Assert.Equal(infos, args);
        }

        [Theory]
        [MemberData(nameof(ArgData))]
        public async Task TestAssembler(ID id)
        {
            var da = ObjTool.New<TU>();

            if (id == default)
            {
                Assert.Throws<AOR>(() => da.GetAssembler(id));
                return;
            }

            var name = id switch { ID.NSM => "hello.asm", _ => "" };
            var obj = da.GetAssembler(id);
            var (path, bytes) = ResTool.Load(name);
            using var td = Files.NewTmpDir();
            using var input = Files.NewMemFile(path, bytes, Defaults.Octet, td);

            var exec = await obj.Assemble(input);

            Assert.Equal("hello.com", exec.File.Name);
            Assert.Equal(26, exec.File.Bytes.Length);
            Assert.Equal(Mimes.ComFile, exec.File.Mime);
            Assert.Equal(0, exec.Exit);
            Assert.True(exec.Ms >= 1);
            Assert.Null(exec.Out.TrimOrNull());
        }
    }
}