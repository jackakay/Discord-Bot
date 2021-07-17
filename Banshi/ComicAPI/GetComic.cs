using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

using System.Web;

using System.Net.Http;
using System.Text.json;
using System.Text.json;

namespace ComicAPI
{
	public class GetComic
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
