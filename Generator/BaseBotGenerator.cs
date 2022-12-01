using Common;
using Common.Extensions;
using Common.Models;
using Common.Models.Input;
using Common.Models.Output;
using Generator.Helpers;
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
        public static IEnumerable<Bot> GenerateBaseDetails(IEnumerable<Datum> rawBots, string workingPath, IEnumerable<string> botTypes)
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot base");

            // Create a list of bot objects ready to be hydrated
            var baseBots = new List<Bot>();
            foreach (var botType in botTypes)
            {
                var typeToAdd = (BotType)Enum.Parse(typeof(BotType), botType);
                baseBots.Add(new Bot(typeToAdd));
            }

            // Iterate over each bot type wejust made and put some data into them
            foreach (var botToUpdate in baseBots)
            {
                var rawBotType = botToUpdate.botType.ToString();
                var rawBotsOfSameType = rawBots.Where(x => string.Equals(x.Info.Settings.Role, rawBotType, StringComparison.OrdinalIgnoreCase))
                                                .ToList();
                var rawBotsOfSameTypeCount = rawBotsOfSameType.Count.ToString();
                

                if (rawBotsOfSameType.Count == 0)
                {
                    LoggingHelpers.LogToConsole($"no bots of type {rawBotType}, skipping", ConsoleColor.DarkRed);
                    continue;
                }

                LoggingHelpers.LogToConsole($"Found {rawBotsOfSameTypeCount} bots of type: {rawBotType}");

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

            return baseBots;
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

        private static void AddVoice(Bot bot, Datum rawBot)
        {
            bot.appearance.voice.AddUnique(rawBot.Info.Voice);
        }

        private static void AddDifficulties(Bot bot, string workingPath)
        {
            string botType = bot.botType.ToString();
            var botDifficultyFiles = Directory
                .GetFiles($"{workingPath}//Assets", "*.txt", SearchOption.TopDirectoryOnly)
                .Where(x => x.Contains(botType, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            DifficultyHelper.AddDifficultySettings(bot, botDifficultyFiles);
        }

        private static void UpdateBodyPartHealth(Bot botToUpdate, List<Datum> rawBots)
        {
            var uniqueHealthSetups = new Dictionary<int, Common.Models.Output.BodyParts>();
            foreach (var bot in rawBots)
            {
                var healthTotal = bot.Health.BodyParts.GetHpMaxTotal();
                var alreadyExists = uniqueHealthSetups.ContainsKey(healthTotal);

                if (!alreadyExists)
                {
                    var bodyPartHpToAdd = new Common.Models.Output.BodyParts();
                    bodyPartHpToAdd.Head.min = bot.Health.BodyParts.Head.Health.Current;
                    bodyPartHpToAdd.Head.max = bot.Health.BodyParts.Head.Health.Maximum;

                    bodyPartHpToAdd.Chest.min = bot.Health.BodyParts.Chest.Health.Current;
                    bodyPartHpToAdd.Chest.max = bot.Health.BodyParts.Chest.Health.Maximum;

                    bodyPartHpToAdd.Stomach.min = bot.Health.BodyParts.Stomach.Health.Current;
                    bodyPartHpToAdd.Stomach.max = bot.Health.BodyParts.Stomach.Health.Maximum;

                    bodyPartHpToAdd.LeftArm.min = bot.Health.BodyParts.LeftArm.Health.Current;
                    bodyPartHpToAdd.LeftArm.max = bot.Health.BodyParts.LeftArm.Health.Maximum;

                    bodyPartHpToAdd.RightArm.min = bot.Health.BodyParts.RightArm.Health.Current;
                    bodyPartHpToAdd.RightArm.max = bot.Health.BodyParts.RightArm.Health.Maximum;

                    bodyPartHpToAdd.LeftLeg.min = bot.Health.BodyParts.LeftLeg.Health.Current;
                    bodyPartHpToAdd.LeftLeg.max = bot.Health.BodyParts.LeftLeg.Health.Maximum;

                    bodyPartHpToAdd.RightLeg.min = bot.Health.BodyParts.RightLeg.Health.Current;
                    bodyPartHpToAdd.RightLeg.max = bot.Health.BodyParts.RightLeg.Health.Maximum;

                    uniqueHealthSetups.Add(healthTotal, bodyPartHpToAdd);
                }
            }

            botToUpdate.health.BodyParts = uniqueHealthSetups.Values.ToList();
        }

        private static void AddVisualAppearanceItems(Bot botToUpdate, Datum rawBot)
        {
            botToUpdate.appearance.head.AddUnique(rawBot.Customization.Head);
            botToUpdate.appearance.body.AddUnique(rawBot.Customization.Body, 1);
            botToUpdate.appearance.hands.AddUnique(rawBot.Customization.Hands);
            botToUpdate.appearance.feet.AddUnique(rawBot.Customization.Feet, 1);
        }

        private static void AddName(Bot botToUpdate, Datum rawBot)
        {
            var name = rawBot.Info.Nickname.Split();
            botToUpdate.firstName.AddUnique(name[0]);
            if (name.Length > 1)
            {
                // Add lastnames to all bots except raiders
                if (botToUpdate.botType != BotType.pmcBot)
                {
                    botToUpdate.lastName.AddUnique(name[1]);
                }
                
            }
        }
    }
}
