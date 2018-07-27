using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;

namespace Miki.Rest.Tests
{
    public class HttpMethods
    {
		const string _baseUrl = "http://httpbin.org";

		[Fact]
        public async Task GetDirectAsync()
        {
			using (var client = new RestClient(_baseUrl + "/get", true))
			{
				RestResponse r = await client.GetAsync();
				Assert.Equal("http://httpbin.org/get", r.HttpResponseMessage.RequestMessage.RequestUri.ToString());
			}
        }

		[Fact]
		public async Task GetIndirectAsync()
		{
			using (var client = new RestClient(_baseUrl, true))
			{
				RestResponse r = await client.GetAsync("get");
				Assert.Equal("http://httpbin.org/get", r.HttpResponseMessage.RequestMessage.RequestUri.ToString());
			}
		}

		[Fact]
		public async Task DeleteDirectAsync()
		{
			using (var client = new RestClient(_baseUrl + "/delete", true))
			{
				RestResponse r = await client.DeleteAsync();
				Assert.Equal("http://httpbin.org/delete", r.HttpResponseMessage.RequestMessage.RequestUri.ToString());
			}
		}

		[Fact]
		public async Task DeleteIndirectAsync()
		{
			using (var client = new RestClient(_baseUrl, true))
			{
				RestResponse r = await client.DeleteAsync("delete");
				Assert.Equal("http://httpbin.org/delete", r.HttpResponseMessage.RequestMessage.RequestUri.ToString());
			}
		}

		[Fact]
		public async Task PatchDirectAsync()
		{
			using (var client = new RestClient(_baseUrl + "/patch", true))
			{
				RestResponse r = await client.PatchAsync();
				Assert.Equal("http://httpbin.org/patch", r.HttpResponseMessage.RequestMessage.RequestUri.ToString());
			}
		}

		[Fact]
		public async Task PatchIndirectAsync()
		{
			using (var client = new RestClient(_baseUrl, true))
			{
				RestResponse r = await client.PatchAsync("patch");
				Assert.Equal("http://httpbin.org/patch", r.HttpResponseMessage.RequestMessage.RequestUri.ToString());
			}
		}

		[Fact]
		public async Task PostDirectAsync()
		{
			using (var client = new RestClient(_baseUrl + "/post", true))
			{
				RestResponse r = await client.PostAsync();
				Assert.Equal("http://httpbin.org/post", r.HttpResponseMessage.RequestMessage.RequestUri.ToString());
			}
		}

		[Fact]
		public async Task PostIndirectAsync()
		{
			using (var client = new RestClient(_baseUrl, true))
			{
				RestResponse r = await client.PostAsync("post");
				Assert.Equal("http://httpbin.org/post", r.HttpResponseMessage.RequestMessage.RequestUri.ToString());
			}
		}

		[Fact]
		public async Task PutDirectAsync()
		{
			using (var client = new RestClient(_baseUrl + "/put", true))
			{
				RestResponse r = await client.PutAsync();
				Assert.Equal("http://httpbin.org/put", r.HttpResponseMessage.RequestMessage.RequestUri.ToString());
			}

		}

		[Fact]
		public async Task PutIndirectAsync()
		{
			using (var client = new RestClient(_baseUrl, true))
			{
				RestResponse r = await client.PutAsync("put");
				Assert.Equal("http://httpbin.org/put", r.HttpResponseMessage.RequestMessage.RequestUri.ToString());
			}
		}
	}
}
