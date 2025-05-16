using Common.Models;
using Common.Models.Output;
using Common.Models.Output.Difficulty;
using Newtonsoft.Json;

namespace Generator.Helpers
{
    public static class DifficultyHelper
    {
        private static readonly string[] _difficulties = new[] { "easy", "normal", "hard", "impossible" };

        public static void AddDifficultySettings(Bot botToUpdate, List<string> difficultyFilePaths)
        {
            // Read bot setting files from assets folder that match this bots type
            // Save into dictionary with difficulty as key
            var difficultySettingsJsons = new Dictionary<string, DifficultySettings>();
            BotType botType = botToUpdate.botType;
            var pathsWithBotType = difficultyFilePaths.Where(x => x.Contains($"_{botType}_BotGlobal", StringComparison.InvariantCultureIgnoreCase));
            foreach (var path in pathsWithBotType)
            {
                var difficultyJson = File.ReadAllText(path);
                var serialisedDifficultySettings = JsonConvert.DeserializeObject<DifficultySettings>(difficultyJson, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore
                });

                serialisedDifficultySettings = ApplyCustomDifficultyValues(botType, serialisedDifficultySettings);

                var difficultyOfFile = GetFileDifficultyFromPath(path);
                difficultySettingsJsons.Add(difficultyOfFile, serialisedDifficultySettings);
            }

            // Find each difficulty in dictionary and save into bot
            foreach (var difficulty in _difficulties)
            {
                var settings = difficultySettingsJsons.FirstOrDefault(x => x.Key.Contains(difficulty));

                // No difficulty settings found, find any settings file and use that
                // This is required for many bot types that only have 'normal' difficulty settings
                if (settings.Key == null)
                {
                    settings = difficultySettingsJsons.FirstOrDefault(x => x.Key != null);
                }

                var warnKey = "WARN_BOT_TYPES";
                if (settings.Value.Mind.ContainsKey(warnKey))
                {
                    var deserialisedArray = getDeserializedStringArray(settings, warnKey);
                    if (deserialisedArray.Length> 0)
                    {
                        settings.Value.Mind[warnKey] = deserialisedArray;
                    }
                }

                var enemyKey = "ENEMY_BOT_TYPES";
                if (settings.Value.Mind.ContainsKey(enemyKey))
                {
                    var deserialisedArray = getDeserializedStringArray(settings, enemyKey);
                    if (deserialisedArray.Length > 0)
                    {
                        settings.Value.Mind[enemyKey] = deserialisedArray;
                    }
                }

                var friendlyKey = "FRIENDLY_BOT_TYPES";
                if (settings.Value.Mind.ContainsKey(friendlyKey))
                {
                    var deserialisedArray = getDeserializedStringArray(settings, friendlyKey);
                    if (deserialisedArray.Length > 0)
                    {
                        settings.Value.Mind[friendlyKey] = deserialisedArray;
                    }
                }

                var revengeKey = "REVENGE_BOT_TYPES";
                if (settings.Value.Mind.ContainsKey(revengeKey))
                {
                    var deserialisedArray = getDeserializedStringArray(settings, revengeKey);
                    if (deserialisedArray.Length > 0)
                    {
                        settings.Value.Mind[revengeKey] = deserialisedArray;
                    }
                }

                SaveSettingsIntoBotFile(botToUpdate, difficulty, settings.Value);
            }
        }

        private static string[] getDeserializedStringArray(KeyValuePair<string, DifficultySettings> settings, string friendlyKey)
        {
            var serialisedArray = JsonConvert.SerializeObject(settings.Value.Mind[friendlyKey]);
            return JsonConvert.DeserializeObject<string[]>(serialisedArray);
        }


        private static DifficultySettings ApplyCustomDifficultyValues(BotType botType, DifficultySettings difficultySettings)
        {
            switch (botType)
            {
                // make all bosses fight PMCs
                case BotType.bosskilla:
                case BotType.bossgluhar:
                case BotType.bosstagilla:
                case BotType.bossbully:
                case BotType.bosskojaniy:
                case BotType.bossboar:
                case BotType.bossboarsniper:
                case BotType.bossknight:
                case BotType.bosszryachiy:
                    AddHostileToPMCSettings(difficultySettings);
                    break;
                default:
                    break;
            }

            return difficultySettings;
        }

        private static void AddHostileToPMCSettings(DifficultySettings difficultySettings)
        {
            var defaultEnemyUsecKey = "DEFAULT_ENEMY_USEC";
            if (difficultySettings.Mind.ContainsKey(defaultEnemyUsecKey))
            {
                difficultySettings.Mind[defaultEnemyUsecKey] = true;
            }
            else
            {
                difficultySettings.Mind.Add(defaultEnemyUsecKey, true);
            }

            var defaultEnemyBearKey = "DEFAULT_ENEMY_BEAR";
            if (difficultySettings.Mind.ContainsKey(defaultEnemyUsecKey))
            {
                difficultySettings.Mind[defaultEnemyBearKey] = true;
            }
            else
            {
                difficultySettings.Mind.Add(defaultEnemyBearKey, true);
            }
        }

        private static string GetFileDifficultyFromPath(string path)
        {
            // Split path into parts and find the last part (filename)
            // Split filename and take the first part (difficulty, easy/normal etc)
            var splitPath = path.Split("\\");
            return splitPath.Last().Split("_")[0];
        }

        private static void SaveSettingsIntoBotFile(Bot botToUpdate, string difficulty, DifficultySettings settings)
        {
            switch (difficulty)
            {
                case "easy":
                    botToUpdate.difficulty.easy = settings;
                    break;
                case "normal":
                    botToUpdate.difficulty.normal = settings;
                    break;
                case "hard":
                    botToUpdate.difficulty.hard = settings;
                    break;
                case "impossible":
                    botToUpdate.difficulty.impossible = settings;
                    break;
            }
        }
    }
}
