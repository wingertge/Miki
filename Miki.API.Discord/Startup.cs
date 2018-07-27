using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Miki.Discord.Rest;
using Miki.WebAPI.Discord.Controllers;

namespace Miki.WebAPI.Discord
{
    public class Startup
    {
		public static string AccessKey;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services
				.AddMvcCore()
				.AddJsonFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			string token = Configuration.GetValue<string>("discord_token");

			// TODO: better authorization
			AccessKey = Configuration.GetValue<string>("auth_code");

			DiscordController.client = new DiscordClient(token, new StackExchange.Redis.Extensions.Core.StackExchangeRedisCacheClient(
			new StackExchange.Redis.Extensions.Protobuf.ProtobufSerializer(),
			new StackExchange.Redis.Extensions.Core.Configuration.RedisConfiguration()
			{
				Hosts = new[] {
					new StackExchange.Redis.Extensions.Core.Configuration.RedisHost()
					{
						Host = "127.0.0.1"
					}
				},
				Password = Configuration.GetValue<string>("redis_password")
			}));

			app.UseMvc();
        }
    }
}
