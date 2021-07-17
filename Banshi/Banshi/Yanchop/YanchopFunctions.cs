using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Banshi.Yanchop
{
	public class YanchopFunctions
	{

		public async Task<PlayerModel> getPlayer(string ign)
		{
			string url = $"https://api.mojang.com/users/profiles/minecraft/" + ign;

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

			var response = await client.GetStringAsync(url);

			client.Dispose();
			PlayerModel model = JsonSerializer.Deserialize<PlayerModel>(response);


			return model;
		}

		public async Task<PreviousNameModel> getPrevName(string uuid)
		{
			string url = $"https://api.mojang.com/user/profiles/" + uuid + "/names";

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

			var response = await client.GetStringAsync(url);

			client.Dispose();

			PreviousNameModel model = JsonSerializer.Deserialize<PreviousNameModel>(response);


			return model;
		}

		

		

	}
}
