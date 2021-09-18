using Common.Models.Output;
using Common.Models.Output.Difficulty;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            foreach (var path in difficultyFilePaths.Where(x=>x.Contains($"_{botToUpdate.botType}", System.StringComparison.InvariantCultureIgnoreCase)))
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

                SaveSettingsIntoBotFile(botToUpdate, difficulty, settings.Value);
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
