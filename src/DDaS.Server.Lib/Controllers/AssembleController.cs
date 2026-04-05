using System.Threading.Tasks;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Common;
using DDaS.Server.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static DDaS.Server.Tools.WebTool;
using AS = DDaS.Core.Assemblers.API.IAssemblers;
using T = DDaS.Core.Common.ITemper;
using H = DDaS.Server.Common.IToaster;

namespace DDaS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssembleController : ControllerBase, IController
    {
        private readonly ILogger<AssembleController> _log;
        private readonly AS _api;
        private readonly T _tmp;
        private readonly H _toa;

        public AssembleController(ILogger<AssembleController> log, AS api, T tmp, H toa)
        {
            _log = log;
            _api = api;
            _tmp = tmp;
            _toa = toa;
        }

        [HttpGet("ids", Name = nameof(AllAssembleIds))]
        public OkObjectResult AllAssembleIds()
        {
            if (_log.IsEnabled(LogLevel.Debug))
                _log.LogDebug("Listing all assembler ids");
            return Ok(_api.ListAssemblerInfo());
        }

        [HttpPost("{id}", Name = nameof(Assemble))]
        public async Task<IActionResult> Assemble(AssembleId id, IFormFile? file)
        {
            if (_log.IsEnabled(LogLevel.Debug))
                _log.LogDebug("Assembling {Id} {File}", id, file?.FileName);
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