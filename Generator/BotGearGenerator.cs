using Common;
using Common.Models.Input;
using Common.Models.Output;
using Generator.Helpers.Gear;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Generator
{
    public static class BotGearGenerator
    {
        public static IEnumerable<Bot> AddGear(this IEnumerable<Bot> baseBots, IEnumerable<Datum> rawBots)
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot gear");

            foreach (var botToUpdate in baseBots)
            {
                var botType = botToUpdate.botType.ToString();
                var rawParsedBotOfCurrentType = rawBots.Where(x => x.Info.Settings.Role.Equals(botType, StringComparison.OrdinalIgnoreCase))
                                                        .ToList();

                if (rawParsedBotOfCurrentType.Count == 0)
                {
                    continue;
                }

                foreach (var rawParsedBot in rawParsedBotOfCurrentType)
                {
                    GearHelpers.AddEquippedGear(botToUpdate, rawParsedBot);
                    GearHelpers.AddEquippedMods(botToUpdate, rawParsedBot);
                    GearHelpers.AddCartridges(botToUpdate, rawParsedBot);
                }
            }

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot gear. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return baseBots;
        }
    }
}
