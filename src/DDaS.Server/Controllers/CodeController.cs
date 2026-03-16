using System.Text;
using System.Threading.Tasks;
using DDaS.Core;
using Microsoft.AspNetCore.Mvc;

namespace DDaS.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeController : ControllerBase
    {
        private readonly ICompiler _compiler;

        public CodeController(ICompiler compiler)
        {
            _compiler = compiler;
        }

        [HttpGet("compile", Name = "CompileCode")]
        public async Task<string> CompileCode(string text)
        {
            var arrays = Encoding.UTF8.GetBytes(text);
            var res = await _compiler.Compile([arrays]);
            return res;
        }
    }
}