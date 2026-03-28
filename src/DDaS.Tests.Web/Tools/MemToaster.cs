using System;
using System.Collections.Concurrent;
using DDaS.Server.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDaS.Tests.Web.Tools
{
    public sealed class MemToaster : Toaster
    {
        private readonly ConcurrentDictionary<ControllerBase, HttpContext> _dict;

        public MemToaster()
        {
            _dict = new ConcurrentDictionary<ControllerBase, HttpContext>();
        }

        public DefaultHttpContext SetHttpCtx(ControllerBase controller)
        {
            var ctx = new DefaultHttpContext();
            _dict[controller] = ctx;
            return ctx;
        }

        public override HttpContext GetHttpCtx(ControllerBase controller)
        {
            if (_dict.TryRemove(controller, out var found))
                return found;
            throw new InvalidOperationException("No context defined!");
        }
    }
}