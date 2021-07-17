using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Banshi.Searching.Space
{
	public class NASASearch
	{
		public async Task<Root> getNASAPhoto()
		{
			string url = $"https://api.nasa.gov/planetary/apod?api_key=DISgU2wmQrsIardTkBuh3MvNmhJbKeWkmGp5ZhIZ&count=1";

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

			var response = await client.GetStringAsync(url);

			client.Dispose();
				

			List<Root> model = JsonSerializer.Deserialize<List<Root>>(response);
			


			return model.First();
			
		}
	}
}
