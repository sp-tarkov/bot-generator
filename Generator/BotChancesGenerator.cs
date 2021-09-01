using Common;
using Generator.Helpers.Gear;
using Generator.Models.Input;
using Generator.Models.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Generator
{
    public static class BotChancesGenerator
    {
        public static IEnumerable<Bot> AddChances(this IEnumerable<Bot> botsWithGear, IEnumerable<Datum> parsedBots)
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot gear");

            foreach (var botToUpdate in botsWithGear)
            {
                var rawParsedBotOfCurrentType = parsedBots
                    .Where(x => x.Info.Settings.Role.Equals(botToUpdate.botType.ToString(), StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (rawParsedBotOfCurrentType.Count == 0)
                {
                    continue;
                }

                // TODO: Add check to make sure incoming bot list has gear
                GearChanceHelpers.CalculateEquipmentChances(botToUpdate, rawParsedBotOfCurrentType);
                GearChanceHelpers.AddGenerationChances(botToUpdate);
                GearChanceHelpers.CalculateModChances(botToUpdate, rawParsedBotOfCurrentType);
            }

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot chances. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return botsWithGear;
        }
    }
}