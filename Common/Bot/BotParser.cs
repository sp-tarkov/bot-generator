using System.Collections.Concurrent;
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
    static readonly JsonSerializerOptions serialiserOptions = new() { };

    public static List<Datum> ParseAsync(string dumpPath, HashSet<string> botTypes)
    {
        var stopwatch = Stopwatch.StartNew();

        DiskHelpers.CreateDirIfDoesntExist(dumpPath);

        var botFiles = Directory.GetFiles(dumpPath, "*.json", SearchOption.TopDirectoryOnly).ToList();
        LoggingHelpers.LogToConsole($"{botFiles.Count} bot dump files found");

        var parsedBotsDict = new HashSet<Datum>();
        var dictionaryLock = new object();

        int totalDupeCount = 0;

        var tasks = new List<Task>(50);
        foreach (var file in botFiles)
        {
            tasks.Add(Task.Factory.StartNew(() =>
            {
                var splitFilePath = file.Split("\\");

                int dupeCount = 0;
                var rawInputString = File.ReadAllText(file);

                List<Datum> bots = null;
                try
                {
                    bots = ParseJson(rawInputString).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"File parse fucked up: {file}");
                    throw;
                }

                if (bots == null || bots.Count == 0)
                {
                    Console.WriteLine($"Skipping file: {splitFilePath.Last()}. no bots found, ");
                    return;
                }

                //Console.WriteLine($"parsing: {bots.Count} bots in file {splitFilePath.Last()}");
                foreach (var bot in bots)
                {
                    // I have no idea
                    if (bot._id == "6483938c53cc9087c70eae86")
                    {
                        Console.WriteLine("oh no");
                    }

                    // We dont know how to parse this bot type, need to add it to types enum
                    if (!botTypes.Contains(bot.Info.Settings.Role.ToLower()))
                    {
                        continue;
                    }

                    lock (dictionaryLock)
                    {
                        // Bot already exists in dictionary, skip
                        if (parsedBotsDict.Contains(bot))
                        {
                            //var existingBot = parsedBotsDict[bot._id];
                            dupeCount++;
                            continue;
                        }


                        if (!parsedBotsDict.Contains(bot))
                        {
                            // Null out data we don't need for generating bots to save RAM
                            bot.Stats = null;
                            bot.Encyclopedia = null;
                            bot.Hideout = null;
                            bot.ConditionCounters = null;
                            bot.Bonuses = null;
                            bot.BackendCounters = null;
                            bot.InsuredItems = null;
                            parsedBotsDict.Add(bot);
                        }
                    }
                }

                totalDupeCount += dupeCount;
            }));
        }

        Task.WaitAll(tasks.ToArray());
        stopwatch.Stop();

        LoggingHelpers.LogToConsole($"Cleaned and Parsed: {parsedBotsDict.Count} bots. {totalDupeCount} dupes were ignored. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

        return parsedBotsDict.ToList();
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

    private static IEnumerable<Datum> ParseJson(string json)
    {
        var deSerialisedObject = JsonSerializer.Deserialize<Root>(json, serialiserOptions);
        return deSerialisedObject.data;
    }
}