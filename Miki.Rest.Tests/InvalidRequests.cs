using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Miki.Rest.Tests
{
	public class InvalidRequests
	{
		const string _baseUrl = "http://httpbin.org";

		[Fact]
		public async Task RequestNotFoundAsync()
		{
			using (var client = new RestClient(_baseUrl))
			{
				RestResponse r = await client.GetAsync("/status/404");
				Assert.Equal(HttpStatusCode.NotFound, r.HttpResponseMessage.StatusCode);
				Assert.Throws<HttpRequestException>(() => r.HttpResponseMessage.EnsureSuccessStatusCode());
			}
		}
	}
}
