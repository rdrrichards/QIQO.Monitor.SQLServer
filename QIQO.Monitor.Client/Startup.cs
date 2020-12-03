using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using QIQO.Monitor.Data;
using System;
using System.IO;
using System.Reflection;

namespace QIQO.Monitor.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                // configure Logging with NLog
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                loggingBuilder.AddNLog(Configuration);
            });
            services.AddMemoryCache();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AnyOrigin", builder =>
            //    {
            //        builder
            //            .AllowAnyOrigin()
            //            .AllowAnyHeader()
            //            .AllowAnyMethod();
            //    });
            //});
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Configuration["Swagger:Version"],
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = Configuration["Swagger:ApplicationName"],
                        Version = Configuration["Swagger:Version"],
                        Description = Configuration["Swagger:Description"]
                    }
                    );
                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                c.IncludeXmlComments(xmlPath);
            });
            services.AddApplicationDataAccessServices(options => {
                options.ConnectionString = Configuration["ConnectionStrings:QIQOMonitor"];
            });
            services.AddEntityServices();
            services.AddControllersWithViews();
            services.AddSignalR(options => {
                options.EnableDetailedErrors = true;
                options.MaximumReceiveMessageSize = null;
            });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
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
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
                $"{Configuration["Swagger:ApplicationName"]} {Configuration["Swagger:Version"]}"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<MonitorComms>("monitorHub");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
