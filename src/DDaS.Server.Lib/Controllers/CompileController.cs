using System.Threading.Tasks;
using DDaS.Core.Compilers.API;
using DDaS.Server.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DDaS.Server.Tools.WebTool;
using AS = DDaS.Core.Compilers.API.ICompilers;
using T = DDaS.Core.Common.Temper;
using H = DDaS.Server.Common.Toaster;

namespace DDaS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompileController : ControllerBase
    {
        private readonly AS _api;
        private readonly T _tmp;
        private readonly H _toa;

        public CompileController(AS api, T tmp, H toa)
        {
            _api = api;
            _tmp = tmp;
            _toa = toa;
        }

        [HttpGet("ids", Name = "AllCompileIds")]
        public OkObjectResult Get()
        {
            return Ok(_api.ListCompilerInfo());
        }

        [HttpPost("asm/{id}", Name = "CompileAsm")]
        public async Task<IActionResult> CompileAsm(CompileId id, IFormFile? file)
        {
            if (file.IsEmpty() is not { } f)
                return BadRequest("No file provided!");

            var tmpDir = _tmp.GetTempDir(this, id);
            using var inputFile = await Save(tmpDir, f);
            var compiler = _api.GetCompiler(id);
            var exec = await compiler.CompileToAsm(inputFile);
            _toa.GetHttpCtx(this).SetHeaders(exec);
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
            var compiler = _api.GetCompiler(id);
            var exec = await compiler.CompileToCom(inputFile);
            _toa.GetHttpCtx(this).SetHeaders(exec);
            using var outputFile = exec.File;
            return ToFile(this, outputFile);
        }
    }
}