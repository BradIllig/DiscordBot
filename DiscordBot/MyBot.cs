using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DiscordBot
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;

        string[] Memes;
        Random rand = new Random();

        string directory = @".\Memes\";
        List<string> memeImages = new List<string>();


    public MyBot()
        {
            
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
            foreach (string myMeme in Directory.GetFiles(directory, "*.jpg", SearchOption.AllDirectories))
            {
                memeImages.Add(myMeme);
            }

            commands.CreateCommand("freshmemepls")
                .Do(async (e) =>
                {
                    int randMeme = rand.Next(memeImages.ToArray().Length);
                    string memeToPost = memeImages[randMeme];
                    await e.Channel.SendFile(memeToPost);
                    await e.Channel.SendMessage("*Send your memes to* **Illigbrad@hotmail.com**");
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
