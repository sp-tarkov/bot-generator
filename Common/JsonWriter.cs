using Common.Models.Output;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using SPTarkov.Server.Core.Utils;
using SPTarkov.Server.Core.Utils.Json;

namespace Common
{
    public class JsonWriter
    {
        private readonly string _workingPath;
        private readonly string _outputFolderName;

        private readonly JsonUtil jsonUtil = new([new SptJsonConverterRegistrator()]);

        public JsonWriter(string workingPath, string outputFolderName)
        {
            _workingPath = workingPath;
            _outputFolderName = outputFolderName;
        }

        public void WriteJson(List<Bot> bots)
        {
            var outputPath = $"{_workingPath}\\{_outputFolderName}";
            DiskHelpers.CreateDirIfDoesntExist(outputPath);

            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            foreach (var bot in bots)
            {
                if (bot.appearance.body.Count == 0) // only process files that have data in them, no body = no dumps
                {
                    LoggingHelpers.LogToConsole($"Unable to process bot type: {bot.botType}, skipping", ConsoleColor.DarkRed);
                    continue;
                }
                var output = jsonUtil.Serialize(bot, true);
                Console.WriteLine($"Writing json file {bot.botType} to {outputPath}");
                File.WriteAllText($"{outputPath}\\{bot.botType.ToString().ToLower()}.json", output);
                Console.WriteLine($"file {bot.botType} written to {outputPath}");
            }
        }
    }
}
