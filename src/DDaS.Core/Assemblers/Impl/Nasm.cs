using System.Threading.Tasks;
using DDaS.Core.API;
using DDaS.Core.Models;

namespace DDaS.Core.Assemblers.Impl
{
    public sealed class Nasm : IAssembler
    {
        
        
        
        public Task<Executed> CompileToAsm(IFileObj input)
        {
            /*

               nasm -f bin -o hello.com hello.asm



               .c -> .asm
               .c -> .com
               
               
               .asm -> .com
               .com -> .dis
               
               
               

             */

            throw new System.NotImplementedException();
        }

        public Task<Executed> CompileToCom(IFileObj input)
        {
            throw new System.NotImplementedException();
        }
    }
}