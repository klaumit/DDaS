using System.Threading.Tasks;
using DDaS.Core.Disassemblers.API;
using DDaS.Core.Tools;
using DDaS.Server.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DDaS.Server.Tools.WebTool;
using AS = DDaS.Core.Disassemblers.API.IDisassemblers;

namespace DDaS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisassembleController : ControllerBase
    {
        private static readonly string TmpDir = FileTool.CreateOrGetDir("tmp")!;

        private readonly AS _api;

        public DisassembleController(AS api)
        {
            _api = api;
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

            using var inputFile = await Save(TmpDir, f);
            var asm = _api.GetDisassembler(id);
            var exec = await asm.Disassemble(inputFile);
            HttpContext.SetHeaders(exec);
            using var outputFile = exec.File;
            return ToFile(this, outputFile);
        }
    }
}