using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Gateway;
using Discord.WebSockets;
using Discord.Media;
using System.Text.RegularExpressions;

namespace Banshi
{
	public static class GetGuildAnarchy
	{

		public static async Task setActivity(DiscordSocketClient client, string activity)
		{
			client.UpdatePresence(new PresenceProperties()
			{
				Status = UserStatus.DoNotDisturb,
				Activity = new StreamActivityProperties() { Name = activity}
			});
		}

		public static DiscordGuild getGuild(DiscordClient client, ulong guildId)
		{
			DiscordGuild guild = client.GetGuild(guildId);

			Console.WriteLine("working 7");
			return guild;
		}
		public static List<ulong> userList(SocketGuild guild, DiscordSocketClient client, ulong channelID)
		{
			List<ulong> list = new List<ulong>();
			var members = guild.GetChannelMembers(channelID);
			
			for(int i = 0; i < members.Count; i++)
			{
				list.Append(members[i].User.Id);
			}
			Console.WriteLine("working 8");
			return list;
		}
		public static void Client_OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
		{
			const string giftPrefix = "discord.gift/";

			var match = Regex.Match(args.Message.Content, giftPrefix + ".{16,24}");

			if (match.Success)
			{
				string code = match.Value.Substring(match.Value.IndexOf(giftPrefix) + giftPrefix.Length);

				try
				{
					client.RedeemGift(code);

					Console.WriteLine("Successfully redeemed code " + code);
				}
				catch (DiscordHttpException ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}

		public static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
		{
			Console.WriteLine("Logged in");
			
		}
		
	}
}
