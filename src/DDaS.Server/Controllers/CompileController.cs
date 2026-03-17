using System.Threading.Tasks;
using DDaS.Core.API;
using DDaS.Core.Tools;
using DDaS.Server.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DDaS.Server.Tools.WebTool;

namespace DDaS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompileController : ControllerBase
    {
        private static readonly string TmpDirA = FileTool.CreateOrGetDir("tmp/asm")!;
        private static readonly string TmpDirC = FileTool.CreateOrGetDir("tmp/com")!;

        private readonly ICompilers _compilers;

        public CompileController(ICompilers compilers)
        {
            _compilers = compilers;
        }

        [HttpPost("asm/{id}", Name = "CompileAsm")]
        public async Task<IActionResult> CompileAsm(CompileId id, IFormFile? file)
        {
            if (file.IsEmpty() is not { } f)
                return BadRequest("No file provided!");

            using var inputFile = await Save(TmpDirA, f);
            var compiler = _compilers.GetCompiler(id);
            using var outputFile = await compiler.CompileToAsm(inputFile);
            return ToFile(this, outputFile);
        }

        [HttpPost("com/{id}", Name = "CompileCom")]
        public async Task<IActionResult> CompileCom(CompileId id, IFormFile? file)
        {
            if (file.IsEmpty() is not { } f)
                return BadRequest("No file provided!");

            using var inputFile = await Save(TmpDirC, f);
            var compiler = _compilers.GetCompiler(id);
            using var outputFile = await compiler.CompileToCom(inputFile);
            return ToFile(this, outputFile);
        }
    }
}