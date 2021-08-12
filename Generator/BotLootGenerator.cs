using Generator.Helpers;
using Generator.Models.Input;
using Generator.Models.Output;
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
            foreach (var bot in _botsWithGear)
            {
                foreach (var rawParsedBot in _rawParsedBots)
                {
                    AddPocketLoot(bot, rawParsedBot);
                }

                AddTacticalVestLoot(bot, _rawParsedBots);
                AddBackbackLoot(bot, _rawParsedBots);
                AddSecureContainerLoot(bot, _rawParsedBots);
            }

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Finished processing bot loot. Took: {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return _botsWithGear;
        }

        private void AddTacticalVestLoot(Bot finalAssaultBot, List<Datum> bots)
        {
            var tacVestItems = GetItemsStoredInEquipmentItem(bots, "TacticalVest");
            finalAssaultBot.inventory.items.TacticalVest.AddRange(tacVestItems);
        }

        private void AddBackbackLoot(Bot finalAssaultBot, List<Datum> bots)
        {
            var backpackItems = GetItemsStoredInEquipmentItem(bots, "Backpack");
            finalAssaultBot.inventory.items.Backpack.AddRange(backpackItems);
        }

        

        private void AddSecureContainerLoot(Bot finalAssaultBot, List<Datum> bots)
        {
            var tacVestItems = GetItemsStoredInEquipmentItem(bots, "SecuredContainer");
            finalAssaultBot.inventory.items.SecuredContainer.AddRange(tacVestItems);
        }

        private void AddPocketLoot(Bot finalBot, Datum bot)
        {
            // pocket loot
            foreach (var lootItem in bot.Inventory.items.Where(x => x?.slotId?.StartsWith("pocket") == true))
            {
                finalBot.inventory.items.Pockets.AddUnique(lootItem._tpl);
            }
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
