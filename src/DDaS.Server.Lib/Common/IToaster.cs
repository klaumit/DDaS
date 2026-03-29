using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDaS.Server.Common
{
    public interface IToaster
    {
        HttpContext GetHttpCtx(ControllerBase controller);
    }
}