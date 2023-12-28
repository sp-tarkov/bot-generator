using Common.Models.Input;
using Common.Models.Output;
using Generator.Helpers.Gear;
using System.Diagnostics;

namespace Generator
{
    public static class BotGearGenerator
    {
        public static IEnumerable<Bot> AddGear(this IEnumerable<Bot> baseBots, Dictionary<string, List<Datum>> rawBots)
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot gear");

            var dictionaryLock = new object();
            var tasks = new List<Task>();

            foreach (var botToUpdate in baseBots)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    var botType = botToUpdate.botType.ToString().ToLower();
                    List<Datum> rawBotsOfSameType;
                    lock (dictionaryLock)
                    {
                        if (!rawBots.TryGetValue(botType, out rawBotsOfSameType))
                        {
                            Console.WriteLine($"(gear) Unable to find {botType} on rawBots data");
                            return;
                        }  
                    }

                    if (rawBotsOfSameType.Count == 0)
                    {
                        return;
                    }

                    foreach (var rawParsedBot in rawBotsOfSameType)
                    {
                        GearHelpers.AddEquippedGear(botToUpdate, rawParsedBot);
                        GearHelpers.AddAmmo(botToUpdate, rawParsedBot);
                        GearHelpers.AddEquippedMods(botToUpdate, rawParsedBot);
                        //GearHelpers.AddCartridges(botToUpdate, rawParsedBot);
                    }

                    GearHelpers.ReduceAmmoWeightValues(botToUpdate);
                    GearHelpers.ReduceEquipmentWeightValues(botToUpdate.inventory.equipment);
                }));
            }

            Task.WaitAll(tasks.ToArray());
            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot gear. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return baseBots;
        }
    }
}
