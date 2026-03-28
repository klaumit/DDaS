using DDaS.Core.Assemblers;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Compilers;
using DDaS.Core.Compilers.API;
using Microsoft.Extensions.DependencyInjection;

namespace DDaS.Server.Tools
{
    public static class InitTool
    {
        public static void Setup(this IServiceCollection services)
        {
            services.AddSingleton<ICompilers>(new Compilers());
            services.AddSingleton<IAssemblers>(new Assemblers());
        }
    }
}