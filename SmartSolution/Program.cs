﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ApplicationConfiguration configuration = new ApplicationConfiguration();
            configuration.Load();

            var host = new WebHostBuilder()
                 .UseKestrel()
                 .UseContentRoot(Directory.GetCurrentDirectory())
                 .UseUrls("http://*:" + configuration.ServerPort)
                 .UseIISIntegration()
                 .ConfigureAppConfiguration((context, configBuilder) =>
                 {
                     configBuilder
                         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                         .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true)
                         .AddEnvironmentVariables();
                 })
                 .ConfigureLogging(loggerFactory => loggerFactory
                     .AddConsole()
                     .AddDebug())
                 .UseStartup<Startup>()
                 .Build();

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
