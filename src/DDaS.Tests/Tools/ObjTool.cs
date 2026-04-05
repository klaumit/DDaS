using Microsoft.Extensions.DependencyInjection;

namespace DDaS.Tests.Tools
{
    public static class ObjTool
    {
        public static T New<T>(IServiceCollection? coll = null) where T : class
        {
            var svc = coll ?? new ServiceCollection();
            svc.AddScoped<T>();
            Setup(svc);
            using var provider = svc.BuildServiceProvider();
            return provider.GetRequiredService<T>();
        }

        private static void Setup(IServiceCollection services)
        {
            services.AddFakeLogging();
        }
    }
}