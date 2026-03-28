using System.Reflection;
using DDaS.Server.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DDaS.Tests.Web.Tools
{
    public static class LoadTool
    {
        public static T New<T>() where T : class
        {
            var svc = new ServiceCollection();
            svc.AddScoped<T>();
            svc.Setup(new MemToaster());
            using var provider = svc.BuildServiceProvider();
            return provider.GetRequiredService<T>();
        }

        public static MemToaster FindToaster(this ControllerBase ctrl)
        {
            var type = ctrl.GetType();
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var field = type.GetField("_toa", flags);
            var val = field?.GetValue(ctrl);
            return (MemToaster)val!;
        }
    }
}