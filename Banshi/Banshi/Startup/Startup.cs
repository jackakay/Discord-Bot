using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using System.Runtime.InteropServices;

using DSharpPlus.Entities;
using System.Diagnostics;

using System.Linq;
using System.Threading;
using System.Windows;
using ComicAPI;


namespace Banshi.Startup
{
    class Startup : IDisposable
    {
        private DiscordClient _discordClient;
        private CommandsNextModule _commandsNext;
     
        readonly bool disposed = false;
        public static string token = "";


        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public static DiscordColor embedDiscordColor;


        static void Main()
        {
            


            Console.Title = "Selfbot by Jack.#9999";

            Console.WriteLine(Figgle.FiggleFonts.Spliff.Render("Phone SelfBot"));

            /* RUN CONFIG CHECKS */


            string filePath = @"token.txt";
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
                 
                Console.WriteLine("Fill in your token file.");
                Thread.Sleep(3000);
                Environment.Exit(0);


            }



            string tokenText = ReadSpecificLine(filePath, 1);

            if (String.IsNullOrWhiteSpace(tokenText))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Put your token in token.txt");
            }


            int count = File.ReadAllLines(@"token.txt").Length;


            if (count < 2)
            {
                embedDiscordColor = DiscordColor.Azure;

            }
            string colourText = ReadSpecificLine(filePath, 2);
            switch (colourText)
            {
                case "black":
                    embedDiscordColor = DiscordColor.Black;
                    break;
                case "blue":
                    embedDiscordColor = DiscordColor.Blue;
                    break;
                case "red":
                    embedDiscordColor = DiscordColor.Red;
                    break;
                case "green":
                    embedDiscordColor = DiscordColor.Green;
                    break;
                case "yellow":
                    embedDiscordColor = DiscordColor.Yellow;
                    break;
                case "white":
                    embedDiscordColor = DiscordColor.White;
                    break;
            }
            string backround = ReadSpecificLine(filePath, 3);

            var handle = GetConsoleWindow();

            if (backround == "no")
            {
                ShowWindow(handle, SW_SHOW);

            }
            else if (backround == "yes")
            {
                ShowWindow(handle, SW_HIDE);
                MessageBox.Show("Running the selfBot in the backround.");


            }



            token = tokenText;
          
            Startup startupClass = new Startup();
           
            startupClass.Run().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        async Task Run()
        {
            try
            {
                var clientConfig = new DiscordConfiguration
                {
                    LogLevel = LogLevel.Critical,
                    Token = token,
                    TokenType = TokenType.User,
                    UseInternalLogHandler = true
                };
                var commandConfig = new CommandsNextConfiguration
                {
                    StringPrefix = ">",
                    EnableDefaultHelp = false,
                    SelfBot = true,
                    CaseSensitive = false
                };

               
                _discordClient = new DiscordClient(clientConfig);

               
                _commandsNext = _discordClient.UseCommandsNext(commandConfig);

               
                _commandsNext.RegisterCommands<Commands.Commands>();

                

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(DateTime.Now + ":" + " Logged in!");

                _commandsNext.CommandErrored += Logging.Logging.CommandErrored;
                _commandsNext.CommandExecuted += Logging.Logging.CommandExecuted;
                

                await _discordClient.ConnectAsync();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(0);
            }
            await Task.Delay(-1);
        }


       
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            if (disposing)
            {
                Run().Dispose();
            }

        }

        public static string ReadSpecificLine(string filePath, int lineNumber)
        {
            string content = null;
            try
            {
                using (StreamReader file = new StreamReader(filePath))
                {
                    for (int i = 1; i < lineNumber; i++)
                    {
                        file.ReadLine();

                        if (file.EndOfStream)
                        {
                            Console.WriteLine($"End of file.  The file only contains {i} lines.");
                            break;
                        }
                    }
                    content = file.ReadLine();
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("There was an error reading the file: ");
                Console.WriteLine(e.Message);
            }

            return content;

        }
        public static int CountLines(FileInfo file) => File.ReadLines(file.FullName).Count();



    }
}
