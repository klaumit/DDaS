using System.Threading.Tasks;
using DDaS.Core.API;
using DDaS.Core.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DDaS.Server.Tools.WebTool;

namespace DDaS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CodeController : ControllerBase
    {
        private static readonly string TmpDirA = FileTool.CreateOrGetDir("tmp/asm")!;
        private static readonly string TmpDirC = FileTool.CreateOrGetDir("tmp/com")!;

        private readonly ICompiler _compiler;

        public CodeController(ICompiler compiler)
        {
            _compiler = compiler;
        }

        [HttpPost("compile/asm", Name = "CompileAsm")]
        public async Task<IActionResult> CompileAsm(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided!");

            using var inputFile = await Save(TmpDirA, file);
            using var outputFile = await _compiler.CompileToAsm(inputFile);
            return ToFile(outputFile);
        }

        [HttpPost("compile/com", Name = "CompileCom")]
        public async Task<IActionResult> CompileCom(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided!");

            using var inputFile = await Save(TmpDirC, file);
            using var outputFile = await _compiler.CompileToCom(inputFile);
            return ToFile(outputFile);
        }

        private FileContentResult ToFile(IFileObj outputFile)
        {
            var name = outputFile.Name;
            var bytes = outputFile.Bytes;
            return File(bytes, Octet, name);
        }
    }
}