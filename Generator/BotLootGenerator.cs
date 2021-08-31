using Common;
using Common.Extensions;
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
    public class BotLootGenerator
    {
        private readonly List<Bot> _botsWithGear;
        private readonly List<Datum> _rawParsedBots;

        public BotLootGenerator(List<Bot> botsWithGear, List<Datum> rawParsedBots)
        {
            _botsWithGear = botsWithGear;
            _rawParsedBots = rawParsedBots;
        }

        internal List<Bot> AddLoot()
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot loot");

            // Iterate over assault/raider etc
            foreach (var botToUpdate in _botsWithGear)
            {
                var rawBotsOfSameType = _rawParsedBots
                    .Where(x => x.Info.Settings.Role.Equals(botToUpdate.botType.ToString(), StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (rawBotsOfSameType.Count == 0)
                {
                    continue;
                }

                foreach (var rawParsedBot in rawBotsOfSameType)
                {
                    AddPocketLoot(botToUpdate, rawParsedBot);
                }

                AddTacticalVestLoot(botToUpdate, rawBotsOfSameType);
                AddBackbackLoot(botToUpdate, rawBotsOfSameType);
                AddSecureContainerLoot(botToUpdate, rawBotsOfSameType);
                AddSpecialLoot(botToUpdate);
            }

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot loot. Took: {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return _botsWithGear;
        }

        private void AddPocketLoot(Bot finalBot, Datum bot)
        {
            // pocket loot
            foreach (var lootItem in bot.Inventory.items.Where(x => x?.slotId?.StartsWith("pocket") == true))
            {
                finalBot.inventory.items.Pockets.AddUnique(lootItem._tpl);
            }
        }

        private void AddTacticalVestLoot(Bot finalBot, List<Datum> bots)
        {
            var tacVestItems = GetItemsStoredInEquipmentItem(bots, "TacticalVest");
            finalBot.inventory.items.TacticalVest.AddRange(tacVestItems);
        }

        private void AddBackbackLoot(Bot finalBot, List<Datum> bots)
        {
            // add generic keys to bosses
            if (finalBot.botType.IsBoss())
            {
                finalBot.inventory.items.Backpack.AddRange(SpecialLootHelper.GetGenericBossKeys());
            }

            var backpackItems = GetItemsStoredInEquipmentItem(bots, "Backpack");
            finalBot.inventory.items.Backpack.AddRange(backpackItems);
        }

        private void AddSecureContainerLoot(Bot finalAssaultBot, List<Datum> bots)
        {
            var tacVestItems = GetItemsStoredInEquipmentItem(bots, "SecuredContainer");
            finalAssaultBot.inventory.items.SecuredContainer.AddRange(tacVestItems);
        }

        private void AddSpecialLoot(Bot botToUpdate)
        {
            botToUpdate.inventory.items.SpecialLoot.AddRange(SpecialLootHelper.GetSpecialLootForBotType(botToUpdate.botType));
        }

        private List<string> GetItemsStoredInEquipmentItem(List<Datum> bots, string containerName)
        {
            var itemsStoredInContainer = new List<string>();
            var containers = new List<string>();
            foreach (var bot in bots)
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
