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

        Random rand = new Random();

        string directory = @".\Memes\";
        List<string> memeImages = new List<string>();

        string marine1 = "What the fuck did you just fucking say about me you little bitch I’ll have you know I graduated top of my class in the Navy Seals and I’ve been involved in numerous secret raids";
        string marine2 = "on Al-Quaeda and I have over 300 confirmed kills I am trained in gorilla warfare and I’m the top sniper in the entire US armed forces You are nothing to me but just another target";
        string marine3 = "I will wipe you the fuck out with precision the likes of which has never been seen before on this Earth mark my fucking words You think you can get away with saying that shit to me";
        string marine4 = "over the Internet Think again fucker As we speak I am contacting my secret network of spies across the USA and your IP is being traced right now so you better prepare for the storm";
        string marine5 = "maggot The storm that wipes out the pathetic little thing you call your life You’re fucking dead kid I can be anywhere anytime and I can kill you in over seven hundred ways and";
        string marine6 = "that’s just with my bare hands Not only am I extensively trained in unarmed combat but I have access to the entire arsenal of the United States Marine Corps and I will use it to its";
        string marine7 = "full extent to wipe your miserable ass off the face of the continent you little shit If only you could have known what unholy retribution your little “clever” comment was about to"; 
        string marine8 = "bring down upon you maybe you would have held your fucking tongue But you couldn’t you didn’t and now you’re paying the price you goddamn idiot I will shit fury all over you";
        string marine9 = "and you will drown in it You’re fucking dead, kiddo.";

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

            ListenForCommands();
            AnnounceVCJoin();


            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzAzMjY2MTY4MDMzMTE2MTYz.C9VrTg.GItnjGeV-JRpg3qG2OKC9JpDqTw", TokenType.Bot);
            });

        }

        private void ListenForCommands()
        {

            foreach (string myMeme in Directory.GetFiles(directory, "*.jpg", SearchOption.AllDirectories))
            {
                memeImages.Add(myMeme);
            }

                discord.MessageReceived += async (s, e) =>
                {
                if (!e.Message.IsAuthor)
                {
                    //await e.Channel.SendMessage(e.Message.Text);
                    if (e.Message.Text.Contains("$"))
                    {
                        if (e.Message.Text.ToLower().Contains("meme"))
                        {
                            int randMeme = rand.Next(memeImages.ToArray().Length);
                            string memeToPost = memeImages[randMeme];
                            await e.Channel.SendFile(memeToPost);
                        }
                        else
                        {
                            await e.Channel.SendMessage("fuk u");
                        }
                    }
                    if (e.Message.Text.ToLower().Contains("re"))
                    {
                        await e.Channel.SendTTSMessage(marine1);
                        await e.Channel.SendTTSMessage(marine2);
                        await e.Channel.SendTTSMessage(marine3);
                        await e.Channel.SendTTSMessage(marine4);
                        await e.Channel.SendTTSMessage(marine5);
                        await e.Channel.SendTTSMessage(marine6);
                        await e.Channel.SendTTSMessage(marine7);
                        await e.Channel.SendTTSMessage(marine8);
                        await e.Channel.SendTTSMessage(marine9);
                        }
                }
            };
        }
        private void AnnounceVCJoin()
        {

            discord.UserUpdated += async (s, e) =>
            {
                var logChannel = e.Server.FindChannels("disabledautists").FirstOrDefault();

                if (e.After.VoiceChannel == null) return;

                if (e.Before.VoiceChannel == e.After.VoiceChannel) return;

                await logChannel.SendTTSMessage($"{e.After.Name} Joined");
            };
        }


        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
}
