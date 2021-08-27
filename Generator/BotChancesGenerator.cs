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
    internal class BotChancesGenerator
    {
        private readonly List<Bot> _bots;
        private readonly List<Datum> _rawParsedBots;

        public BotChancesGenerator(List<Bot> botsWithGearAndLoot, List<Datum> parsedBots)
        {
            _bots = botsWithGearAndLoot;
            _rawParsedBots = parsedBots;
        }

        internal List<Bot> AddChances()
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot gear");

            foreach (var botToUpdate in _bots)
            {
                var rawParsedBotOfCurrentType = _rawParsedBots
                    .Where(x => x.Info.Settings.Role.Equals(botToUpdate.botType.ToString(), StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (rawParsedBotOfCurrentType.Count == 0)
                {
                    continue;
                }

                GearChanceHelpers.CalculateEquipmentChances(botToUpdate, rawParsedBotOfCurrentType);
                GearChanceHelpers.AddGenerationChances(botToUpdate);
                GearChanceHelpers.CalculateModChances(botToUpdate, rawParsedBotOfCurrentType);
            }

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot chances. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return _bots;
        }
    }
}