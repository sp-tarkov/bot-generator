using System.Diagnostics;
using System.Linq;
using Common.Extensions;
using Common.Models;
using Common.Models.Input;
using Common.Models.Output;
using Generator.Helpers.Gear;

namespace Generator
{
    public static class BotLootGenerator
    {
        internal static void AddLoot(Bot botToUpdate, Datum rawBotData)
        {
            AddLootToContainers(botToUpdate, rawBotData);
        }

        private static void AddLootToContainers(Bot botToUpdate, Datum rawBot)
        {
            // Filter out base inventory items and equipment mod items
            var rawBotItems = rawBot.Inventory.items.Where(item => item.location != null);
                
            var botBackpack = rawBot.Inventory.items.FirstOrDefault(item => item.slotId == "Backpack");
            if (botBackpack != null)
            {
                AddLootItemsToContainerDictionary(rawBotItems, botBackpack._id, botToUpdate.inventory.items.Backpack, "backpack", botToUpdate.botType);
            }

            var botPockets = rawBot.Inventory.items.FirstOrDefault(item => item.slotId == "Pockets");
            if (botPockets != null)
            {
                AddLootItemsToContainerDictionary(rawBotItems, botPockets._id, botToUpdate.inventory.items.Pockets);
            }

            var botVest = rawBot.Inventory.items.FirstOrDefault(item => item.slotId == "TacticalVest");
            if (botVest != null)
            {
                AddLootItemsToContainerDictionary(rawBotItems, botVest._id, botToUpdate.inventory.items.TacticalVest);
            }

            var botSecure = rawBot.Inventory.items.FirstOrDefault(item => item.slotId == "SecuredContainer");
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

            // Cleanup of weights
            GearHelpers.ReduceWeightValues(botToUpdate.inventory.items.Backpack);
            GearHelpers.ReduceWeightValues(botToUpdate.inventory.items.Pockets);
            GearHelpers.ReduceWeightValues(botToUpdate.inventory.items.TacticalVest);
            GearHelpers.ReduceWeightValues(botToUpdate.inventory.items.SecuredContainer);
        }

        /// <summary>
        /// Look for items inside itemsToFilter that have the parentid of `containerId` and add them to dictToAddTo
        /// Keep track of how many items are added in the dictToAddTo value
        /// </summary>
        /// <param name="itemsToFilter">Bots inventory items</param>
        /// <param name="containerId"></param>
        /// <param name="dictToAddTo"></param>
        private static void AddLootItemsToContainerDictionary(IEnumerable<Common.Models.Input.Item> itemsToFilter, string containerId, Dictionary<string, int> dictToAddTo, string container = "", BotType type = BotType.arenafighterevent)
        {
            foreach (var itemToAdd in itemsToFilter)
            {
                if (itemToAdd.parentId != containerId) continue;

                if (!dictToAddTo.ContainsKey(itemToAdd._tpl))
                {
                    dictToAddTo[itemToAdd._tpl] = 1;

                    continue;
                }

                dictToAddTo[itemToAdd._tpl]++;
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
