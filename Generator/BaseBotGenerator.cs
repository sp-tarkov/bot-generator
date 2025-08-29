using Common.Extensions;
using Common.Models;
using Common.Models.Input;
using Common.Models.Output;
using Generator.Helpers;
using Generator.Helpers.Gear;

namespace Generator
{
    public static class BaseBotGenerator
    {
        public static void UpdateBaseDetails(Bot botData, Datum rawBotData)
        {
            UpdateBodyPartHealth(botData, rawBotData);
            AddExperience(botData, rawBotData);
            AddStandingForKill(botData, rawBotData);
            AddAggressorBonus(botData, rawBotData);
            AddSkills(botData, rawBotData);
            botData.experience.useSimpleAnimator = rawBotData.Info.Settings.UseSimpleAnimator;

            AddVisualAppearanceItems(botData, rawBotData);
            AddName(botData, rawBotData);
            AddVoice(botData, rawBotData);
        }

        private static void AddSkills(Bot botToUpdate, Datum rawBotData)
        {
            // Find the smallest and biggest value for each skill
            foreach (var skill in rawBotData.Skills.Common)
            {
                if (botToUpdate.skills.Common.TryGetValue(skill.Id, out var existingSkill))
                {
                    existingSkill.min = Math.Min(existingSkill.min, skill.Progress);
                    existingSkill.max = Math.Max(existingSkill.max, skill.Progress);
                }
                else
                {
                    botToUpdate.skills.Common.Add(skill.Id, new MinMax(skill.Progress, skill.Progress));
                }
            }
        }

        private static void AddStandingForKill(Bot botToUpdate, Datum rawBotData)
        {
            botToUpdate.experience.standingForKill ??= new Dictionary<string, object>();

            if (!botToUpdate.experience.standingForKill.ContainsKey(rawBotData.Info.Settings.BotDifficulty))
            {
                botToUpdate.experience.standingForKill.Add(rawBotData.Info.Settings.BotDifficulty, rawBotData.Info.Settings.StandingForKill);
            }
        }

        private static void AddAggressorBonus(Bot botToUpdate, Datum rawBotData)
        {
            botToUpdate.experience.aggressorBonus ??= new Dictionary<string, object>();

            if (!botToUpdate.experience.aggressorBonus.ContainsKey(rawBotData.Info.Settings.BotDifficulty))
            {
                botToUpdate.experience.aggressorBonus.Add(rawBotData.Info.Settings.BotDifficulty, rawBotData.Info.Settings.AggressorBonus);
            }
        }

        private static void AddExperience(Bot botToUpdate, Datum rawBotData)
        {
            botToUpdate.experience.reward ??= new();

            botToUpdate.experience.reward.TryGetValue(rawBotData.Info.Settings.BotDifficulty, out var minMaxValues);
            if (minMaxValues is null)
            {
                botToUpdate.experience.reward.Add(rawBotData.Info.Settings.BotDifficulty, new(rawBotData.Info.Settings.Experience, rawBotData.Info.Settings.Experience));

                return;
            }

            minMaxValues.min = Math.Min(minMaxValues.min, rawBotData.Info.Settings.Experience);
            minMaxValues.max = Math.Max(minMaxValues.max, rawBotData.Info.Settings.Experience);

        }

        private static void AddVoice(Bot bot, Datum rawBot)
        {
            GearHelpers.IncrementDictionaryValue(bot.appearance.voice, rawBot.Customization.Voice);
        }

        public static async Task AddDifficulties(Bot bot)
        {
            string workingPath = Directory.GetCurrentDirectory();
            string botType = bot.botType.ToString();
            var botDifficultyFiles = Directory
                .GetFiles($"{workingPath}//Assets", "*.txt", SearchOption.TopDirectoryOnly)
                .Where(x => x.Contains(botType, StringComparison.InvariantCultureIgnoreCase))
                .ToList();

            await DifficultyHelper.AddDifficultySettings(bot, botDifficultyFiles);
        }

        private static void UpdateBodyPartHealth(Bot botToUpdate, Datum rawBot)
        {
            var bodyPartHpToAdd = new Common.Models.Output.BodyParts()
            {
                Head = new MinMax(rawBot.Health.BodyParts.Head.Health.Current, rawBot.Health.BodyParts.Head.Health.Maximum),
                Chest = new MinMax(rawBot.Health.BodyParts.Chest.Health.Current, rawBot.Health.BodyParts.Chest.Health.Maximum),
                Stomach = new MinMax(rawBot.Health.BodyParts.Stomach.Health.Current, rawBot.Health.BodyParts.Stomach.Health.Maximum),
                LeftArm = new MinMax(rawBot.Health.BodyParts.LeftArm.Health.Current, rawBot.Health.BodyParts.LeftArm.Health.Maximum),
                RightArm = new MinMax(rawBot.Health.BodyParts.RightArm.Health.Current, rawBot.Health.BodyParts.RightArm.Health.Maximum),
                LeftLeg = new MinMax(rawBot.Health.BodyParts.LeftLeg.Health.Current, rawBot.Health.BodyParts.LeftLeg.Health.Maximum),
                RightLeg = new MinMax(rawBot.Health.BodyParts.RightLeg.Health.Current, rawBot.Health.BodyParts.RightLeg.Health.Maximum)
            };

            if (!botToUpdate.health.BodyParts.Contains(bodyPartHpToAdd))
            {
                botToUpdate.health.BodyParts.Add(bodyPartHpToAdd);
            }
        }

        private static void AddVisualAppearanceItems(Bot botToUpdate, Datum rawBot)
        {
            GearHelpers.IncrementDictionaryValue(botToUpdate.appearance.feet, rawBot.Customization.Feet);

            GearHelpers.IncrementDictionaryValue(botToUpdate.appearance.body, rawBot.Customization.Body);

            GearHelpers.IncrementDictionaryValue(botToUpdate.appearance.head, rawBot.Customization.Head);

            GearHelpers.IncrementDictionaryValue(botToUpdate.appearance.hands, rawBot.Customization.Hands);
        }

        private static void AddName(Bot botToUpdate, Datum rawBot)
        {
            var name = rawBot.Info.Nickname.Split();
            botToUpdate.firstName.AddUnique(name[0]);
            if (name.Length > 1)
            {
                // Add lastnames to all bots except raiders
                if (botToUpdate.botType != BotType.pmcbot)
                {
                    botToUpdate.lastName.AddUnique(name[1]);
                }
            }
        }
    }
}
