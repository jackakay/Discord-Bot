using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Banshi.Comic
{
	public class ComicGetter
	{
		public async Task<ComicModel> loadComic(int comicNumber = 0)
		{
			string url = $"https://xkcd.com/" + comicNumber + "/info.0.json";

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

			var response = await client.GetStringAsync(url);


			ComicModel model = JsonSerializer.Deserialize<ComicModel>(response);


			return model;
		}
	}
}
