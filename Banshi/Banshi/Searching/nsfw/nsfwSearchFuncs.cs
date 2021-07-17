using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Banshi.Searching.nsfw
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
 public class nsfwSearchFuncs
	{
		public async Task<Root> getnsfw(string tags)
		{
			string url = $"https://purrbot.site/api/img/nsfw/" + tags+ "/gif";

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

			var response = await client.GetStringAsync(url);

			client.Dispose();

			Root model = JsonSerializer.Deserialize<Root>(response);


			return model;
		}
	}


}
