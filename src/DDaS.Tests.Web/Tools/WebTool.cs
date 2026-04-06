using DDaS.Tools.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

        private static void Setup(ServiceCollection svc)
        {
            svc.AddLogging(builder => builder.AddFakeLogging().SetMinimumLevel(LogLevel.Trace));
        }
    }
}