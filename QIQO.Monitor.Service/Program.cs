﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace QIQO.Monitor.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("Initializing main application");
                if (isService)
                {
                    var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                    var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                    Directory.SetCurrentDirectory(pathToContentRoot);
                }

                var builder = CreateWebHostBuilder(
                    args.Where(arg => arg != "--console").ToArray());

                var host = builder.Build();

                if (isService)
                {
                    // To run the app without the CustomWebHostService change the
                    // next line to host.RunAsService();
                    host.RunAsCustomService();
                }
                else
                {
                    host.Run();
                }
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .UseNLog()
                //.ConfigureAppConfiguration((context, config) =>
                //{
                //    // Configure the app here.
                //})
                // .UseUrls("https://127.0.0.1:44365")
                .UseStartup<Startup>();
    }
}