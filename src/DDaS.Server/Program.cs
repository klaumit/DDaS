using DDaS.Core.API;
using DDaS.Core.Compilers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDaS.Server
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var bld = WebApplication.CreateBuilder(args);

            bld.Services.AddSingleton<ICompilers>(new Compilers());

            bld.Services.AddControllers();
            bld.Services.AddOpenApi();

            var app = bld.Build();

            app.UseDefaultFiles();
            app.MapStaticAssets();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}