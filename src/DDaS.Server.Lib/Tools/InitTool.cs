using DDaS.Core.Assemblers;
using DDaS.Core.Assemblers.API;
using DDaS.Core.Common;
using DDaS.Core.Compilers;
using DDaS.Core.Compilers.API;
using DDaS.Core.Disassemblers;
using DDaS.Core.Disassemblers.API;
using DDaS.Server.Common;
using Microsoft.Extensions.DependencyInjection;

namespace DDaS.Server.Tools
{
    public static class InitTool
    {
        public static void Setup(this IServiceCollection services, Toaster? a = null, Temper? b = null)
        {
            services.AddSingleton(b ?? new Temper());
            services.AddSingleton(a ?? new Toaster());
            services.AddSingleton<ICompilers>(new Compilers());
            services.AddSingleton<IAssemblers>(new Assemblers());
            services.AddSingleton<IDisassemblers>(new Disassemblers());
        }
    }
}