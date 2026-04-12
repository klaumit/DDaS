using System.IO;
using DDaS.Core.Tools;
using DDaS.Tools.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// ReSharper disable ConvertIfStatementToReturnStatement

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

        private static string GetEnvironmentName()
        {
            var envName = FileTool.GetEnvVar("ASPNETCORE_ENVIRONMENT", "");
            if (!string.IsNullOrWhiteSpace(envName))
                return envName;

            envName = FileTool.GetEnvVar("DOTNET_ENVIRONMENT", "");
            if (!string.IsNullOrWhiteSpace(envName))
                return envName;

            return "Production";
        }

        private static void Setup(ServiceCollection services)
        {
            var root = Directory.GetCurrentDirectory();
            var envName = GetEnvironmentName();
            var config = new ConfigurationBuilder()
                .SetBasePath(root)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .Build();
            services.AddScoped<IConfiguration>(_ => config);
            services.AddLogging(bld =>
            {
                bld.AddConfiguration(config.GetSection("Logging"));
                bld.AddConsole();
            });
        }
    }
}