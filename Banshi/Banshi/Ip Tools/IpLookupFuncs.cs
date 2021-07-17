
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Banshi.Ip_Tools
{
	public class IpLookupFuncs
	{

		public async Task<Root> getGeo(string ip)
		{
			string url = $"https://api.ipgeolocation.io/ipgeo?apiKey=fcbe380041d8454ca6d2ab3cd7981a62&ip=" + ip;

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

			var response = await client.GetStringAsync(url);



			Root model = JsonSerializer.Deserialize<Root>(response);
			

			return model;
		}

	}
}
