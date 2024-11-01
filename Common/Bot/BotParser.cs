using System.Collections.Concurrent;
using Common.Models.Input;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Common.Models.Output;

namespace Common.Bots;

public static class BotParser
{
    private static readonly JsonSerializerOptions serialiserOptions = new() { };

    public static async Task<List<Datum>> ParseAsync(string dumpPath, HashSet<string> botTypes)
    {
        var stopwatch = Stopwatch.StartNew();

        DiskHelpers.CreateDirIfDoesntExist(dumpPath);

        var botFiles = Directory.GetFiles(dumpPath, "*.json", SearchOption.TopDirectoryOnly);
        LoggingHelpers.LogToConsole($"{botFiles.Length} bot dump files found");

        // key = bot type
        // Store bots keyed against their ID so we never get duplicates
        var parsedBotsDict = new ConcurrentDictionary<string, Datum>();
        
        int totalDupeCount = 0;
        var tasks = new List<Task>();
        foreach (var filePath in botFiles)
        {
            tasks.Add(ProcessBotFile(botTypes, filePath, parsedBotsDict, totalDupeCount));
        }

        await Task.WhenAll(tasks.ToArray());
        stopwatch.Stop();
        
        LoggingHelpers.LogToConsole($"Cleaned and Parsed: {parsedBotsDict.Count} bots. {totalDupeCount} dupes were ignored. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

        return [.. parsedBotsDict.Values];
    }

    private static async Task<int> ProcessBotFile(
        HashSet<string> botTypes,
        string filePath,
        ConcurrentDictionary<string, Datum> parsedBotsDict,
        int totalDupeCount)
    {
        var splitFilePath = filePath.Split("\\");

        int dupeCount = 0;

        List<Datum> bots = [];
        try
        {
            // Parse the bots inside the json file
            using (var reader = new StreamReader(filePath))
            {
                var deSerialisedObject = JsonSerializer.Deserialize<Root>(reader.ReadToEnd(), serialiserOptions);
                bots.AddRange(deSerialisedObject.data.Where(botData => botTypes.Contains(botData.Info.Settings.Role.ToLower())));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"File parse fucked up: {filePath}");
            throw;
        }

        if (bots == null || bots.Count == 0)
        {
            Console.WriteLine($"Skipping file: {splitFilePath.Last()}. no bots found, ");
            return totalDupeCount;
        }

        //Console.WriteLine($"parsing: {bots.Count} bots in file {splitFilePath.Last()}");
        foreach (var bot in bots)
        {
            // Bot fucks up something, never allow it in
            if (bot._id == "6483938c53cc9087c70eae86")
            {
                Console.WriteLine("oh no");
                continue;
            }

            // null out unnecessary data to save ram
            bot.Stats = null;
            bot.Encyclopedia = null;
            bot.Hideout = null;
            bot.TaskConditionCounters = null;
            bot.Bonuses = null;
            bot.InsuredItems = null;

            // Add bot if not already added
            if (!parsedBotsDict.TryAdd(bot._id, bot))
            {
                dupeCount++;
            }
        }

        totalDupeCount += dupeCount;
        //Console.WriteLine($"Parsed file: {filePath}");
        return totalDupeCount;
    }

    private static IEnumerable<Datum> ParseJson(string json)
    {
        var deSerialisedObject = JsonSerializer.Deserialize<Root>(json, serialiserOptions);
        return deSerialisedObject.data;
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
}