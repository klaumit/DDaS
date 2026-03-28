using DDaS.Server.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace DDaS.Tests.Web.Tools
{
    public static class LoadTool
    {
        public static T New<T>() where T : class
        {
            var svc = new ServiceCollection();
            svc.AddScoped<T>();
            svc.Setup();
            using var provider = svc.BuildServiceProvider();
            return provider.GetRequiredService<T>();
        }
    }
}