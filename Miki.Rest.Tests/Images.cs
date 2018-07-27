using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Miki.Rest.Tests
{
    public class Images
	{
		const string _baseUrl = "http://httpbin.org";

		[Fact]
		public async Task GetStreamDirectAsync()
		{
			using (var client = new RestClient(_baseUrl + "/image/png", true))
			{
				Stream r = await client.GetStreamAsync();
				if(r.Length == 0)
				{
					throw new Exception("size is 0");
				}
			}
		}

		[Fact]
		public async Task GetStreamIndirectAsync()
		{
			using (var client = new RestClient(_baseUrl, true))
			{
				Stream r = await client.GetStreamAsync("image/png");
				if (r.Length == 0)
				{
					throw new Exception("size is 0");
				}
			}
		}

	}
}
