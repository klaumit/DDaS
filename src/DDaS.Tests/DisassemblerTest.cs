using DDaS.Core.Disassemblers;
using DDaS.Core.Disassemblers.API;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using DDaS.Core.Tools;
using DDaS.Tests.Tools;
using DDaS.Tests.Web.Tools;
using static System.Enum;
using ID = DDaS.Core.Disassemblers.API.DisassembleId;
using AOR = System.ArgumentOutOfRangeException;

namespace DDaS.Tests
{
    public class DisassemblerTest
    {
        private static readonly IDisassemblers Da = new Disassemblers();
        public static TheoryData<ID> ArgData => new(GetValues<ID>());

        [Fact]
        public void TestInfos()
        {
            var infos = Da.ListDisassemblerInfo()
                .Select(i => Parse<ID>(i.Id!)).ToArray();
            var args = ArgData.Cast<ID>()
                .Except([default]).Select(i => i).ToArray();
            Assert.Equal(infos, args);
        }

        [Theory]
        [MemberData(nameof(ArgData))]
        public async Task TestDisassembler(ID id)
        {
            if (id == default)
            {
                Assert.Throws<AOR>(() => Da.GetDisassembler(id));
                return;
            }

            var name = id switch { ID.NSM or ID.ICE or ID.O16 => "hello.com", _ => "" };
            var obj = Da.GetDisassembler(id);
            var (path, bytes) = ResTool.Load(name);
            var input = new MemFile(path, bytes, Defaults.Octet);

            var exec = await obj.Disassemble(input);

            Assert.Equal("hello.s", exec.File.Name);
            Assert.True(exec.File.Bytes.Length >= 183);
            Assert.Equal(Defaults.Octet, exec.File.Mime);
            Assert.Equal(0, exec.Exit);
            Assert.True(exec.Ms >= 1);
            Assert.Null(exec.Out.TrimOrNull());
        }
    }
}