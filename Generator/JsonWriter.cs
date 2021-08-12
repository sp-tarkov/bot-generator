using Generator.Models.Output;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Generator
{
    public class JsonWriter
    {
        private readonly string _workingPath;
        private readonly string _outputFolderName;

        public JsonWriter(string workingPath, string outputFolderName)
        {
            _workingPath = workingPath;
            _outputFolderName = outputFolderName;
        }

        public void WriteJson(List<Bot> bots)
        {
            var outputPath = $"{_workingPath}\\{_outputFolderName}";
            CreateDirIfDoesntExist(outputPath);

            foreach (var bot in bots)
            {
                var output = JsonConvert.SerializeObject(bot, Formatting.Indented);
                Console.WriteLine($"Writing json file {bot.botType} to {outputPath}");
                File.WriteAllText($"{outputPath}\\{bot.botType}.json", output);
                Console.WriteLine($"file {bot.botType} written to {outputPath}");
            }
            
        }

        private void CreateDirIfDoesntExist(string path)
        {
            if (!Directory.Exists($"{path}"))
            {
                //create dump dir
                Directory.CreateDirectory($"{path}");
            }
        }
    }
}
