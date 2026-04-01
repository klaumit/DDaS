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
        public static void Setup(this IServiceCollection services, IToaster a)
        {
            services.AddSingleton(new Temper());
            services.AddSingleton(a);
            services.AddSingleton<ICompilers>(new Compilers());
            services.AddSingleton<IAssemblers>(new Assemblers());
            services.AddSingleton<IDisassemblers>(new Disassemblers());
        }
    }
}