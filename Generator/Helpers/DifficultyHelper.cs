using Common.Models;
using Common.Models.Input;
using Common.Models.Output;
using Common.Models.Output.Difficulty;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Generator.Helpers
{
    public static class DifficultyHelper
    {
        private static readonly JsonSerializerOptions options = new()
        {
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
            Converters = { new JsonStringEnumConverter() },
            AllowTrailingCommas = true,
        };

        private static readonly string[] _difficulties = ["easy", "normal", "hard", "impossible"];

        public static async Task AddDifficultySettings(Bot botToUpdate, List<string> difficultyFilePaths)
        {
            // Read bot setting files from assets folder that match this bots type
            // Save into dictionary with difficulty as key
            Dictionary<string, Common.Models.Output.Difficulty.DifficultyCategories> difficultySettingsJsons = new();
            var pathsWithBotType = difficultyFilePaths.Where(x => x.Contains($"_{botToUpdate.botType}_BotGlobalSettings", StringComparison.InvariantCultureIgnoreCase));
            foreach (var path in pathsWithBotType)
            {
                await using FileStream fs = new(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);

                var serialisedDifficultySettings = await JsonSerializer.DeserializeAsync<Common.Models.Output.Difficulty.DifficultyCategories>(fs, options);

                var difficultyOfFile = GetFileDifficultyFromPath(path);
                difficultySettingsJsons.Add(difficultyOfFile, serialisedDifficultySettings);
            }

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

        private static void SaveSettingsIntoBotFile(Bot botToUpdate, string difficulty, Common.Models.Output.Difficulty.DifficultyCategories categories)
        {
            switch (difficulty)
            {
                case "easy":
                    botToUpdate.difficulty.easy = categories;
                    break;
                case "normal":
                    botToUpdate.difficulty.normal = categories;
                    break;
                case "hard":
                    botToUpdate.difficulty.hard = categories;
                    break;
                case "impossible":
                    botToUpdate.difficulty.impossible = categories;
                    break;
            }
        }
    }
}
