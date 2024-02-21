using System.Diagnostics;
using System.Linq;
using Common.Extensions;
using Common.Models.Input;
using Common.Models.Output;
using Generator.Helpers.Gear;

namespace Generator
{
    public static class BotLootGenerator
    {
        internal static IEnumerable<Bot> AddLoot(this IEnumerable<Bot> botsWithGear, Dictionary<string, List<Datum>> rawBots)
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot loot");

            var dictionaryLock = new object();

            var tasks = new List<Task>(50);
            foreach (var botToUpdate in botsWithGear)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    var botType = botToUpdate.botType.ToString().ToLower();
                    List<Datum> rawBotsOfSameType;
                    lock (dictionaryLock)
                    {
                        if (!rawBots.TryGetValue(botType, out rawBotsOfSameType))
                        {
                            Console.WriteLine($"(loot) Unable to find {botType} on rawBots data");
                            return;
                        }
                    }

                    if (rawBotsOfSameType.Count == 0)
                    {
                        return;
                    }

                    AddLootToContainers(botType, botToUpdate, rawBotsOfSameType);
                    GearHelpers.ReduceWeightValues(botToUpdate.inventory.equipment.Backpack);
                    GearHelpers.ReduceWeightValues(botToUpdate.inventory.equipment.Pockets);
                    GearHelpers.ReduceWeightValues(botToUpdate.inventory.equipment.TacticalVest);
                    GearHelpers.ReduceWeightValues(botToUpdate.inventory.equipment.SecuredContainer);

                    //foreach (var rawParsedBot in rawBotsOfSameType)
                    //{
                    //    AddPocketLoot(botToUpdate, rawParsedBot);
                    //}

                    //AddTacticalVestLoot(botToUpdate, rawBotsOfSameType);
                    //AddBackpackLoot(botToUpdate, rawBotsOfSameType);
                    //AddSecureContainerLoot(botToUpdate, rawBotsOfSameType);
                    //AddSpecialLoot(botToUpdate);
                }));
            }

            Task.WaitAll(tasks.ToArray());

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot loot. Took: {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return botsWithGear;
        }

        private static void AddLootToContainers(string botType, Bot botToUpdate, List<Datum> rawBotsOfSameType)
        {
            // Process each bot
            foreach (var rawBot in rawBotsOfSameType)
            {
                // Filter out base inventory items and equipment mod items
                var rawBotItems = rawBot.Inventory.items.Where(x => x.parentId != null || x.location != null).ToList();
                
                var botBackpack = rawBotItems.FirstOrDefault(item => item.slotId == "Backpack");
                if (botBackpack != null)
                {
                    AddLootItemsToContainerDictionary(rawBotItems, botBackpack._id, botToUpdate.inventory.items.Backpack);
                }

                var botPockets = rawBotItems.FirstOrDefault(item => item.slotId == "Pockets");
                if (botPockets != null)
                {
                    AddLootItemsToContainerDictionary(rawBotItems, botPockets._id, botToUpdate.inventory.items.Pockets);
                }

                var botVest = rawBotItems.FirstOrDefault(item => item.slotId == "TacticalVest");
                if (botVest != null)
                {
                    AddLootItemsToContainerDictionary(rawBotItems, botVest._id, botToUpdate.inventory.items.TacticalVest);
                }

                var botSecure = rawBotItems.FirstOrDefault(item => item.slotId == "SecuredContainer");
                if (botSecure != null)
                {
                    AddLootItemsToContainerDictionary(rawBotItems, botSecure._id, botToUpdate.inventory.items.SecuredContainer);
                }

                // Add generic keys to bosses
                if (botToUpdate.botType.IsBoss())
                {
                    var keys = SpecialLootHelper.GetGenericBossKeysDictionary();
                    foreach (var bosskey in keys)
                    {
                        if (!botToUpdate.inventory.items.Backpack.ContainsKey(bosskey.Key))
                        {
                            botToUpdate.inventory.items.Backpack.Add(bosskey.Key, bosskey.Value);
                        }
                    }
                }

                AddSpecialLoot(botToUpdate);
            }
        }

        /// <summary>
        /// Look for items inside itemsToFilter that have the parentid of `containerId` and add them to dictToAddTo
        /// Keep track of how many items are added in the dictToAddTo value
        /// </summary>
        /// <param name="itemsToFilter">Bots inventory items</param>
        /// <param name="containerId"></param>
        /// <param name="dictToAddTo"></param>
        private static void AddLootItemsToContainerDictionary(List<Item> itemsToFilter, string containerId, Dictionary<string, int> dictToAddTo)
        {
            var backpackLootItems = itemsToFilter.Where(item => item.parentId == containerId);
            foreach (var backpackItem in backpackLootItems)
            {
                if (!dictToAddTo.ContainsKey(backpackItem._tpl))
                {
                    dictToAddTo[backpackItem._tpl] = 1;

                    return;
                }

                dictToAddTo[backpackItem._tpl]++;
            }
        }

        private static void AddSpecialLoot(Bot botToUpdate)
        {
            var itemsToAdd = SpecialLootHelper.GetSpecialLootForBotType(botToUpdate.botType);
            foreach (var item in itemsToAdd)
            {
                if (!botToUpdate.inventory.items.SpecialLoot.ContainsKey(item.Key))
                {
                    botToUpdate.inventory.items.SpecialLoot.Add(item.Key, item.Value);
                }
            }
        }
    }
}
