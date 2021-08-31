using Common;
using Generator.Models.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Generator
{
    internal class BotParser
    {
        private readonly string _dumpPath;

        public BotParser(string dumpPath)
        {
            _dumpPath = dumpPath;
        }

        public List<Datum> Parse()
        {
            var stopwatch = Stopwatch.StartNew();

            var failedFilesCount = 0;
            DiskHelpers.CreateDirIfDoesntExist(_dumpPath);

            var botFiles = Directory.GetFiles(_dumpPath, "*.json", SearchOption.TopDirectoryOnly).ToList();
            Console.WriteLine($"{botFiles.Count} bot dump files found");

            var parsedBots = new List<Datum>();
            Parallel.ForEach(botFiles, file => {
                var splitFile = file.Split("\\");

                var json = File.ReadAllText(file);
                try
                {
                    json = PruneMalformedBsgJson(json, splitFile.Last());

                    var bots = ParseJson(json);

                    if (bots == null || bots.Count == 0)
                    {
                        Console.WriteLine($"skipping file: {splitFile.Last()}. no bots found, ");
                        return;
                    }

                    Console.WriteLine($"parsing: {bots.Count} bots in file {splitFile.Last()}");
                    foreach (var bot in bots)
                    {
                        parsedBots.Add(bot);
                    }
                }
                catch (JsonException jex)
                {
                    failedFilesCount++;
                    Console.WriteLine($"JSON Error message: {jex.Message} || file: {splitFile.Last()}");
                }
            });

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Cleaned and Parsed: {parsedBots.Count} bots. Failed: {failedFilesCount}. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return parsedBots;
        }

        private string PruneMalformedBsgJson(string json, string fileName)
        {
            // Bsg send json where an item has a location of 1 but it should be an object with x/y/z coords
            var o = JObject.Parse(json);
            var jItemsToReplace = o.SelectTokens("$.data[*].Inventory.items[?(@.location == 1)].location");
            //var jItemsToReplace = o.SelectTokens("$.data[*].Inventory.items[?(@.location == 1 && @.slotId == 'cartridges')].location");

            if (jItemsToReplace != null && jItemsToReplace.Any())
            {
                LoggingHelpers.LogToConsole($"file {fileName} has {jItemsToReplace.Count()} json issues, cleaning up.");
                foreach (var item in jItemsToReplace)
                {
                    var obj = new { x = 1, y = 0, r = 0 };
                    item.Replace(JToken.FromObject(obj));
                }
            }

            return o.ToString();
        }

        private static List<Datum> ParseJson(string json)
        {
            var serialisedObject = JsonConvert.DeserializeObject<Root>(json);

            return serialisedObject.data;
        }
    }
}