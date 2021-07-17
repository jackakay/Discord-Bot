using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Banshi.Overlay
{
	public class OverlayFuncs
	{
		public async Task<string> makeGay(string img)
		{
			string url = $"https://some-random-api.ml/canvas/gay/?avatar=" + img;

			using var client = new HttpClient();
			//client.DefaultRequestHeaders.Add("application/json", "");

			var response = await client.GetStringAsync(url);

			client.Dispose();


			return response;

			
		}
	}
}
