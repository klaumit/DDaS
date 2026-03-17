using System.Threading.Tasks;
using DDaS.Core.API;
using DDaS.Core.Tools;
using DDaS.Server.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDaS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeController : ControllerBase
    {
        private static readonly string TmpDir = FileTool.CreateOrGetDir("tmp_code")!;

        private readonly ICompiler _compiler;

        public CodeController(ICompiler compiler)
        {
            _compiler = compiler;
        }

        [HttpPost("compile", Name = "CompileCode")]
        public async Task<IActionResult> Upload(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided.");

            using var inputFile = await WebTool.Save(TmpDir, file);
            using var outputFile = await _compiler.Compile(inputFile);

            var name = outputFile.Name;
            var bytes = outputFile.Bytes;
            return File(bytes, WebTool.Octet, name);
        }
    }
}