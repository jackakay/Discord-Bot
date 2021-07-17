using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Gateway;

namespace Banshi.Server_Based_Commands
{
	public class Moderation
	{
		public static async Task BanMember(DiscordClient client, ulong guildID, ulong UserID)
		{
			await client.BanGuildMemberAsync(guildID, UserID);
			
		}
		public static async Task KickMember(DiscordClient client, ulong guildID, ulong UserID)
		{
			await client.KickGuildMemberAsync(guildID, UserID);

		}
	}
}
