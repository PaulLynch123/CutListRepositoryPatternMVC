using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CutListRepositoryPatternMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //entry point IHostBuilderObject making ASP.COre application (Kestral web server / IIS server)
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //referencing the Startup.cs file
                    webBuilder.UseStartup<Startup>();
                });
    }
}
