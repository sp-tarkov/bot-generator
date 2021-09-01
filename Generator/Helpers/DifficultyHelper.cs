using Generator.Models.Output;
using Generator.Models.Output.Difficulty;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Generator.Helpers
{
    public static class DifficultyHelper
    {
        private static readonly string[] _difficulties = new[] { "easy", "normal", "hard", "impossible" };

        public static void AddDifficultySettings(Bot bot, List<string> difficultyFilePaths)
        {
            // Read bot setting files from assets folder that match this bots type
            // Save into dictionary with difficulty as key
            var difficultySettingsJsons = new Dictionary<string, DifficultySettings>();
            foreach (var path in difficultyFilePaths.Where(x=>x.Contains($"_{bot.botType}", System.StringComparison.InvariantCultureIgnoreCase)))
            {
                var json = File.ReadAllText(path);
                var serialisedObject = JsonConvert.DeserializeObject<DifficultySettings>(json);

                var difficultyOfFile = GetFileDifficultyFromPath(path);
                difficultySettingsJsons.Add(difficultyOfFile, serialisedObject);
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

                SaveSettingsIntoBotFile(bot, difficulty, settings.Value);
            }
        }

        private static string GetFileDifficultyFromPath(string path)
        {
            // Split path into parts and find the last part (filename)
            // Split filename and take the first part (difficulty, easy/normal etc)
            var splitPath = path.Split("\\");
            return splitPath.Last().Split("_")[0];
        }

        private static void SaveSettingsIntoBotFile(Bot bot, string difficulty, DifficultySettings settings)
        {
            switch (difficulty)
            {
                case "easy":
                    bot.difficulty.easy = settings;
                    break;
                case "normal":
                    bot.difficulty.normal = settings;
                    break;
                case "hard":
                    bot.difficulty.hard = settings;
                    break;
                case "impossible":
                    bot.difficulty.impossible = settings;
                    break;
            }
        }
    }
}
