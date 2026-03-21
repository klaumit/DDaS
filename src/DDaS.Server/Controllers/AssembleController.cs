using System.Threading.Tasks;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Tools;
using DDaS.Server.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DDaS.Server.Tools.WebTool;

namespace DDaS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssembleController : ControllerBase
    {
        private static readonly string TmpDir = FileTool.CreateOrGetDir("tmp")!;

        private readonly IAssemblers _assemblers;

        public AssembleController(IAssemblers assemblers)
        {
            _assemblers = assemblers;
        }

        [HttpGet("ids", Name = "AllAssembleIds")]
        public OkObjectResult Get()
        {
            return Ok(_assemblers.ListAssemblerInfo());
        }

        [HttpPost("asm/{id}", Name = "AssembleAsm")]
        public async Task<IActionResult> AssembleAsm(AssembleId id, IFormFile? file)
        {
            if (file.IsEmpty() is not { } f)
                return BadRequest("No file provided!");

            using var inputFile = await Save(TmpDir, f);
            var assembler = _assemblers.GetAssembler(id);
            var exec = await assembler.Disassemble(inputFile);
            HttpContext.SetHeaders(exec);
            using var outputFile = exec.File;
            return ToFile(this, outputFile);
        }

        [HttpPost("com/{id}", Name = "AssembleCom")]
        public async Task<IActionResult> AssembleCom(AssembleId id, IFormFile? file)
        {
            if (file.IsEmpty() is not { } f)
                return BadRequest("No file provided!");

            using var inputFile = await Save(TmpDir, f);
            var assembler = _assemblers.GetAssembler(id);
            var exec = await assembler.Assemble(inputFile);
            HttpContext.SetHeaders(exec);
            using var outputFile = exec.File;
            return ToFile(this, outputFile);
        }
    }
}