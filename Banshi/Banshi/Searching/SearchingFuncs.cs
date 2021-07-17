
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Banshi.Searching
{
	public class SearchingFuncs
	{

		public async Task<Root> getWordDef(string word)
		{
			string url = $"https://api.dictionaryapi.dev/api/v2/entries/en_US/" + word;

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

			var response = await client.GetStringAsync(url);

			client.Dispose();

			Root model = JsonSerializer.Deserialize<Root>(response);


			return model;
		}

		public async Task<UrbanModel> getUrbanDef(string word)
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://mashape-community-urban-dictionary.p.rapidapi.com/define?term="+word),
				Headers =
			{
		{ "x-rapidapi-key", "de3a2e27camsha131444b8ce8f59p1ba7efjsn77ba32163609" },
		{ "x-rapidapi-host", "mashape-community-urban-dictionary.p.rapidapi.com" },
			},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				client.Dispose();
				UrbanModel model = JsonSerializer.Deserialize<UrbanModel>(body);
				return model;
			}
			
		}

		public async Task<Cat.Root> getCatPhoto()
		{
			string url = $"https://api.thecatapi.com/v1/images/search";

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

			var response = await client.GetStringAsync(url);


			client.Dispose();
			List<Cat.Root> model = JsonSerializer.Deserialize<List<Cat.Root>> (response);

			
			return model.First();
		}
		public async Task<Dog.Root> getDogPhoto()
		{
			string url = $"https://dog.ceo/api/breeds/image/random";

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

			var response = await client.GetStringAsync(url);

			client.Dispose();

			Dog.Root model = JsonSerializer.Deserialize<Dog.Root>(response);


			return model;
		}
	}
}
