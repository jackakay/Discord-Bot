using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading;
using System.Linq;
using System.Text;
using System.Management;
using DSharpPlus;
using DSharpPlus.Net;
using System.Collections.Generic;
using System.IO;

using System.Diagnostics;

using System.Collections;
using System.Net;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;

namespace Banshi.Commands
{
    public class Commands
    {
        public static string yourGaynessInString;
        public static string theirGaynessInString;

        public static int playerOption;




        [Command("HelloWorld"), Description("A simple test command")]

        public async Task HelloWorld(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync();
            Console.WriteLine(ctx.Channel.Id);
            await ctx.RespondAsync("Hello World!");


        }


        [Command("embed"), Description("Embed command")]

        public async Task embed(CommandContext ctx, string title, [RemainingText] string description)
        {


            var discordEmbedTest = new DiscordEmbedBuilder
            {
                Title = title,
                ThumbnailUrl = ctx.User.AvatarUrl,
                Description = description,
                Color = Startup.Startup.embedDiscordColor
            };

            await ctx.Message.DeleteAsync();

            await ctx.RespondAsync(embed: discordEmbedTest);
        }



        [Command("btc"), Description("Gets current Bitcoin Price")]
        public async Task btc(CommandContext ctx)
        {
            var uri = String.Format(@"https://blockchain.info/tobtc?currency=USD&value={0}", 1);

            WebClient client = new WebClient();

            client.UseDefaultCredentials = true;

            var data = client.DownloadString(uri);

            var result = 1 / Convert.ToDouble(data.Replace('.', ',')); //you will receive 1 btc = result;






            var btcEmbed = new DiscordEmbedBuilder
            {
                Title = "Bitcoin Price",
                ThumbnailUrl = ctx.User.AvatarUrl,
                Description = "Current Price for $1 - " + result,
                Color = Startup.Startup.embedDiscordColor
            };


            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync(embed: btcEmbed);

        }


        [Command("poll"), Description("Sends a poll")]
        public async Task poll(CommandContext ctx, string a, string b, [RemainingText] string question)
        {


            var pollEmbed = new DiscordEmbedBuilder
            {
                Title = question,
                Description = "A -  " + a + "\n\n" + "B -  " + b,
                ThumbnailUrl = ctx.User.AvatarUrl,
                Color = Startup.Startup.embedDiscordColor


            };

            var aEmoji = DiscordEmoji.FromName(ctx.Client, ":a:");
            var bEmoji = DiscordEmoji.FromName(ctx.Client, ":b:");
            await ctx.Message.DeleteAsync();
            var pollMessage = await ctx.RespondAsync(embed: pollEmbed);
            await pollMessage.CreateReactionAsync(aEmoji).ConfigureAwait(false);
            Thread.Sleep(600);
            await pollMessage.CreateReactionAsync(bEmoji).ConfigureAwait(false);

        }

        [Command("nitro"), Description("Generates a nitro linkn")]
        public async Task nitro(CommandContext ctx)
        {
            Random random = new Random();
            string url = "https://discord.gift/";
            string code = "";

            string dict = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

            for (int i = 0; i < 24; i++)
            {
                code += dict[random.Next(0, dict.Length - 1)];
            }

            string nitro = url + code;

            await ctx.Message.DeleteAsync();

            await ctx.RespondAsync(nitro);
        }

        [Command("Coinflip"), Description("Flips a coin")]
        public async Task coinflip(CommandContext ctx)
        {

            Random rnd = new Random();
            int result = rnd.Next(1, 3);
            string coinflipResult = ".";
            if (result == 1)
            {
                coinflipResult = "Heads";
            }
            else if (result == 2)
            {
                coinflipResult = "Tails";
            }

            var coinflipEmbed = new DiscordEmbedBuilder
            {
                Title = "Landed on ",
                Description = coinflipResult + "!",
                ThumbnailUrl = ctx.User.AvatarUrl,
                Color = Startup.Startup.embedDiscordColor
            };

            await ctx.Message.DeleteAsync();

            



            await ctx.RespondAsync(embed: coinflipEmbed);
        }

        [Command("info"), Description("Gets when a user created their account")]
        public async Task created(CommandContext ctx, DiscordUser user)
        {
            await ctx.Message.DeleteAsync();



            string time = Convert.ToString(user.CreationTimestamp);
            var userEmbed = new DiscordEmbedBuilder
            {
                Title = user.Username,
                Description = "Created at - " + time,
                ThumbnailUrl = user.AvatarUrl,
                Color = Startup.Startup.embedDiscordColor
            };

            

          

            
            await ctx.RespondAsync(embed: userEmbed);


        }

        [Command("gayness"), Description("Gayness bar between to people.")]
        public async Task gayness(CommandContext ctx, DiscordUser user)
        {

            Random randint = new Random();



            int yourGayness = randint.Next(1, 10);
            int theirGayness = randint.Next(1, 10);



            string gayness = "=";
            for (int i = 0; i <= yourGayness; i++)
            {
                yourGaynessInString += gayness;

            }
            for (int i = 0; i <= theirGayness; i++)
            {
                theirGaynessInString += gayness;

            }

            var gaynessEmbed = new DiscordEmbedBuilder
            {

                Title = "Gayness",
                ThumbnailUrl = ctx.User.AvatarUrl,
                Color = Startup.Startup.embedDiscordColor,
                Description = "Your gayness - \n" + yourGaynessInString + "\n" + user.Username + "'s gayness - \n" + theirGaynessInString
            };

            

            
            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync(embed: gaynessEmbed);
        }

