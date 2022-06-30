using Common;
using Common.Extensions;
using Common.Models.Input;
using Common.Models.Output;
using Generator.Helpers.Gear;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Generator
{
    public static class BotLootGenerator
    {
        internal static IEnumerable<Bot> AddLoot(this IEnumerable<Bot> botsWithGear, IEnumerable<Datum> rawBots)
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot loot");

            // Iterate over assault/raider etc
            Parallel.ForEach(botsWithGear, botToUpdate =>
            {
                var botType = botToUpdate.botType.ToString();
                var rawBotsOfSameType = rawBots
                                        .Where(x => x.Info.Settings.Role.Equals(botType, StringComparison.OrdinalIgnoreCase))
                                        .ToList();

                if (rawBotsOfSameType.Count == 0)
                {
                    return;
                }

                foreach (var rawParsedBot in rawBotsOfSameType)
                {
                    AddPocketLoot(botToUpdate, rawParsedBot);
                }

                AddTacticalVestLoot(botToUpdate, rawBotsOfSameType);
                AddBackpackLoot(botToUpdate, rawBotsOfSameType);
                AddSecureContainerLoot(botToUpdate, rawBotsOfSameType);
                AddSpecialLoot(botToUpdate);
            });

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot loot. Took: {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return botsWithGear;
        }

        private static void AddPocketLoot(Bot botToUpdate, Datum rawBot)
        {
            // pocket loot
            foreach (var lootItem in rawBot.Inventory.items.Where(x => x?.slotId?.StartsWith("pocket") == true))
            {
                botToUpdate.inventory.items.Pockets.AddUnique(lootItem._tpl);
            }
        }

        private static void AddTacticalVestLoot(Bot botToUpdate, IEnumerable<Datum> rawBots)
        {
            var tacVestItems = GetItemsStoredInEquipmentItem(rawBots, "TacticalVest");
            botToUpdate.inventory.items.TacticalVest.AddRange(tacVestItems);
        }

        private static void AddBackpackLoot(Bot botToUpdate, IEnumerable<Datum> rawBots)
        {
            // add generic keys to bosses
            if (botToUpdate.botType.IsBoss())
            {
                botToUpdate.inventory.items.Backpack.AddRange(SpecialLootHelper.GetGenericBossKeys());
            }

            var backpackItems = GetItemsStoredInEquipmentItem(rawBots, "Backpack");
            botToUpdate.inventory.items.Backpack.AddRange(backpackItems);
        }

        private static void AddSecureContainerLoot(Bot botToUpdate, IEnumerable<Datum> rawBots)
        {
            var tacVestItems = GetItemsStoredInEquipmentItem(rawBots, "SecuredContainer");
            botToUpdate.inventory.items.SecuredContainer.AddRange(tacVestItems);
        }

        private static void AddSpecialLoot(Bot botToUpdate)
        {
            botToUpdate.inventory.items.SpecialLoot.AddRange(SpecialLootHelper.GetSpecialLootForBotType(botToUpdate.botType));
        }

        private static IEnumerable<string> GetItemsStoredInEquipmentItem(IEnumerable<Datum> rawBots, string containerName)
        {
            var itemsStoredInContainer = new List<string>();
            var containers = new List<string>();
            foreach (var bot in rawBots)
            {
                // find the container type we want on this bot (backpack etc)
                // Add to list
                var botContainers = bot.Inventory.items.Where(x => x.slotId == containerName);
                foreach (var c in botContainers)
                {
                    containers.AddUnique(c._id);
                }

                foreach (var item in bot.Inventory.items)
                {
                    if (containers.Contains(item.parentId))
                    {
                        itemsStoredInContainer.AddUnique(item._tpl);
                    }
                }
            }

            return itemsStoredInContainer;
        }
    }
}
