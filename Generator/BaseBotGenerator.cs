using Generator.Helpers;
using Generator.Models;
using Generator.Models.Input;
using Generator.Models.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Generator
{
    public class BaseBotGenerator
    {
        private readonly List<Datum> _rawParsedBots;

        public BaseBotGenerator(List<Datum> parsedBots)
        {
            _rawParsedBots = parsedBots;
        }

        public List<Bot> AddBaseDetails()
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot base");

            var assaultBot = new Bot(BotType.assault);
            var raiderBot = new Bot(BotType.pmcBot);
            var marksmanBot = new Bot(BotType.marksman);

            var rawBots = new List<Bot>
            {
                assaultBot,
                raiderBot,
                marksmanBot
            };

            foreach (var botToUpdate in rawBots)
            {
                var rawBotData = _rawParsedBots
                    .Where(x => string.Equals(x.Info.Settings.Role, botToUpdate.botType.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
                UpdateBodyPartHealth(botToUpdate, rawBotData);
            }

            foreach (var bot in rawBots)
            {
                AddDifficulties(bot, _rawParsedBots);
                foreach (var rawParsedBot in _rawParsedBots)
                {
                    AddVisualAppearanceItems(bot, rawParsedBot);
                    AddName(bot, rawParsedBot.Info.Nickname);
                    AddVoice(bot, rawParsedBot);
                }
            }

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot base. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");


            return rawBots;
        }

        private void AddVoice(Bot bot, Datum rawParsedBot)
        {
            if (bot.botType == BotType.assault || bot.botType == BotType.marksman)
            {
                if (rawParsedBot.Info.Voice.StartsWith("scav", StringComparison.InvariantCultureIgnoreCase))
                {
                    bot.appearance.voice.AddUnique(rawParsedBot.Info.Voice);
                }
            }
            
        }

        private void AddDifficulties(Bot bot, List<Datum> rawParsedBots)
        {
            var botType = bot.botType;
            var firstBotOfDesiredType = rawParsedBots.First(x => x.Info.Settings.Role == botType.ToString());

            switch (botType)
            {
                case BotType.assault:
                    DifficultyHelper.AddAssaultDifficulties(bot);
                    break;
                case BotType.pmcBot:
                    break;
                case BotType.marksman:
                    break;
                default:
                    break;
            }
        }

        private void UpdateBodyPartHealth(Bot botToUpdate, List<Datum> rawParsedBots)
        {
            var firstBotOfDesiredType = rawParsedBots.First(x => x.Info.Settings.Role == botToUpdate.botType.ToString());
            botToUpdate.health.BodyParts.Head.min = firstBotOfDesiredType.Health.BodyParts.Head.Health.Current;
            botToUpdate.health.BodyParts.Head.max = firstBotOfDesiredType.Health.BodyParts.Head.Health.Maximum;

            botToUpdate.health.BodyParts.Chest.min = firstBotOfDesiredType.Health.BodyParts.Chest.Health.Current;
            botToUpdate.health.BodyParts.Chest.max = firstBotOfDesiredType.Health.BodyParts.Chest.Health.Maximum;

            botToUpdate.health.BodyParts.Stomach.min = firstBotOfDesiredType.Health.BodyParts.Stomach.Health.Current;
            botToUpdate.health.BodyParts.Stomach.max = firstBotOfDesiredType.Health.BodyParts.Stomach.Health.Maximum;

            botToUpdate.health.BodyParts.LeftArm.min = firstBotOfDesiredType.Health.BodyParts.LeftArm.Health.Current;
            botToUpdate.health.BodyParts.LeftArm.max = firstBotOfDesiredType.Health.BodyParts.LeftArm.Health.Maximum;

            botToUpdate.health.BodyParts.RightArm.min = firstBotOfDesiredType.Health.BodyParts.RightArm.Health.Current;
            botToUpdate.health.BodyParts.RightArm.max = firstBotOfDesiredType.Health.BodyParts.RightArm.Health.Maximum;

            botToUpdate.health.BodyParts.LeftLeg.min = firstBotOfDesiredType.Health.BodyParts.LeftLeg.Health.Current;
            botToUpdate.health.BodyParts.LeftLeg.max = firstBotOfDesiredType.Health.BodyParts.LeftLeg.Health.Maximum;

            botToUpdate.health.BodyParts.RightLeg.min = firstBotOfDesiredType.Health.BodyParts.RightLeg.Health.Current;
            botToUpdate.health.BodyParts.RightLeg.max = firstBotOfDesiredType.Health.BodyParts.RightLeg.Health.Maximum;
        }

        private void AddVisualAppearanceItems(Bot botToUpdate, Datum rawBot)
        {
            botToUpdate.appearance.head.AddUnique(rawBot.Customization.Head);
            botToUpdate.appearance.body.AddUnique(rawBot.Customization.Body);
            botToUpdate.appearance.hands.AddUnique(rawBot.Customization.Hands);
            botToUpdate.appearance.feet.AddUnique(rawBot.Customization.Feet);
        }

        private void AddName(Bot botToUpdate, string nickName)
        {
            var name = nickName.Split();
            botToUpdate.firstName.AddUnique(name[0]);
            if (name.Length > 1)
            {
                botToUpdate.lastName.AddUnique(name[1]);
            }
        }
    }
}