        [Command("ban"), Description("bans a player if sufficient permissions")]
        public async Task ban(CommandContext ctx, ulong guildID, DiscordUser member, [RemainingText] string reason)
        {

            await ctx.Message.DeleteAsync();
            Discord.DiscordClient client = new Discord.DiscordClient(Startup.Startup.token);
            await Server_Based_Commands.Moderation.BanMember(client, guildID, member.Id);

            var banEmbed = new DiscordEmbedBuilder
            {
                ThumbnailUrl = ctx.User.AvatarUrl,
                Color = Startup.Startup.embedDiscordColor,
                Title = "Banned Member!",
                Description = "Just banned " + member.Username
            };

            await ctx.RespondAsync(embed: banEmbed);


        }

        [Command("roleinfo"), Description("Gets info on a role")]
        public async Task roleinfo(CommandContext ctx, ulong id)
        {

            DiscordGuild guild = ctx.Member.Guild;

            DiscordRole role = guild.GetRole(id);
            string name = role.Name;
            DateTimeOffset timestamp = role.CreationTimestamp;

            string mention = role.Mention;

            var roleInfo = new DiscordEmbedBuilder
            {
                ThumbnailUrl = ctx.User.AvatarUrl,
                Color = Startup.Startup.embedDiscordColor,
                Title = "Info on " + name,
                Description = "Role info: \n Name: " + name + "\n Time of Creation: " + timestamp + "\n Mention: " + mention + "\n\n Report Concluded."


            };

            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync(embed: roleInfo);

        }


        [Command("crash"), Description("Sends a gif that crashes discord.")]
        public async Task crash(CommandContext ctx)
        {
            string gif = "https://thumbs.gfycat.com/FavoriteHandmadeInchworm-size_restricted.gif";

            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync(gif);

        }
        [Command("spam"), Description("spams a message")]
        public async Task spam(CommandContext ctx, int amount, [RemainingText] string message)
        {
            await ctx.Message.DeleteAsync();
            for (int i = 0; i < amount; i++)
            {
                await ctx.RespondAsync(message);
                Thread.Sleep(500);
                if (i > 6) Thread.Sleep(1500);
            }
        }

        [Command("ascii")]
        public async Task ascii(CommandContext ctx, [RemainingText] string message)
        {
            string toSend = Figgle.FiggleFonts.Ogre.Render(message);

            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync("``` \n" + toSend + "\n```");

        }

