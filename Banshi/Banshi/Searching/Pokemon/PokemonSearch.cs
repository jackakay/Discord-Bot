using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Banshi.Searching.Pokemon
{
	public class PokemonSearch
	{
		public async Task<Root> getPokemonStats(string pokemonName)
		{
			string url = $"https://pokeapi.co/api/v2/pokemon/"+ pokemonName;

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

			var response = await client.GetStringAsync(url);

			client.Dispose();

			Root model = JsonSerializer.Deserialize<Root>(response);

			
			return model;
		}
	}
}
