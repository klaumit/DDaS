using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeMadeStatic.Global

namespace DDaS.Server.Common
{
    public class Toaster
    {
        public virtual HttpContext GetHttpCtx(ControllerBase controller)
        {
            var ctx = controller.HttpContext;
            return ctx;
        }
    }
}