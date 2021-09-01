using Common;
using Common.Extensions;
using Common.Models;
using Generator.Helpers;
using Generator.Models.Input;
using Generator.Models.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Generator
{
    public static class BaseBotGenerator
    {
        //TODO: pass in bot types and use those to create the classes in rawBots list
        public static IEnumerable<Bot> GenerateBaseDetails(IEnumerable<Datum> parsedBots, string workingPath, IEnumerable<string> botTypes)
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot base");

            // Create a list of bot objects ready to be hydrated
            var rawBots = new List<Bot>();
            foreach (var botType in botTypes)
            {
                var typeToAdd = (BotType)Enum.Parse(typeof(BotType), botType);
                rawBots.Add(new Bot(typeToAdd));
            }

            // Iterate over each bot type wejust made and put some data into them
            foreach (var botToUpdate in rawBots)
            {
                var rawBotsOfSameType = parsedBots
                    .Where(x => string.Equals(x.Info.Settings.Role, botToUpdate.botType.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();

                if (rawBotsOfSameType.Count == 0)
                {
                    LoggingHelpers.LogToConsole($"no bots of type {botToUpdate.botType}", ConsoleColor.DarkRed);
                    continue;
                }

                LoggingHelpers.LogToConsole($"Found {rawBotsOfSameType.Count} bots of type: {botToUpdate.botType}");

                UpdateBodyPartHealth(botToUpdate, rawBotsOfSameType);
                AddDifficulties(botToUpdate, workingPath);
                AddExperience(botToUpdate, rawBotsOfSameType);
                AddStandingForKill(botToUpdate, rawBotsOfSameType);
                AddSkills(botToUpdate, rawBotsOfSameType);

                foreach (var rawParsedBot in rawBotsOfSameType)
                {
                    AddVisualAppearanceItems(botToUpdate, rawParsedBot);
                    AddName(botToUpdate, rawParsedBot);
                    AddVoice(botToUpdate, rawParsedBot);
                }
            }

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot base. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return rawBots;
        }

        private static void AddSkills(Bot botToUpdate, IEnumerable<Datum> rawBotsOfSameType)
        {
            var firstBotOfDesiredType = rawBotsOfSameType.FirstOrDefault();

            foreach (var skill in firstBotOfDesiredType.Skills.Common)
            {
                botToUpdate.skills.Common.Add(skill.Id, new MinMax(skill.Progress, skill.Progress));
            }
        }

        private static void AddStandingForKill(Bot botToUpdate, IEnumerable<Datum> rawBotsOfSameType)
        {
            var firstBotOfDesiredType = rawBotsOfSameType.FirstOrDefault();

            botToUpdate.experience.standingForKill = firstBotOfDesiredType.Info.Settings.StandingForKill;
            botToUpdate.experience.aggressorBonus = firstBotOfDesiredType.Info.Settings.AggressorBonus;
        }

        private static void AddExperience(Bot botToUpdate, IEnumerable<Datum> rawBotsOfSameType)
        {
            var firstBotOfDesiredType = rawBotsOfSameType.FirstOrDefault();

            botToUpdate.experience.reward.min = firstBotOfDesiredType.Info.Settings.Experience;
            botToUpdate.experience.reward.max = firstBotOfDesiredType.Info.Settings.Experience;
        }

        private static void AddVoice(Bot bot, Datum rawParsedBot)
        {
            bot.appearance.voice.AddUnique(rawParsedBot.Info.Voice);
        }

        private static void AddDifficulties(Bot bot, string workingPath)
        {
            var botFiles = Directory
                .GetFiles($"{workingPath}//Assets", "*.txt", SearchOption.TopDirectoryOnly)
                .Where(x => x.Contains(bot.botType.ToString(), StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            DifficultyHelper.AddDifficultySettings(bot, botFiles);
        }

        private static void UpdateBodyPartHealth(Bot botToUpdate, List<Datum> rawParsedBots)
        {
            var firstBotOfDesiredType = rawParsedBots.FirstOrDefault();
            if (firstBotOfDesiredType == null)
            {
                LoggingHelpers.LogToConsole($"bot type of: {botToUpdate.botType} not found, unable to update body part health.");
                return;
            }

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

        private static void AddVisualAppearanceItems(Bot botToUpdate, Datum rawBot)
        {
            botToUpdate.appearance.head.AddUnique(rawBot.Customization.Head);
            botToUpdate.appearance.body.AddUnique(rawBot.Customization.Body);
            botToUpdate.appearance.hands.AddUnique(rawBot.Customization.Hands);
            botToUpdate.appearance.feet.AddUnique(rawBot.Customization.Feet);
        }

        private static void AddName(Bot botToUpdate, Datum rawBot)
        {
            var name = rawBot.Info.Nickname.Split();
            botToUpdate.firstName.AddUnique(name[0]);
            if (name.Length > 1)
            {
                botToUpdate.lastName.AddUnique(name[1]);
            }
        }
    }
}
