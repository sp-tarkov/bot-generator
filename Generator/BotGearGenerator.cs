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

            foreach (var bot in _baseBots)
            {
                GearChanceHelpers.AddEquipmentChances(bot);
                GearChanceHelpers.AddGenerationChances(bot);
                GearChanceHelpers.AddModChances(bot);

                foreach (var rawParsedBot in _rawParsedBots.Where(x => x.Info.Settings.Role.Equals(bot.botType.ToString(), StringComparison.OrdinalIgnoreCase)))
                {
                    GearHelpers.AddEquippedGear(bot, rawParsedBot);
                    GearHelpers.AddEquippedMods(bot, rawParsedBot);
                    GearHelpers.AddCartridges(bot, rawParsedBot);
                }
            }

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot gear. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return _baseBots;
        }
    }
}