        [Command("serial"), Description("Gets users serial number")]
        public async Task serial(CommandContext ctx)
        {
            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""c:""");
            dsk.Get();
            string firstId = dsk["VolumeSerialNumber"].ToString();

            var embed = new DiscordEmbedBuilder
            {
                ThumbnailUrl = ctx.User.AvatarUrl,
                Color = Startup.Startup.embedDiscordColor,
                Title = "Your HWID",
                Description = "Serial number: " + firstId


            };

            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync(embed: embed);
            DiscordGuild guild = ctx.Guild;

        }
        [Command("serverinfo"), Description("gets info on a server")]
        public async Task serverinfo(CommandContext ctx)
        {

            await ctx.Message.DeleteAsync();

            DiscordGuild guild = ctx.Guild;

            string memberCount = guild.MemberCount.ToString();

            var embed = new DiscordEmbedBuilder
            {
                ThumbnailUrl = guild.IconUrl,
                Color = Startup.Startup.embedDiscordColor,
                Title = "Info on: " + guild.Name,
                Description = "Created on: " + guild.CreationTimestamp.ToString() + "\nOwner: " + guild.Owner
            };


            await ctx.RespondAsync(embed: embed);


        }

        [Command("commands"), Description("Lists commands")]
        public async Task commands(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder
            {
                ThumbnailUrl = ctx.User.AvatarUrl,
                Color = Startup.Startup.embedDiscordColor,
                Title = "Commands",
                Description = " Commands: \n\n**Embed:** Title, Description\n**Poll:** Answer A, Answer B, Question\n**Nitro:** generates a nitro code \n**Coinflip: ** Peforms a coinflip\n**Info:** User, gets info on a user.\n**Gayness: **User, gets gayness of levels or a user\n" +
                "**Ban: ** User, bans a user if you have permissions\n**Roleinfo** Role, gets info on a role\n**Crash: ** Sends a gif that crashes discord.\n**Spam: ** Amount, Message. Spams a message x amount of times\n" +
                "**Ascii: ** Message, sends a message in ascii format\n**Serial** Gets your hard disk serial number\n**Serverinfo: ** Gets info on the current server\n**btc** Gets bitcoins current price."


            };



            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync(embed: embed);
        }

        [Command("deleteserver"), Description("kicks a player if sufficient permissions")]
        public async Task deleteserver(CommandContext ctx)
        {

            DiscordGuild guild = ctx.Member.Guild;
            await ctx.Message.DeleteAsync();

            await guild.DeleteAsync();

        }
        [Command("purge"), Description("deletes messages")]
        public async Task purge(CommandContext ctx, int amountToDelete)
        {



            DiscordGuild guild = ctx.Member.Guild;
            await ctx.Message.DeleteAsync();

            amountToDelete++;



            var messagesToDelete = await ctx.Channel.GetMessagesAsync(limit: amountToDelete);

            for (int i = 0; i < messagesToDelete.Count; i++)
            {

                if (messagesToDelete[i].Author.Username == ctx.Message.Author.Username)
                {
                    await ctx.Channel.DeleteMessageAsync(messagesToDelete[i]);
                    Thread.Sleep(200);
                }

            }




        }
        [Command("createrole"), Description("creates a role")]
        public async Task createrole(CommandContext ctx, string roleName, string color)
        {

            DiscordColor discordcolor = DiscordColor.Black;

            switch (color)
            {
                case "black":
                    discordcolor = DiscordColor.Black;
                    break;
                case "blue":
                    discordcolor = DiscordColor.Blue;
                    break;
                case "red":
                    discordcolor = DiscordColor.Red;
                    break;
                case "green":
                    discordcolor = DiscordColor.Green;
                    break;
                case "yellow":
                    discordcolor = DiscordColor.Yellow;
                    break;
                case "white":
                    discordcolor = DiscordColor.White;
                    break;
            }

            await ctx.Message.DeleteAsync();
            DiscordGuild guild = ctx.Member.Guild;
            await guild.CreateRoleAsync(name: roleName, color: discordcolor);



            var embed = new DiscordEmbedBuilder
            {
                Title = "Just Created " + roleName + " role",
                Color = discordcolor
            };
            await ctx.RespondAsync(embed: embed);

        }

        [Command("giverole"), Description("gives a role")]
        public async Task giverole(CommandContext ctx, DiscordMember user, string StringRole)
        {
            await ctx.Message.DeleteAsync();
            var role = ctx.Guild.Roles.FirstOrDefault(x => x.Name == StringRole);
            DiscordGuild guild = ctx.Guild;
            await guild.GrantRoleAsync(user, role);

            var embed = new DiscordEmbedBuilder
            {
                Title = "Given " + StringRole + " role to " + user.Username.ToString(),
                Color = role.Color

            };

            await ctx.Channel.SendMessageAsync(embed: embed);

        }

        [Command("removerole"), Description("removes a role")]
        public async Task removerole(CommandContext ctx, DiscordMember user, string StringRole)
        {
            await ctx.Message.DeleteAsync();
            var role = ctx.Guild.Roles.FirstOrDefault(x => x.Name == StringRole);
            DiscordGuild guild = ctx.Guild;
            await guild.RevokeRoleAsync(user, role, "lol");

            var embed = new DiscordEmbedBuilder
            {
                Title = "Removed " + StringRole + " role from " + user.Username.ToString(),
                Color = role.Color

            };
            await ctx.RespondAsync(embed: embed);

        }

        [Command("spamallusers"), Description("spams all users")]
        public async Task spamallusers(CommandContext ctx, int amount)
        {
            DiscordGuild guild = ctx.Guild;

            var spam = guild.Members;

            var sendMessage = "";

            await ctx.Message.DeleteAsync();

            for (int i = 0; i < amount; i++)
            {
                for (int k = 0; k < guild.MemberCount; k++)
                {
                    var message = "@" + spam.ElementAt(k);
                    sendMessage += message;
                    Thread.Sleep(100);
                }

                Thread.Sleep(500);
                if (i > 6) Thread.Sleep(1500);
                await ctx.RespondAsync(sendMessage);
            }
        }

        [Command("deleterole"), Description("deletes a role")]
        public async Task deleterole(CommandContext ctx, string StringRole)
        {
            await ctx.Message.DeleteAsync();

            var role = ctx.Guild.Roles.FirstOrDefault(x => x.Name == StringRole);
            DiscordGuild guild = ctx.Guild;

            await guild.DeleteRoleAsync(role);

            var embed = new DiscordEmbedBuilder
            {
                Title = "Deleted " + StringRole + " role",
                Color = role.Color

            };

            await ctx.RespondAsync(embed: embed);

        }
        [Command("createchannel"), Description("creates a channel")]
        public async Task createchannel(CommandContext ctx, string name, string option)
        {
            DiscordGuild guild = ctx.Guild;

            ChannelType type = ChannelType.Unknown;

            await ctx.Message.DeleteAsync();

            var embed = new DiscordEmbedBuilder
            {
                Title = "Created channel - " + name,
                Color = Startup.Startup.embedDiscordColor

            };

            if (option == "voice") type = ChannelType.Voice;
            else if (option == "text") type = ChannelType.Text;
            await guild.CreateChannelAsync(name: name, type: type);



            await ctx.RespondAsync(embed: embed);
        }



        [Command("messageUser"), Description("Gets all members of a guild and puts them in a txt file")]
        public async Task messageusers(CommandContext ctx, DiscordMember member, [RemainingText] string message)
        {
            await ctx.Message.DeleteAsync();

            await member.SendMessageAsync(content: message);

        }


        [Command("animate"), Description("animates text")]
        public async Task animate(CommandContext ctx, [RemainingText] string message)
        {


            char[] charString = message.ToCharArray();

            string completeMessage = "";

            int timer = 1250;

            for (int i = 0; i <= charString.Length; i++)
            {
                completeMessage = completeMessage + charString[i];
                await ctx.Message.ModifyAsync(completeMessage);
                Thread.Sleep(300);

                timer += 150;
                if (i > 6) Thread.Sleep(timer);
            }


        }


        [Command("ccgen"), Description("generates a credit card")]
        public async Task ccgen(CommandContext ctx, [RemainingText] string type)
        {
            await ctx.Message.DeleteAsync();

            string[] _firstName = new string[] { "Adam", "Alex", "Aaron", "Ben", "Carl", "Dan", "David", "Edward", "Fred", "Frank", "George", "Hal", "Hank", "Ike", "John", "Jack", "Joe", "Larry", "Monte", "Matthew", "Mark", "Nathan", "Otto", "Paul", "Peter", "Roger", "Roger", "Steve", "Thomas", "Tim", "Ty", "Victor", "Walter" };
            string[] _lastName = new string[] { "Anderson", "Ashwoon", "Aikin", "Bateman", "Bongard", "Bowers", "Boyd", "Cannon", "Cast", "Deitz", "Dewalt", "Ebner", "Frick", "Hancock", "Haworth", "Hesch", "Hoffman", "Kassing", "Knutson", "Lawless", "Lawicki", "Mccord", "McCormack", "Miller", "Myers", "Nugent", "Ortiz", "Orwig", "Ory", "Paiser", "Pak", "Pettigrew", "Quinn", "Quizoz", "Ramachandran", "Resnick", "Sagar", "Schickowski", "Schiebel", "Sellon", "Severson", "Shaffer", "Solberg", "Soloman", "Sonderling", "Soukup", "Soulis", "Stahl", "Sweeney", "Tandy", "Trebil", "Trusela", "Trussel", "Turco", "Uddin", "Uflan", "Ulrich", "Upson", "Vader", "Vail", "Valente", "Van Zandt", "Vanderpoel", "Ventotla", "Vogal", "Wagle", "Wagner", "Wakefield", "Weinstein", "Weiss", "Woo", "Yang", "Yates", "Yocum", "Zeaser", "Zeller", "Ziegler", "Bauer", "Baxster", "Casal", "Cataldi", "Caswell", "Celedon", "Chambers", "Chapman", "Christensen", "Darnell", "Davidson", "Davis", "DeLorenzo", "Dinkins", "Doran", "Dugelman", "Dugan", "Duffman", "Eastman", "Ferro", "Ferry", "Fletcher", "Fietzer", "Hylan", "Hydinger", "Illingsworth", "Ingram", "Irwin", "Jagtap", "Jenson", "Johnson", "Johnsen", "Jones", "Jurgenson", "Kalleg", "Kaskel", "Keller", "Leisinger", "LePage", "Lewis", "Linde", "Lulloff", "Maki", "Martin", "McGinnis", "Mills", "Moody", "Moore", "Napier", "Nelson", "Norquist", "Nuttle", "Olson", "Ostrander", "Reamer", "Reardon", "Reyes", "Rice", "Ripka", "Roberts", "Rogers", "Root", "Sandstrom", "Sawyer", "Schlicht", "Schmitt", "Schwager", "Schutz", "Schuster", "Tapia", "Thompson", "Tiernan", "Tisler" };

            Random rand = new Random();

            int intfirstName_ = rand.Next(1, _firstName.Length);
            string firstName_ = _firstName[intfirstName_];
            int intLastName_ = rand.Next(1, _lastName.Length);
            string lastName_ = _lastName[intfirstName_];

            int cardNumber = 0;
            string cardNumberFull = "";

            for (int i = 0; i < 4; i++)
            {
                int number = rand.Next(1, 10);
                cardNumberFull = cardNumberFull + number.ToString();
            }
            cardNumberFull = cardNumberFull + " ";

            for (int i = 0; i < 4; i++)
            {
                int number = rand.Next(1, 10);
                cardNumberFull = cardNumberFull + number.ToString();
            }
            cardNumberFull = cardNumberFull + " ";

            for (int i = 0; i < 4; i++)
            {
                int number = rand.Next(1, 10);
                cardNumberFull = cardNumberFull + number.ToString();
            }
            cardNumberFull = cardNumberFull + " ";

            for (int i = 0; i < 4; i++)
            {
                int number = rand.Next(1, 10);
                cardNumberFull = cardNumberFull + number.ToString();
            }
            cardNumberFull = cardNumberFull + " ";



            int[] months = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] year = new int[] { 21, 22, 23, 24 };

            string expiryDate;
            int monthIndex = rand.Next(1, months.Length);
            int yearIndex = rand.Next(1, year.Length);

            int monthInt = months[monthIndex];
            int yearInt = year[yearIndex];

            string ccvString = "";
            for (int i = 0; i < 3; i++)
            {
                int Digit = rand.Next(1, 10);
                ccvString = ccvString + Digit.ToString();

            }

            expiryDate = "0" + monthInt.ToString() + "/" + yearInt.ToString();


            var embed = new DiscordEmbedBuilder
            {
                ThumbnailUrl = "https://media.discordapp.net/attachments/816402177374289964/838865381531713586/genericcreditcard.jpg",
                Color = Startup.Startup.embedDiscordColor,
                Title = "Credit Card Generated ",
                Description = "Card Holder Name: " + firstName_ + " " + lastName_ + "\n Credit Card Number: " + cardNumberFull + "\n Expiry Date: " + expiryDate + "\n CCV: " + ccvString
            };


            await ctx.RespondAsync(embed: embed);

        }

        [Command("suggestion"), Description("Send a suggestion")]
        public async Task suggestion(CommandContext ctx, [RemainingText] string suggestion)
        {
            await ctx.Message.DeleteAsync();
            var embed = new DiscordEmbedBuilder
            {
                ThumbnailUrl = ctx.User.AvatarUrl,
                Color = Startup.Startup.embedDiscordColor,
                Title = "Suggestion",
                Description = suggestion
            };

            var message = await ctx.RespondAsync(embed: embed);

            var aEmoji = DiscordEmoji.FromName(ctx.Client, ":white_check_mark:");
            var bEmoji = DiscordEmoji.FromName(ctx.Client, ":negative_squared_cross_mark:");


            await message.CreateReactionAsync(aEmoji).ConfigureAwait(false);
            Thread.Sleep(600);
            await message.CreateReactionAsync(bEmoji).ConfigureAwait(false);

        }
        [Command("shrug")]
        public async Task shrug(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync("¯_(ツ)_/¯");
        }
        [Command("tableflip")]
        public async Task tableflip(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync("(╯°□°）╯︵ ┻━┻");

        }

        [Command("kick"), Description("kicks a member")]
        public async Task kick(CommandContext ctx,ulong guildID, DiscordUser member)
        {
            await ctx.Message.DeleteAsync();
            await ctx.Message.DeleteAsync();
            Discord.DiscordClient client = new Discord.DiscordClient(Startup.Startup.token);
            await Server_Based_Commands.Moderation.BanMember(client, guildID, member.Id);

            var banEmbed = new DiscordEmbedBuilder
            {

                ImageUrl = ctx.User.AvatarUrl,
                Color = Startup.Startup.embedDiscordColor,
                Title = "Kicked Member!",
                Description = "Just kicked " + member.Username
            };

            await ctx.RespondAsync(embed: banEmbed);


        }
        [Command("pin"), Description("pins a message")]
        public async Task pin(CommandContext ctx, [RemainingText] string message)
        {
            await ctx.Message.DeleteAsync();

            await ctx.Channel.SendMessageAsync(message);
            await ctx.Message.PinAsync();
        }

        [Command("calculate"), Description("calculator")]
        public async Task calculate(CommandContext ctx, float firstNo, string operat, float secondNo)
        {
            await ctx.Message.DeleteAsync();
            float result;

            switch (operat)
            {
                case "+":
                    result = add(firstNo, secondNo);
                    await ctx.Channel.SendMessageAsync(content: result.ToString());
                    break;
                case "-":
                    result = substract(firstNo, secondNo);
                    await ctx.Channel.SendMessageAsync(content: result.ToString());
                    break;
                case "/":
                    result = divide(firstNo, secondNo);
                    await ctx.Channel.SendMessageAsync(content: result.ToString());
                    break;
                case "*":
                    result = multiply(firstNo, secondNo);
                    await ctx.Channel.SendMessageAsync(content: result.ToString());
                    break;

            }


        }

        /*
        #region coc
        [Command("cocplayer"), Description("Everything to do with clash of clans.")]
        public async Task coc(CommandContext ctx, [RemainingText] string id)
        {
            await ctx.Message.DeleteAsync();

            ClashOfClansClient client = new ClashOfClansClient("szqcrnc8");



            Player player = await client.Players.GetPlayerAsync(id);



            var discordembed = new DiscordEmbedBuilder
            {
                ThumbnailUrl = player.League.IconUrls.ToString(),
                Title = player.Name,
                Description = "Trophies: " + player.Trophies.ToString()

            };


            await ctx.Channel.SendMessageAsync(embed: discordembed);


        }
        #endregion

        */
        #region calculate
        float add(float a, float b)
        {
            return a + b;
        }
        float substract(float a, float b)
        {
            return a - b;
        }
        float multiply(float a, float b)
        {
            return a * b;
        }
        float divide(float a, float b)
        {
            return a / b;
        }
        #endregion

        private int maxNumber = 0;
        private int currentNumber = 0;

        [Command("comic")]
        public async Task comic(CommandContext ctx, [RemainingText] int number = 0)
        {
            Comic.ComicGetter comicGetter = new Comic.ComicGetter();
            var comic = await comicGetter.loadComic(comicNumber: number);


            var embed = new DiscordEmbedBuilder
            {
                Title = "Comic - " + comic.title,
                ImageUrl = comic.img

            };

            await ctx.Message.DeleteAsync();

            await ctx.RespondAsync(embed: embed);


        }

        //not working
        public string[] arrayOfChoices;

        [Command("choices")]
        public async Task choices(CommandContext ctx, [RemainingText] string choices)
        {
            await ctx.Message.DeleteAsync();
            string[] splitChoices = choices.Split(' ');

            for (int i = 0; i < splitChoices.Length; i++)
            {
                arrayOfChoices[i] = splitChoices[i];
            }

            Random rnd = new Random();
            int randomInt = rnd.Next(1, arrayOfChoices.Length);
            string choice = arrayOfChoices[randomInt];
            await ctx.Channel.SendMessageAsync(choice);

        }

        [Command("server")]
        public async Task server(CommandContext ctx, string choice)
        {

            string title = "";
            string description = "error";
            string imageurl = "";

            if (choice == "owner")
            {
                DiscordMember user = ctx.Guild.Owner;

                title = "Owner of the server is " + user.DisplayName;
                description = "";
                imageurl = user.AvatarUrl;
            }
            else if (choice == "name")
            {
                DiscordGuild guild = ctx.Member.Guild;
                title = "Server name - " + guild.Name;
                description = "";
                imageurl = guild.IconUrl;


            }
            else if (choice == "sid")
            {
                DiscordGuild guild = ctx.Guild;
                title = "The server id is - " + guild.Id.ToString();
                imageurl = guild.IconUrl;
                description = "";
            }
            else if (choice == "roles")
            {
                DiscordGuild guild = ctx.Guild;
                StringBuilder builder = new StringBuilder("");
                for (int i = 0; i < guild.Roles.Count; i++)
                {
                    builder.Append(guild.Roles[i].Name + "\n");
                }
                title = "Roles for " + guild.Name + " server";
                description = builder.ToString();
                imageurl = guild.IconUrl;
            }
            else if (choice == "users")
            {
                DiscordGuild guild = ctx.Guild;
                StringBuilder builder = new StringBuilder("");
                for (int i = 0; i < guild.MemberCount; i++)
                {
                    builder.Append(guild.Members[i].Username + "\n");
                }
                title = "Users in " + guild.Name + " server";
                description = builder.ToString();
                imageurl = guild.IconUrl;
            }


            await ctx.Message.DeleteAsync();

            var embed = new DiscordEmbedBuilder
            {
                Title = title,
                Description = description,
                ImageUrl = imageurl,
                Color = Startup.Startup.embedDiscordColor
            };
            await ctx.RespondAsync(embed: embed);

        }



        [Command("loadcommandtemplate")]
        public async Task loadcommandtemplate(CommandContext ctx)
        {


            Models.CustomCommandsModel com = new Models.CustomCommandsModel();
            com.CommandName = "name (change me)";
            com.CommandContent = "content (change me)";
            string output = JsonSerializer.Serialize(com);
            File.WriteAllText("Command.json", output);

            await ctx.Message.DeleteAsync();
        }

        public static string COMMANDNAME = getCommandName();

        public static string commandContent = getCommandContent();


        public static string getCommandName()
        {
            CustomCommands.JsonParser parser = new CustomCommands.JsonParser();
            var model = parser.getCommand("Command.json");
            string commandName = model.CommandName;
            string commandContent = model.CommandContent;
            return commandName;
        }
        public static string getCommandContent()
        {
            CustomCommands.JsonParser parser = new CustomCommands.JsonParser();
            var model = parser.getCommand("Command.json");
            string commandName = model.CommandName;
            string commandContent = model.CommandContent;
            return commandContent;
        }

        [Command("c")]
        public async Task c(CommandContext ctx, string commandName)
        {


            await ctx.Message.DeleteAsync();
            if (commandName == COMMANDNAME)
            {
                await ctx.RespondAsync(commandContent);
            }
        }
        public static DiscordColor colourOfEmbed;
        [Command("Myembed")]
        public async Task myembed(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync();
            if (File.Exists("EmbedJson.json"))
            {

                StreamReader r = new StreamReader("EmbedJson.json");
                string json = r.ReadToEnd();

                var customEmbed = JsonSerializer.Deserialize<EmbedModel>(json);

                DiscordEmbedBuilder.EmbedFooter footer = new DiscordEmbedBuilder.EmbedFooter();
                footer.Text = customEmbed.Footer;
                colourOfEmbed = Startup.Startup.embedDiscordColor;

                switch (customEmbed.Colour)
                {
                    case "Red":
                        colourOfEmbed = DiscordColor.Red;
                        break;
                    case "Blue":
                        colourOfEmbed = DiscordColor.Blue;
                        break;
                    case "White":
                        colourOfEmbed = DiscordColor.White;
                        break;
                    case "Black":
                        colourOfEmbed = DiscordColor.Black;
                        break;

                }
                var embed = new DiscordEmbedBuilder
                {
                    Title = customEmbed.Title,
                    Description = customEmbed.Description,
                    Color = colourOfEmbed,
                    ImageUrl = customEmbed.ImageURL,
                    ThumbnailUrl = customEmbed.ThumbnailURL,
                    Footer = footer

                };
                await ctx.RespondAsync(embed: embed);
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Use the embed visualizer or create a json file called EmbedJson.json and follow the correct format.");

                Thread.Sleep(4000);
                await ctx.Message.DeleteAsync();
            }


        }
        [Command("minecraftPlayer")]
        public async Task yanchop(CommandContext ctx, string args)
        {


            Yanchop.YanchopFunctions func = new Yanchop.YanchopFunctions();



            var model = await func.getPlayer(args);




            var embed = new DiscordEmbedBuilder
            {
                Title = "Basic info on " + args,
                Description = "ID: " + model.id,
                Color = Startup.Startup.embedDiscordColor,
                ImageUrl = "https://minotar.net/body/" + args
            };
            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync(embed: embed);

        }

        [Command("ipcheck")]
        public async Task ipcheck(CommandContext ctx, string ip)
        {
            Ip_Tools.IpLookupFuncs funcs = new Ip_Tools.IpLookupFuncs();
            var model = await funcs.getGeo(ip);



            var tim_zone = model.time_zone;
            var embed = new DiscordEmbedBuilder
            {
                Title = "Info on " + ip,
                Color = Startup.Startup.embedDiscordColor,
                Description = "Country: " + model.country_name + "\nCity: " + model.city + "\nCurrent Time: " + tim_zone.current_time + "\nISP: " + model.isp
            };

            await ctx.RespondAsync(embed: embed);
            await ctx.Message.DeleteAsync();
        }

        [Command("nuke")]
        public async Task nuke(CommandContext ctx, string choice)
        {
            DiscordGuild guild = ctx.Guild;
            await ctx.Message.DeleteAsync();
            if (choice == "ban")
            {
                for (int i = 0; i < guild.MemberCount; i++)
                {
                    await guild.BanMemberAsync(guild.Members[i]);
                    Thread.Sleep(150 + i * 2);
                }
            }

        }
       

        [Command("define")]
        public async Task define(CommandContext ctx, string word)
        {

            IEnumerable<gnuciDictionary.Word> definitions = gnuciDictionary.EnglishDictionary.Define(word);


            await ctx.Message.DeleteAsync();

            var embed = new DiscordEmbedBuilder
            {
                Title = "Definition on " + word,
                Description = definitions.First().ToString(),
                Color = Startup.Startup.embedDiscordColor
            };
            await ctx.RespondAsync(embed: embed);

        }
        [Command("urban")]
        public async Task urban(CommandContext ctx, [RemainingText] string word)
        {
            await ctx.Message.DeleteAsync();
            Searching.SearchingFuncs funcs = new Searching.SearchingFuncs();
            var model = await funcs.getUrbanDef(word);
            var embed = new DiscordEmbedBuilder
            {
                Title = "Urban Definition on " + word,
                Description = model.list[0].definition,
                Color = Startup.Startup.embedDiscordColor
            };
            await ctx.RespondAsync(embed: embed);
        }
        [Command("cat")]
        public async Task cat(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync();
            Searching.SearchingFuncs funcs = new Searching.SearchingFuncs();
            var model = await funcs.getCatPhoto();
            var embed = new DiscordEmbedBuilder
            {
                ImageUrl = model.url,
                Color = Startup.Startup.embedDiscordColor

            };
            await ctx.RespondAsync(embed: embed);
        }
        [Command("dog")]
        public async Task dog(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync();
            Searching.SearchingFuncs funcs = new Searching.SearchingFuncs();
            var model = await funcs.getDogPhoto();
            var embed = new DiscordEmbedBuilder
            {
                ImageUrl = model.message,
                Color = Startup.Startup.embedDiscordColor

            };
            await ctx.RespondAsync(embed: embed);
        }
        [Command("pokemon")]
        public async Task pokemon(CommandContext ctx, string pokemonName)
        {

            Searching.Pokemon.PokemonSearch search = new Searching.Pokemon.PokemonSearch();
            var model = await search.getPokemonStats(pokemonName);
            string title = model.name.ToUpperInvariant();

            var embed = new DiscordEmbedBuilder
            {
                Title = title,
                Url = $"https://www.pokemon.com/uk/pokedex/" + model.name,
                Color = Startup.Startup.embedDiscordColor,
                ImageUrl = model.sprites.front_default


            };
            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync(embed: embed);
        }
        [Command("space")]
        public async Task space(CommandContext ctx)
        {
            await ctx.Message.DeleteAsync();
            Searching.Space.NASASearch search = new Searching.Space.NASASearch();
            var model = await search.getNASAPhoto();

            var embed = new DiscordEmbedBuilder
            {
                Title = "NASA Photo",
                ImageUrl = model.hdurl,
                Color = Startup.Startup.embedDiscordColor

            };

            await ctx.RespondAsync(embed: embed);
        }
        [Command("overlay")]
        public async Task overlay(CommandContext ctx, DiscordUser user, string choice)
        {
            await ctx.Message.DeleteAsync();
            Overlay.OverlayFuncs funcs = new Overlay.OverlayFuncs();

            var client = new HttpClient();
            string url = user.AvatarUrl;


            var embed = new DiscordEmbedBuilder
            {
                Color = Startup.Startup.embedDiscordColor,
                ImageUrl = $"https://some-random-api.ml/canvas/" + choice + "/?avatar=" + url
            };
            await ctx.RespondAsync(embed: embed);

        }
        [Command("nsfw")]
        public async Task nsfw(CommandContext ctx, string tag)
        {
            await ctx.Message.DeleteAsync();
            Searching.nsfw.nsfwSearchFuncs nsfwfunc = new Searching.nsfw.nsfwSearchFuncs();
            var model = await nsfwfunc.getnsfw(tag);
            var embed = new DiscordEmbedBuilder
            {
                Color = Startup.Startup.embedDiscordColor,
                ImageUrl = model.link
            };
            await ctx.RespondAsync(embed: embed);
        }
        [Command("qrcode")]
        public async Task qrcode(CommandContext ctx, [RemainingText]string search)
        {
            await ctx.Message.DeleteAsync();

            var embed = new DiscordEmbedBuilder
            {
                Title = "QR code for " + search,
                Color = Startup.Startup.embedDiscordColor,
                ImageUrl = "https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=" + search
            };
            await ctx.RespondAsync(embed: embed);
        }

        [Command("msgall")]
        public async Task msgall(CommandContext ctx, ulong guildId, ulong channelId, [RemainingText] string message)
        {
            await ctx.Message.DeleteAsync();
			#region test
			/*
            Discord.Gateway.DiscordSocketClient client = new Discord.Gateway.DiscordSocketClient();
            client.Token = Startup.Startup.token;
            client.Login(client.Token);
            Console.WriteLine("working 1");
            var guild = GetGuildAnarchy.getGuild(client, guildId);
            Console.WriteLine("working 2");
            var users = await ConvertIDToUserAsync(GetGuildAnarchy.userList(guild, client, channelId), ctx);
            Console.WriteLine("working 3");
            await MessageAllUsers(users, ctx, message);
            Console.WriteLine("working 4");
            */
			#endregion

		}
		#region msgallfuncs
		public static async Task<List<DiscordUser>> ConvertIDToUserAsync(List<ulong> list, CommandContext ctx)
        {
            List<DiscordUser> userList = new List<DiscordUser>();
            for (int i = 0; i < list.Count; i++)
            {
                var userToBeAdded = await ctx.Client.GetUserAsync(list[i]);
                userList.Add(userToBeAdded);

            }
            Console.WriteLine("working 5");
            return userList;
        }
        public async Task MessageAllUsers(List<DiscordUser> users, CommandContext ctx, string text)
        {
            for (int i = 0; i < users.Count; i++)
            {

                var dm = await ctx.Client.CreateDmAsync(users[i]);
                await dm.SendMessageAsync(text);
                Thread.Sleep(150 + i * 2);
            }
            Console.WriteLine("working 6");

        }
		#endregion
        [Command("aniNick")]
        public async Task aniNick(CommandContext ctx, ulong guildID, string nick)
		{
            await ctx.Message.DeleteAsync();

            Discord.DiscordClient client = new Discord.DiscordClient(Startup.Startup.token);
            Discord.DiscordGuild guild = GetGuildAnarchy.getGuild(client, guildID);
            string currentNick = "";
            string nickname = nick;
            while (true)
            {
                for (int i = 0; i < nickname.Length; i++)
                {
                    currentNick += nickname[i];
                    guild.SetNickname(currentNick);
                    Thread.Sleep(1000);
                }

                currentNick = "";
            }
        }
        //https://cdn.discordapp.com/attachments/848272167225393275/855361030807748608/video0.mp4
        [Command("sniper")]
        public async Task sniper(CommandContext ctx)
		{
            Discord.Gateway.DiscordSocketClient client = new Discord.Gateway.DiscordSocketClient();
            client.OnLoggedIn += GetGuildAnarchy.Client_OnLoggedIn;
            client.OnMessageReceived += GetGuildAnarchy.Client_OnMessageReceived;
            client.Login(Startup.Startup.token);
            await ctx.Message.DeleteAsync();
        }
        [Command("bigfloppafriday")]
        public async Task floppa(CommandContext ctx)
		{
            await ctx.Message.DeleteAsync();
            await ctx.RespondAsync("https://cdn.discordapp.com/attachments/848272167225393275/855361030807748608/video0.mp4");
		}
		
        [Command("massmention")]
        public async Task massmention(CommandContext ctx, ulong guildID)
		{
            await ctx.Message.DeleteAsync();
           
            
            
		}
        [Command("help")]
        public async Task help(CommandContext ctx)
		{
            await ctx.Message.DeleteAsync();

            using FileStream fs = File.OpenRead("PhoneSBCommands.txt");
            await ctx.RespondWithFileAsync(fs);
		}
       
        
	}
}


