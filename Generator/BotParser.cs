﻿using System.Collections.Concurrent;
using Common.Models.Input;
using System.Diagnostics;
using System.Text.Json;
using Common.Models.Output;
using Common.Models;
using Generator;
using Generator.Helpers.Gear;

namespace Common.Bots;

public static class BotParser
{
    public static async Task<List<Bot>> Parse(string dumpPath, HashSet<string> botTypes)
    {
        var stopwatch = Stopwatch.StartNew();

        // Build the list of base bot data
        var baseBots = new HashSet<Bot>();
        foreach (var botType in botTypes)
        {
            var typeToAdd = (BotType)Enum.Parse(typeof(BotType), botType);
            baseBots.Add(new Bot(typeToAdd));
        }

        DiskHelpers.CreateDirIfDoesntExist(dumpPath);
        var botFiles = Directory.GetFiles(dumpPath, "*.json", SearchOption.TopDirectoryOnly);
        LoggingHelpers.LogToConsole($"{botFiles.Length} bot dump files found");

        var parsedBotIds = new ConcurrentDictionary<string, bool>();
        var totalDupeCount = 0;

        // Lock thread amount to the amount of semaphore locks we have
        var semaphore = new SemaphoreSlim(8);
        var tasks = botFiles.Select(async (filePath, index) =>
        {
            await semaphore.WaitAsync();
            try
            {
                if ((index + 1) % 500 == 0)
                    Console.WriteLine($"Processing file {index + 1}");

                return await ProcessBotFileAsync(baseBots, filePath, parsedBotIds);
            }
            finally
            {
                semaphore.Release();
            }
        });

        var dupeCounts = await Task.WhenAll(tasks);
        totalDupeCount = dupeCounts.Sum();

        // Handle things we can only do once all data has been processed
        foreach (var bot in baseBots)
        {
            if (bot.botCount == 0) continue;

            await BaseBotGenerator.AddDifficulties(bot);
            GearChanceHelpers.CalculateModChances(bot);
            GearChanceHelpers.CalculateEquipmentModChances(bot);
            GearChanceHelpers.CalculateEquipmentChances(bot);
            GearChanceHelpers.ApplyModChanceOverrides(bot);
            GearChanceHelpers.ApplyEquipmentChanceOverrides(bot);
            GearHelpers.ReduceAmmoWeightValues(bot);
            GearHelpers.ReduceEquipmentWeightValues(bot.inventory.equipment);
            GearHelpers.ReduceWeightValues(bot.appearance.voice);
            GearHelpers.ReduceWeightValues(bot.appearance.feet);
            GearHelpers.ReduceWeightValues(bot.appearance.body);
            GearHelpers.ReduceWeightValues(bot.appearance.head);
            GearHelpers.ReduceWeightValues(bot.appearance.hands);
        }

        stopwatch.Stop();
        LoggingHelpers.LogToConsole($"{totalDupeCount} dupes were ignored. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

        return baseBots.ToList();
    }

    private static async Task<int> ProcessBotFileAsync(
    HashSet<Bot> baseBots,
    string filePath,
    ConcurrentDictionary<string, bool> parsedBotIds)
    {
        Root deSerialisedObject;

        try
        {
            await using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 65536, useAsync: true);
            deSerialisedObject = await JsonSerializer.DeserializeAsync<Root>(fs);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to parse file from path: {filePath}, skipping. {e.Message}");
            return 0;
        }

        if (deSerialisedObject?.data is null)
        {
            Console.WriteLine($"Failed to process file: {filePath} as its data object is null");
            return 0;
        }

        var dupeCount = 0;
        var botDataList = deSerialisedObject.data.ToList();

        var botDataByType = new Dictionary<BotType, List<Datum>>();

        foreach (var botData in botDataList)
        {
            try
            {
                if (!parsedBotIds.TryAdd(botData._id, true))
                {
                    dupeCount++;
                    continue;
                }

                var role = botData.Info.Settings.Role;
                if (!Enum.TryParse<BotType>(role, true, out var botType))
                {
                    Console.WriteLine($"Skipping: {botData._id} due to unknown role: {role}");
                    continue;
                }

                if (!botDataByType.TryGetValue(botType, out var list))
                {
                    list = new List<Datum>();
                    botDataByType[botType] = list;
                }
                list.Add(botData);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Bot in file: {filePath} was invalid, skipping - {e.Message}");
            }
        }

        foreach (var kvp in botDataByType)
        {
            var botType = kvp.Key;
            var botDataItems = kvp.Value;

            var baseBot = baseBots.FirstOrDefault(bot => bot.botType == botType);
            if (baseBot == null)
            {
                Console.WriteLine($"Skipping bot type: {botType} - not found in base bots");
                continue;
            }

            lock (baseBot)
            {
                baseBot.botCount += botDataItems.Count;

                foreach (var botData in botDataItems)
                {
                    BaseBotGenerator.UpdateBaseDetails(baseBot, botData);
                    BotGearGenerator.AddGear(baseBot, botData);
                    BotLootGenerator.AddLoot(baseBot, botData);
                    BotChancesGenerator.AddChances(baseBot, botData);
                }
            }
        }

        return dupeCount;
    }
}