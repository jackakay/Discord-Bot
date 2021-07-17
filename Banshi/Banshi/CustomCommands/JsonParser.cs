using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banshi.CustomCommands
{
	 class JsonParser
	{
		public Models.CustomCommandsModel getCommand(string filepath)
		{
			using(StreamReader r = new StreamReader(filepath))
			{
				string json = r.ReadToEnd();
				var model = JsonSerializer.Deserialize< Models.CustomCommandsModel>(json);
				return model;
			}

			
		}


	}
}
