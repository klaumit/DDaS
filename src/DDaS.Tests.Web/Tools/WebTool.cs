using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DDaS.Core.Models;
using Xunit;
using DDaS.Core.Tools;
using DDaS.Tools;
using DDaS.Tools.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static System.Enum;
using ID = DDaS.Core.Assemblers.API.AssembleId;
using C = DDaS.Server.Controllers.AssembleController;

namespace DDaS.Tests.Web
{
    public static class WebTool
    {
        public static T New<T>() where T : class
        {
            var svc = new ServiceCollection();
            Setup(svc);
            return LoadTool.New<T>(svc);
        }

        private static void Setup(ServiceCollection services)
        {
            services.AddFakeLogging();
        }
    }
}