using System.Threading.Tasks;
using DDaS.Core.API;
using DDaS.Core.Models;
using DDaS.Core.Tools;

namespace DDaS.Core.Impl
{
    public sealed class BCpp20 : DosBased, ICompiler
    {
        public async Task<IFileObj> CompileToAsm(IFileObj input)
        {
            return await Compile(input, [@"D:\b20", "BCC", "-1", "-S"], Defaults.AsmExt);
        }

        public async Task<IFileObj> CompileToCom(IFileObj input)
        {
            return await Compile(input, [@"D:\b20", "BCC", "-1", "-mt", "-lt"], Defaults.ComExt);
        }
    }
}