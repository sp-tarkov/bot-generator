using Common.Models.Input;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Bots;

public static class BotParser
{
    static JsonSerializerOptions serialiserOptions = new JsonSerializerOptions { };

    public static async Task<List<Datum>> ParseAsync(string dumpPath, string[] botTypes)
    {
        var stopwatch = Stopwatch.StartNew();

        DiskHelpers.CreateDirIfDoesntExist(dumpPath);

        var botFiles = Directory.GetFiles(dumpPath, "*.json", SearchOption.TopDirectoryOnly).ToList();
        LoggingHelpers.LogToConsole($"{botFiles.Count} bot dump files found");

        var parsedBotsDict = new Dictionary<string, Datum>(10000);
        int totalDupeCount = 0;

        ParallelOptions parallelOptions = new()
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };
        await Parallel.ForEachAsync(botFiles, parallelOptions, async(file, token) =>
        {
            var splitFilePath = file.Split("\\");

            int dupeCount = 0;
            var rawInputString = await ReadFileContentsAsync(file);

            //var json = rawInputString;
            //if (rawInputString.Contains("location\":1,"))
            //{
            //    json = PruneMalformedBsgJson(rawInputString, splitFilePath.Last());
            //}

            List<Datum> bots = null;
            try
            {
                bots = ParseJson(rawInputString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"file parse fucked up: {file}");
                throw;
            }

            
            if (bots == null || bots.Count == 0)
            {
                Console.WriteLine($"skipping file: {splitFilePath.Last()}. no bots found, ");
                return;
            }

            Console.WriteLine($"parsing: {bots.Count} bots in file {splitFilePath.Last()}");
            foreach (var bot in bots)
            {
                if (!botTypes.Contains(bot.Info.Settings.Role.ToLower()))
                {
                    continue;
                }

                if (!parsedBotsDict.ContainsKey(bot._id))
                {
                    parsedBotsDict.Add(bot._id, bot);
                }
                else
                {
                    var existingBot = parsedBotsDict[bot._id];
                    dupeCount++;
                }
            }

            totalDupeCount += dupeCount;
        });

        stopwatch.Stop();
        LoggingHelpers.LogToConsole($"Cleaned and Parsed: {parsedBotsDict.Count} bots. {totalDupeCount} dupes were ignored. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

        return (parsedBotsDict.Select(x => x.Value)).ToList();
    }

    private static async Task<string> ReadFileContentsAsync(string file)
    {
        using var reader = File.OpenText(file);
        return await reader.ReadToEndAsync();
    }

    private static string PruneMalformedBsgJson(string json, string fileName)
    {
        // Bsg send json where an item has a location of 1 but it should be an object with x/y/z coords
        var o = JObject.Parse(json);
        var jItemsToReplace = o.SelectTokens("$.data[*].Inventory.items[?(@.location == 1)].location");
        //var jItemsToReplace = o.SelectTokens("$.data[*].Inventory.items[?(@.location == 1 && @.slotId == 'cartridges')].location");

        if (jItemsToReplace != null && jItemsToReplace.Any())
        {
            LoggingHelpers.LogToConsole($"file {fileName} has {jItemsToReplace.Count()} json issues, cleaning up.", ConsoleColor.Yellow);
            foreach (var item in jItemsToReplace)
            {
                var obj = new { x = 1, y = 0, r = 0 };
                item.Replace(JToken.FromObject(obj));
            }
        }
        var returnString = o.ToString();

        o = null;
        jItemsToReplace = null;

        return returnString;
    }

    private static List<Datum> ParseJson(string json)
    {
        var deSerialisedObject = JsonSerializer.Deserialize<Root>(json, serialiserOptions);
        return deSerialisedObject.data;
    }
}