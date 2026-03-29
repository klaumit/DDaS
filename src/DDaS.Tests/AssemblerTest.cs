using DDaS.Core.Assemblers;
using DDaS.Core.Assemblers.API;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using DDaS.Core.Tools;
using DDaS.Tests.Tools;
using DDaS.Tests.Web.Tools;
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
        public async Task TestAssembler(ID id)
        {
            if (id == default)
            {
                Assert.Throws<AOR>(() => Da.GetAssembler(id));
                return;
            }

            var name = id switch { ID.NSM => "hello.asm", _ => "" };
            var obj = Da.GetAssembler(id);
            var (path, bytes) = ResTool.Load(name);
            var input = new MemFile(path, bytes, Defaults.Octet);

            var exec = await obj.Assemble(input);

            Assert.Equal("hello.com", exec.File.Name);
            Assert.Equal(26, exec.File.Bytes.Length);
            Assert.Equal(Defaults.Octet, exec.File.Mime);
            Assert.Equal(0, exec.Exit);
            Assert.True(exec.Ms >= 1);
            Assert.Null(exec.Out.TrimOrNull());
        }
    }
}