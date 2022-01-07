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
            var botType = botToUpdate.botType.ToString();
            var pathsWithBotType = difficultyFilePaths.Where(x => x.Contains($"_{botType}", StringComparison.InvariantCultureIgnoreCase));
            foreach (var path in pathsWithBotType)
            {
                var json = File.ReadAllText(path);
                var serialisedDifficultySettings = JsonConvert.DeserializeObject<DifficultySettings>(json);

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

                SaveSettingsIntoBotFile(botToUpdate, difficulty, settings.Value);
            }
        }

        private static DifficultySettings ApplyCustomDifficultyValues(string botType, DifficultySettings difficultySettings)
        {
            switch (botType)
            {
                // make all bosses fight PMCs
                case "bosskilla":
                case "bossgluhar":
                case "bosstagilla":
                case "bossbully":
                case "bosskojaniy":
                    AddHostileToPMCSettings(difficultySettings);
                    break;
                default:
                    break;
            }

            return difficultySettings;
        }

        private static void AddHostileToPMCSettings(DifficultySettings settings)
        {
            var defaultEnemyUsecKey = "DEFAULT_ENEMY_USEC";
            if (settings.Mind.ContainsKey(defaultEnemyUsecKey))
            {
                settings.Mind[defaultEnemyUsecKey] = true;
            }
            else
            {
                settings.Mind.Add(defaultEnemyUsecKey, true);
            }

            var defaultEnemyBearKey = "DEFAULT_ENEMY_BEAR";
            if (settings.Mind.ContainsKey(defaultEnemyUsecKey))
            {
                settings.Mind[defaultEnemyBearKey] = true;
            }
            else
            {
                settings.Mind.Add(defaultEnemyBearKey, true);
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
