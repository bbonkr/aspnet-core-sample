using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using SampleMvc.Board;
using SampleMvc.Dashboard.Data;
using SampleMvc.Board.Data;
using SampleMvc.Board.Models;
using SampleMvc.Board.Controllers;
using SampleMvc.Data;

namespace SampleMvc
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(mvcOptions =>
            {

            });

            services.Configure<Microsoft.AspNetCore.Mvc.Razor.RazorViewEngineOptions>(o =>
            {
                o.FileProviders.Add(new CompositeFileProvider(new EmbeddedFileProvider(
                    typeof(SampleMvc.Dashboard.Controllers.HomeController).GetTypeInfo().Assembly,
                    typeof(SampleMvc.Dashboard.Controllers.HomeController).AssemblyQualifiedName)));

                o.FileProviders.Add(new CompositeFileProvider(new EmbeddedFileProvider(
                    typeof(DocumentsController).GetTypeInfo().Assembly,
                    typeof(DocumentsController).AssemblyQualifiedName)));
            });

            services.AddTransient<DbContext, ItemDbContext>();
            services.AddTransient<DbContext, AppDbContext>();

            services.AddTransient<IDocumentRepository, DocumentRepository>();

            // dotnet ef migrations add "AddDocumentEntity" --context "SampleMvc.Dashboard.ItemDbContext"
            services.AddDbContext<Dashboard.Data.ItemDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("SampleMvc"));
            });

            // dotnet ef migrations add "AddDocumentEntity" --context "SampleMvc.Board.DocumentDbContext"
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("SampleMvc"));
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            AppDbInitializer.Initialize(app.ApplicationServices.GetService<AppDbContext>());
        }
    }
}
