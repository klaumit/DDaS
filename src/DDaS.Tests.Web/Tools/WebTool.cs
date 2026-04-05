using DDaS.Tools.Web;
using Microsoft.Extensions.DependencyInjection;

namespace DDaS.Tests.Web.Tools
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