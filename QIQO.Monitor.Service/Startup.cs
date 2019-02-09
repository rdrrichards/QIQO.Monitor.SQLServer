using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using QIQO.Monitor.Service.Polling;
using QIQO.Monitor.SQLServer.Data;

namespace QIQO.Monitor.Service
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddEntityServices();
            services.AddDataAccess();
            services.AddPollers();
            services.AddSingleton<IServerManager, ServerManager>();
            services.AddSignalR();
            services.AddTransient<IHubClientService, HubClientService>();
            services.AddSingleton<IMonitorService, MonitorService>();
            services.BuildServiceProvider().GetService<IMonitorService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSignalR(routes =>
            {
                routes.MapHub<ResultsHub>("/results");
            });
        }
    }
}
