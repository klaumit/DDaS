using System.Threading.Tasks;
using DDaS.Core.Disassemblers.API;
using DDaS.Server.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static DDaS.Server.Tools.WebTool;
using AS = DDaS.Core.Disassemblers.API.IDisassemblers;
using T = DDaS.Core.Common.ITemper;
using H = DDaS.Server.Common.IToaster;

namespace DDaS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisassembleController : ControllerBase
    {
        private readonly ILogger<DisassembleController> _log;
        private readonly AS _api;
        private readonly T _tmp;
        private readonly H _toa;

        public DisassembleController(ILogger<DisassembleController> log, AS api, T tmp, H toa)
        {
            _log = log;
            _api = api;
            _tmp = tmp;
            _toa = toa;
        }

        [HttpGet("ids", Name = nameof(AllDisassembleIds))]
        public OkObjectResult AllDisassembleIds()
        {
            if (_log.IsEnabled(LogLevel.Debug))
                _log.LogDebug("Listing all disassembler ids");
            return Ok(_api.ListDisassemblerInfo());
        }

        [HttpPost("{id}", Name = nameof(Disassemble))]
        public async Task<IActionResult> Disassemble(DisassembleId id, IFormFile? file)
        {
            if (_log.IsEnabled(LogLevel.Debug))
                _log.LogDebug("Disassembling '{Id}' '{File}'", id, file?.FileName);
            if (file.IsEmpty() is not { } f)
                return BadRequest("No file provided!");

            using var tmpDir = _tmp.CreateTmpDir(this, id);
            using var inputFile = await Save(tmpDir, f);
            var asm = _api.GetDisassembler(id);
            var exec = await asm.Disassemble(inputFile);
            _toa.GetHttpCtx(this).SetHeaders(exec);
            using var outputFile = exec.File;
            return ToFile(this, outputFile);
        }
    }
}