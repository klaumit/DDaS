using System.Threading.Tasks;
using DDaS.Core.Disassemblers.API;
using DDaS.Server.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DDaS.Server.Tools.WebTool;
using AS = DDaS.Core.Disassemblers.API.IDisassemblers;
using T = DDaS.Core.Common.Temper;

namespace DDaS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisassembleController : ControllerBase
    {
        private readonly AS _api;
        private readonly T _tmp;

        public DisassembleController(AS api, T tmp)
        {
            _api = api;
            _tmp = tmp;
        }

        [HttpGet("ids", Name = "AllDisassembleIds")]
        public OkObjectResult Get()
        {
            return Ok(_api.ListDisassemblerInfo());
        }

        [HttpPost("{id}", Name = "Disassemble")]
        public async Task<IActionResult> Assemble(DisassembleId id, IFormFile? file)
        {
            if (file.IsEmpty() is not { } f)
                return BadRequest("No file provided!");

            var tmpDir = _tmp.GetTempDir(this, id);
            using var inputFile = await Save(tmpDir, f);
            var asm = _api.GetDisassembler(id);
            var exec = await asm.Disassemble(inputFile);
            HttpContext.SetHeaders(exec);
            using var outputFile = exec.File;
            return ToFile(this, outputFile);
        }
    }
}