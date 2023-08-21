using Common;
using Generator.Helpers.Gear;
using Common.Models.Input;
using Common.Models.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Generator.Weighting;

namespace Generator
{
    public static class BotChancesGenerator
    {
        public static IEnumerable<Bot> AddChances(this IEnumerable<Bot> botsToUpdate, IEnumerable<Datum> rawBots)
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot gear");

            var weightHelper = new WeightingService();
            foreach (var botToUpdate in botsToUpdate)
            {
                var botType = botToUpdate.botType.ToString();
                var rawParsedBotOfCurrentType = rawBots
                    .Where(x => x.Info.Settings.Role.Equals(botType, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (rawParsedBotOfCurrentType.Count == 0)
                {
                    continue;
                }

                // TODO: Add check to make sure incoming bot list has gear
                GearChanceHelpers.CalculateEquipmentChances(botToUpdate, rawParsedBotOfCurrentType);
                GearChanceHelpers.AddGenerationChances(botToUpdate, rawBots, weightHelper);
                GearChanceHelpers.CalculateModChances(botToUpdate, rawParsedBotOfCurrentType);
                GearChanceHelpers.ApplyModChanceOverrides(botToUpdate);
                GearChanceHelpers.ApplyEquipmentChanceOverrides(botToUpdate);
            }

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot chances. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return botsToUpdate;
        }
    }
}