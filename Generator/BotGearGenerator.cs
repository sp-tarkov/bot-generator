using Generator.Helpers;
using Generator.Helpers.Gear;
using Generator.Models.Input;
using Generator.Models.Output;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Generator
{
    public class BotGearGenerator
    {
        private readonly List<Bot> _baseBots;
        private readonly List<Datum> _rawParsedBots;

        public BotGearGenerator(List<Bot> baseBots, List<Datum> parsedBots)
        {
            _baseBots = baseBots;
            _rawParsedBots = parsedBots;
        }

        internal List<Bot> AddGear()
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot gear");

            foreach (var botToUpdate in _baseBots)
            {
                var rawParsedBotOfCurrentType = _rawParsedBots
                    .Where(x => x.Info.Settings.Role.Equals(botToUpdate.botType.ToString(), StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (rawParsedBotOfCurrentType.Count == 0)
                {
                    break;
                }

                GearChanceHelpers.CalculateEquipmentChances(botToUpdate, rawParsedBotOfCurrentType);
                GearChanceHelpers.AddGenerationChances(botToUpdate);
                GearChanceHelpers.CalculateModChances(botToUpdate, rawParsedBotOfCurrentType);

                foreach (var rawParsedBot in rawParsedBotOfCurrentType)
                {
                    GearHelpers.AddEquippedGear(botToUpdate, rawParsedBot);
                    GearHelpers.AddEquippedMods(botToUpdate, rawParsedBot);
                    GearHelpers.AddCartridges(botToUpdate, rawParsedBot);
                }
            }

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot gear. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return _baseBots;
        }
    }
}
