using System.IO;
using DDaS.Tests.Web.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DDaS.Runner.Core
{
    public static class ConTool
    {
        public static T New<T>() where T : class
        {
            var svc = new ServiceCollection();
            Setup(svc);
            return LoadTool.New<T>(svc);
        }

        private static void Setup(ServiceCollection services)
        {
            var root = Directory.GetCurrentDirectory();
            var config = new ConfigurationBuilder()
                .SetBasePath(root)
                .AddJsonFile("appsettings.json")
                .Build();
            services.AddScoped<IConfiguration>(_ => config);
            services.AddLogging(bld => {
    bld.AddConfiguration(config.GetSection("Logging"));
bld.AddConsole();
});
        }
    }
}
