using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blace.TestEditor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //GenerateEnumsTest();

            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private static void GenerateEnumsTest()
        {
            var path = @"C:\Users\floris\Documents\Repo\Blace\src\Blace\wwwroot\ace\";
            var files = Directory.GetFiles(path);

            Console.WriteLine("public enum Theme { ");

            foreach (var file in files)
            {
                if (file.Contains("theme-") && !file.Contains(".min"))
                {
                    var value = file.Replace(path, "").Replace("theme-", string.Empty).Replace(".js", "");
                    Console.WriteLine(value.First().ToString().ToUpperInvariant() + value.Substring(1, value.Length - 1) + ",");
                }
            }

            Console.WriteLine("}");
        }
    }
}
