using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using NLog.Extensions.Logging;
using QIQO.Monitor.Data;
using QIQO.Monitor.SQLServer.Data;
using System;
using System.Net.Http.Headers;

namespace QIQO.Monitor.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    Microsoft.Extensions.Configuration.IConfiguration configuration = hostContext.Configuration;
                    services.AddSingleton<IMemoryCache>(new MemoryCache(new MemoryCacheOptions()));
                    services.AddHostedService<BlockingWorker>();
                    services.AddHttpClient("QIQOMonitor", options =>
                    {
                        options.BaseAddress = new Uri(configuration["Settings:QIQOMonitor"]);
                        options.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    });
                    services.AddApplicationDataAccessServices(options =>
                    {
                        options.ConnectionString = configuration["ConnectionStrings:QIQOMonitor"];
                    });
                    services.AddSqlServerDataAccessServices();
                    services.AddLogging(loggingBuilder =>
                    {
                        // configure Logging with NLog
                        loggingBuilder.ClearProviders();
                        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                        loggingBuilder.AddNLog(configuration);
                    });
                });
    }
}
