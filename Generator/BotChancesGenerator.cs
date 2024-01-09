using Generator.Helpers.Gear;
using Common.Models.Input;
using Common.Models.Output;
using System.Diagnostics;
using Generator.Weighting;

namespace Generator
{
    public static class BotChancesGenerator
    {
        public static IEnumerable<Bot> AddChances(this IEnumerable<Bot> botsToUpdate, Dictionary<string, List<Datum>> rawBots)
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot gear");

            // use lock for lock safety
            var dictionaryLock = new object();
            var weightHelper = new WeightingService();
            // multithread
            var tasks = new List<Task>();
            foreach (var botToUpdate in botsToUpdate)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    var botType = botToUpdate.botType.ToString().ToLower();
                    List<Datum> rawBotsOfSameType;
                    lock (dictionaryLock)
                    {
                        if (!rawBots.TryGetValue(botType, out rawBotsOfSameType))
                        {
                            Console.WriteLine($"(chances) Unable to find {botType} on rawBots data");
                            return;
                        }
                    }

                    if (rawBotsOfSameType.Count == 0)
                    {
                        return;
                    }

                    // TODO: Add check to make sure incoming bot list has gear
                    GearChanceHelpers.CalculateEquipmentChances(botToUpdate, rawBotsOfSameType);
                    GearChanceHelpers.AddGenerationChances(botToUpdate, weightHelper);
                    GearChanceHelpers.CalculateModChances(botToUpdate, rawBotsOfSameType);
                    GearChanceHelpers.CalculateEquipmentModChances(botToUpdate, rawBotsOfSameType);
                    GearChanceHelpers.ApplyModChanceOverrides(botToUpdate);
                    GearChanceHelpers.ApplyEquipmentChanceOverrides(botToUpdate);
                }));
            }

            Task.WaitAll(tasks.ToArray());
            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot chances. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return botsToUpdate;
        }
    }
}
