using System.Threading.Tasks;
using DDaS.Core.Compilers.API;
using DDaS.Server.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DDaS.Server.Tools.WebTool;
using T = DDaS.Core.Common.Temper;

namespace DDaS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompileController : ControllerBase
    {
        private readonly ICompilers _compilers;
        private readonly T _tmp;

        public CompileController(ICompilers compilers, T tmp)
        {
            _compilers = compilers;
            _tmp = tmp;
        }

        [HttpGet("ids", Name = "AllCompileIds")]
        public OkObjectResult Get()
        {
            return Ok(_compilers.ListCompilerInfo());
        }

        [HttpPost("asm/{id}", Name = "CompileAsm")]
        public async Task<IActionResult> CompileAsm(CompileId id, IFormFile? file)
        {
            if (file.IsEmpty() is not { } f)
                return BadRequest("No file provided!");

            var tmpDir = _tmp.GetTempDir(this, id);
            using var inputFile = await Save(tmpDir, f);
            var compiler = _compilers.GetCompiler(id);
            var exec = await compiler.CompileToAsm(inputFile);
            HttpContext.SetHeaders(exec);
            using var outputFile = exec.File;
            return ToFile(this, outputFile);
        }

        [HttpPost("com/{id}", Name = "CompileCom")]
        public async Task<IActionResult> CompileCom(CompileId id, IFormFile? file)
        {
            if (file.IsEmpty() is not { } f)
                return BadRequest("No file provided!");

            var tmpDir = _tmp.GetTempDir(this, id);
            using var inputFile = await Save(tmpDir, f);
            var compiler = _compilers.GetCompiler(id);
            var exec = await compiler.CompileToCom(inputFile);
            HttpContext.SetHeaders(exec);
            using var outputFile = exec.File;
            return ToFile(this, outputFile);
        }
    }
}