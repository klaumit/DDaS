using System.Threading.Tasks;
using DDaS.Core.Assemblers.API;
using DDaS.Server.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DDaS.Server.Tools.WebTool;
using AS = DDaS.Core.Assemblers.API.IAssemblers;
using T = DDaS.Core.Common.Temper;
using H = DDaS.Server.Common.Toaster;

namespace DDaS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssembleController : ControllerBase
    {
        private readonly AS _api;
        private readonly T _tmp;
        private readonly H _toa;

        public AssembleController(AS api, T tmp, H toa)
        {
            _api = api;
            _tmp = tmp;
            _toa = toa;
        }

        [HttpGet("ids", Name = "AllAssembleIds")]
        public OkObjectResult Get()
        {
            return Ok(_api.ListAssemblerInfo());
        }

        [HttpPost("{id}", Name = "Assemble")]
        public async Task<IActionResult> Assemble(AssembleId id, IFormFile? file)
        {
            if (file.IsEmpty() is not { } f)
                return BadRequest("No file provided!");

            var tmpDir = _tmp.GetTempDir(this, id);
            using var inputFile = await Save(tmpDir, f);
            var asm = _api.GetAssembler(id);
            var exec = await asm.Assemble(inputFile);
            _toa.GetHttpCtx(this).SetHeaders(exec);
            using var outputFile = exec.File;
            return ToFile(this, outputFile);
        }
    }
}