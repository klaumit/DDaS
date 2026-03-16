using DDaS.Core;
using Microsoft.AspNetCore.Mvc;

namespace DDaS.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeController : ControllerBase
    {
        private readonly Class1 _ctx;

        public CodeController(Class1 ctx)
        {
            _ctx = ctx;
        }

        [HttpGet("compile", Name = "CompileCode")]
        public string CompileCode()
        {
            return " ?! ";
        }
    }
}