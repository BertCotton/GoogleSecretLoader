using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleSecretLoader
{
    public class Program
    {
        public static void Main(string[] args)
        {
          


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var environmentVariableConfiguration = new ConfigurationBuilder()
                .AddJsonFile(("appsettings.json"))
                .AddEnvironmentVariables()
                .Build();
            var secretFilePath = environmentVariableConfiguration["GoogleSecreteFilePath"];
            var gcpProjectId = environmentVariableConfiguration["GCP_ProjectName"];
            var applicationName = environmentVariableConfiguration["ApplicationName"];
        
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", optional: true)
                        .AddCommandLine(args)
                        .Add(new GoogleSecretLoader(secretFilePath, gcpProjectId, applicationName));
                }))
                .ConfigureWebHost(builder =>
                {
                    builder.UseStartup<Startup>();
                });
        }
    }

}
