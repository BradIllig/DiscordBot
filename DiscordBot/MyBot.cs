using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;

        string[] Memes;
        Random rand = new Random();

        public MyBot()
        {    
            // Enter name of JPG here
            Memes = new string[]
                {
                    "meme/"
                };

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '$';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            RegisterMemeCommand();
            RegisterResponseCommand();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzAzMjY2MTY4MDMzMTE2MTYz.C9VrTg.GItnjGeV-JRpg3qG2OKC9JpDqTw", TokenType.Bot);
            });

        }

        private void RegisterMemeCommand()
        {
            commands.CreateCommand("meme")
                .Do(async (e) =>
                {

                    int randMeme = rand.Next(Memes.Length);
                    string memeToPost = Memes[randMeme];
                    await e.Channel.SendFile(memeToPost);
                });
        }

        private void RegisterResponseCommand()
        {
            commands.CreateCommand("hello")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("fuk u");
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
