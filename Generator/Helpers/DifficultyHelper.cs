using Common.Models;
using Common.Models.Output;
using Common.Models.Output.Difficulty;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Generator.Helpers
{
    public static class DifficultyHelper
    {
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
        };

        private static readonly string[] _difficulties = ["easy", "normal", "hard", "impossible"];

        public static void AddDifficultySettings(Bot botToUpdate, List<string> difficultyFilePaths)
        {
            // Read bot setting files from assets folder that match this bots type
            // Save into dictionary with difficulty as key
            Dictionary<string, DifficultySettings> difficultySettingsJsons = new();
            var pathsWithBotType = difficultyFilePaths.Where(x => x.Contains($"_{botToUpdate.botType}_BotGlobalSettings", StringComparison.InvariantCultureIgnoreCase));
            foreach (var path in pathsWithBotType)
            {
                var difficultyJson = File.ReadAllText(path);

                var serialisedDifficultySettings = JsonSerializer.Deserialize<DifficultySettings>(difficultyJson, options);

                serialisedDifficultySettings = ApplyCustomDifficultyValues(botToUpdate.botType, serialisedDifficultySettings);

                var difficultyOfFile = GetFileDifficultyFromPath(path);
                difficultySettingsJsons.Add(difficultyOfFile, serialisedDifficultySettings);
            }

            // Find each difficulty in dictionary and save into bot
            const string warnKey = "WARN_BOT_TYPES";
            const string enemyKey = "ENEMY_BOT_TYPES";
            const string friendlyKey = "FRIENDLY_BOT_TYPES";
            const string revengeKey = "REVENGE_BOT_TYPES";
            foreach (var difficulty in _difficulties)
            {
                var settings = difficultySettingsJsons.FirstOrDefault(x => x.Key.Contains(difficulty));

                // No difficulty settings found, find any settings file and use that
                // This is required for many bot types that only have 'normal' difficulty settings
                if (settings.Key == null)
                {
                    Console.WriteLine($"Difficulty: {difficulty} not found for {botToUpdate.botType}, falling back to any value found");
                    settings = difficultySettingsJsons.FirstOrDefault(x => x.Key != null);
                    if (settings.Key is null)
                    {
                        Console.WriteLine($"No difficulty values found for {botToUpdate.botType}");
                    }
                }

                if (settings.Value.Mind.ContainsKey(warnKey))
                {
                    var deserialisedArray = GetDeserializedStringArray(settings, warnKey);
                    if (deserialisedArray.Length> 0)
                    {
                        settings.Value.Mind[warnKey] = deserialisedArray;
                    }
                }

                if (settings.Value.Mind.ContainsKey(enemyKey))
                {
                    var deserialisedArray = GetDeserializedStringArray(settings, enemyKey);
                    if (deserialisedArray.Length > 0)
                    {
                        settings.Value.Mind[enemyKey] = deserialisedArray;
                    }
                }

                if (settings.Value.Mind.ContainsKey(friendlyKey))
                {
                    var deserialisedArray = GetDeserializedStringArray(settings, friendlyKey);
                    if (deserialisedArray.Length > 0)
                    {
                        settings.Value.Mind[friendlyKey] = deserialisedArray;
                    }
                }

                if (settings.Value.Mind.ContainsKey(revengeKey))
                {
                    var deserialisedArray = GetDeserializedStringArray(settings, revengeKey);
                    if (deserialisedArray.Length > 0)
                    {
                        settings.Value.Mind[revengeKey] = deserialisedArray;
                    }
                }

                SaveSettingsIntoBotFile(botToUpdate, difficulty, settings.Value);
            }
        }

        private static string[] GetDeserializedStringArray(KeyValuePair<string, DifficultySettings> settings, string friendlyKey)
        {
            object serialisedArray = settings.Value.Mind[friendlyKey];

            var json = JsonSerializer.Serialize(serialisedArray);
            return JsonSerializer.Deserialize<string[]>(json) ?? [];
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
                case BotType.bosspartisan:
                case BotType.bosszryachiy:
                    AddHostileToPMCSettings(difficultySettings);
                    break;
            }

            return difficultySettings;
        }

        private static void AddHostileToPMCSettings(DifficultySettings difficultySettings)
        {
            const string defaultEnemyUsecKey = "DEFAULT_ENEMY_USEC";
            if (difficultySettings.Mind.ContainsKey(defaultEnemyUsecKey))
            {
                difficultySettings.Mind[defaultEnemyUsecKey] = true;
            }
            else
            {
                difficultySettings.Mind.Add(defaultEnemyUsecKey, true);
            }

            const string defaultEnemyBearKey = "DEFAULT_ENEMY_BEAR";
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
