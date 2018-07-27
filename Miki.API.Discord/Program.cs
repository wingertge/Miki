using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Miki.WebAPI.Discord
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

		public static IWebHost BuildWebHost(string[] args)
		{
			IConfigurationRoot root = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			return WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseKestrel()
				.UseUrls(root.GetValue<string>("urls"))
				.UseConfiguration(root)
				.Build();
		}
    }
}
