using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.AspNetCore.Server.Kestrel.Https.Internal;
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
                    logger.Debug($"SetCurrentDirectory in main application: {pathToContentRoot}");
                }

                var builder = CreateWebHostBuilder(
                    args.Where(arg => arg != "--console").ToArray());

                var host = builder.Build();

                if (isService)
                {
                    logger.Debug("RunAsCustomService in main application");
                    // To run the app without the CustomWebHostService change the
                    // next line to host.RunAsService();
                    host.RunAsCustomService();
                }
                else
                {
                    logger.Debug("Run in main application");
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
                .UseStartup<Startup>();
                //*** At some point, I would really like to use SSL in this service
                //.ConfigureKestrel((context, options) =>
                //{
                //    options.ListenAnyIP(7377, listenOptions =>
                //    {
                //        var signingCertificate = CertificateLoader.LoadFromStoreCert(
                //            "QIQO Software", "(null)", StoreLocation.CurrentUser,
                //            allowInvalid: true);
                //        // var signingCertificate = new X509Certificate2(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "qiqo-cert.pfx"));
                //        listenOptions.UseHttps(signingCertificate);
                //    });
                //});
    }
}