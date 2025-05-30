﻿using Common;
using Common.Bots;
using Common.Models.Input;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UniqueTemplates;

public class Program
{
    static async Task Main(string[] args)
    {
        // Get the unique bot types that bsg generate from live dumps

        // Read raw bot dumps and turn into c# objects
        var workingPath = Directory.GetCurrentDirectory();
        var dumpPath = $"{workingPath}//dumps";
        var parsedBots = await BotParser.ParseAsync(dumpPath);

        if (parsedBots.Count == 0)
        {
            LoggingHelpers.LogToConsole("no bots found, unable to continue");
            LoggingHelpers.LogToConsole("Check your dumps are in 'Generator\\bin\\Debug\\netcoreapp3.1\\dumps' and start with 'resp.' NOT 'req.'");
            return;
        }

        var dictDupeCount = 0;
        var uniqueBotTemplates = new Dictionary<string, Datum>();
        foreach (var bot in parsedBots)
        {
            if (uniqueBotTemplates.ContainsKey(bot._id))
            {
                dictDupeCount++;
                continue;
            }

            uniqueBotTemplates.Add(bot._id, bot);
        }

        //LoggingHelpers.LogToConsole($"LIST       templates found: {botTemplates.Count}, {dupeCount} bots rejected as duplicate");
        LoggingHelpers.LogToConsole($"DICTIONARY templates found: {uniqueBotTemplates.Count}, {dictDupeCount} bots rejected as duplicate");

        var groupedBots = uniqueBotTemplates.GroupBy(x => x.Value.Info.Settings.Role).ToList();

        foreach (var group in groupedBots)
        {
            var botList = new List<Datum>();
            foreach (var bot in group)
            {
                botList.Add(bot.Value);
            }

            LoggingHelpers.LogToConsole($"{botList.Count} unique {group.Key} bots found");

            var jsonWriter = new JsonWriter(workingPath, "output");
            jsonWriter.WriteJson(botList, group.Key);
        }
    }
}
