﻿using Discord;
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

        //The marine response
        String[] marineText = {
        "What the fuck did you just fucking say about me you little bitch I’ll have you know I graduated top of my class in the Navy Seals and I’ve been involved in numerous secret raids",
        "on Al-Quaeda and I have over 300 confirmed kills I am trained in gorilla warfare and I’m the top sniper in the entire US armed forces You are nothing to me but just another target",
        "I will wipe you the fuck out with precision the likes of which has never been seen before on this Earth mark my fucking words You think you can get away with saying that shit to me",
        "over the Internet Think again fucker As we speak I am contacting my secret network of spies across the USA and your IP is being traced right now so you better prepare for the storm",
        "maggot The storm that wipes out the pathetic little thing you call your life You’re fucking dead kid I can be anywhere anytime and I can kill you in over seven hundred ways and",
        "that’s just with my bare hands Not only am I extensively trained in unarmed combat but I have access to the entire arsenal of the United States Marine Corps and I will use it to its",
        "full extent to wipe your miserable ass off the face of the continent you little shit If only you could have known what unholy retribution your little “clever” comment was about to",
        "bring down upon you maybe you would have held your fucking tongue But you couldn’t you didn’t and now you’re paying the price you goddamn idiot I will shit fury all over you",
        "and you will drown in it You’re fucking dead, kiddo."
        };

        //String[] ArgText = {
        //"What in Davy Jones’ locker did ye just bark at me, ye scurvy bilgerat? I’ll have ye know I be the meanest cutthroat on the seven seas,",
        //"and I’ve led numerous raids on fishing villages, and raped over 300 wenches.I be trained in hit-and-run pillaging and be the deadliest with a pistol of all the captains",
        //"on the high seas.Ye be nothing to me but another source o’ swag.I’ll have yer guts for garters and keel haul ye like never been done before, hear me true. You think ye can hide behind",
        //"your newfangled computing device? Think twice on that, scallywag. As we parley I be contacting my secret network o’ pirates across the sea and yer port is being tracked right now",
        //"so ye better prepare for the typhoon, weevil. The kind o’ monsoon that’ll wipe ye off the map.You’re sharkbait, fool.I can sail anywhere, in any waters, and can kill ye",
        //"in o’er seven hundred ways, and that be just with me hook and fist.Not only do I be top o’ the line with a cutlass, but I have an entire pirate fleet at my beck and call and I’ll damned",
        //"sure use it all to wipe yer arse off o’ the world, ye dog. If only ye had had the foresight to know what devilish wrath your jibe was about to incur, ye might have belayed the comment.",
        //"But ye couldn’t, ye didn’t, and now ye’ll pay the ultimate toll, you buffoon. I’ll shit fury all over ye and ye’ll drown in the depths o’ it.You’re fish food now, lad."
        //};

        //String[] DoItText = {
        //"DO IT, just DO IT! Don’t let your dreams be dreams. Yesterday, you said tomorrow. So just. DO IT! Make. your dreams. COME TRUE! Just… do it! Some people dream of success,",
        //"while you’re gonna wake up and work HARD at it! NOTHING IS IMPOSSIBLE!You should get to the point where anyone else would quit, and you’re not gonna stop there.",
        //"NO! What are you waiting for? … DO IT! Just… DO IT! Yes you can! Just do it! If you’re tired of starting over, stop. giving. up."
        // };

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

            //Used to bypass the need for a prefix and will listen for key words as commands
            ListenForCommands();
            //Says in TTS who joins a Voice Channel
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
                        else if (e.Message.Text.ToLower().Contains("coin"))
                        {
                                if (rand.Next() % 2 == 0)
                                {
                                    await e.Channel.SendMessage("Heads");
                                }
                                else
                                {
                                    await e.Channel.SendMessage("Tails");
                                }
                        }
                        //else if (e.Message.Text.ToLower().Contains("roll"))
                        //{

                        //    await e.Channel.SendMessage("");
                        //}
                        else
                        {
                            await e.Channel.SendMessage("fuk u");
                        }
                    }


                    if (e.Message.Text.ToLower().Contains("ree"))
                    {
                        foreach (string marine in marineText)
                        {
                            await e.Channel.SendTTSMessage(marine);
                        }
                    }
                }
            };
        }
        private void AnnounceVCJoin()
        {
            discord.UserUpdated += async (s, e) =>
            {
                var logChannel = e.Server.FindChannels("autistsunitedhq").FirstOrDefault();

                if (e.After.VoiceChannel == null) return;

                if (e.Before.VoiceChannel == e.After.VoiceChannel) return;

                await logChannel.SendTTSMessage($"{e.After.Name} Joined {e.After.VoiceChannel}");
            };
        }


        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
}
