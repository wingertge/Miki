using System.Threading.Tasks;

namespace Miki.Common
{
	public class ClientInformation
	{
		public string Name { get; set; } = "IABot";
		public string Version { get; set; } = "1.0.0";

		public string Token { get; set; } = "";

		public int ShardCount { get; set; } = 1;
		public int StartShardId { get; set; } = 0;

		public string DatabaseProvider { get; set;} = "";
		public string DatabaseConnectionString { get; set; } = "";
    }
}