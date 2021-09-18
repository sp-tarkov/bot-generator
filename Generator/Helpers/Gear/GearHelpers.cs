using Common.Extensions;
using Common.Models.Input;
using Common.Models.Output;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Helpers.Gear
{
    public static class GearHelpers
    {
        public static void AddEquippedMods(Bot botToUpdate, Datum rawParsedBot)
        {
            var modItemsInRawBot = new List<Item>();
            var itemsWithModsInRawBot = new List<Item>();

            modItemsInRawBot = rawParsedBot.Inventory.items
                .Where(x => x.slotId != null && (x.slotId.StartsWith("mod_") || x.slotId.StartsWith("patron_in_weapon"))).ToList();

            // get items with Mods by iterating over mod items and getting the parent item
            itemsWithModsInRawBot.AddRange(modItemsInRawBot
                .Select(modItem => rawParsedBot.Inventory.items
                .Find(x => x._id == modItem.parentId)));

            var itemsWithModsDictionary = botToUpdate.inventory.mods;
            foreach (var itemToAdd in itemsWithModsInRawBot)
            {
                var modsToAdd = modItemsInRawBot.Where(x => x.parentId == itemToAdd._id).ToList();

                AddItemToDictionary(itemToAdd, modsToAdd, itemsWithModsDictionary);
            }

            botToUpdate.inventory.mods = itemsWithModsDictionary;
        }

        public static void AddEquippedGear(Bot botToUpdate, Datum bot)
        {
            // add equipped gear
            foreach (var inventoryItem in bot.Inventory.items)
            {
                switch (inventoryItem.slotId?.ToLower())
                {
                    case "headwear":
                        botToUpdate.inventory.equipment.Headwear.AddUnique(inventoryItem._tpl);
                        break;
                    case "earpiece":
                        botToUpdate.inventory.equipment.Earpiece.AddUnique(inventoryItem._tpl);
                        break;
                    case "facecover":
                        botToUpdate.inventory.equipment.FaceCover.AddUnique(inventoryItem._tpl);
                        break;
                    case "armorvest":
                        botToUpdate.inventory.equipment.ArmorVest.AddUnique(inventoryItem._tpl);
                        break;
                    case "eyewear":
                        botToUpdate.inventory.equipment.Eyewear.AddUnique(inventoryItem._tpl);
                        break;
                    case "armband":
                        botToUpdate.inventory.equipment.ArmBand.AddUnique(inventoryItem._tpl);
                        break;
                    case "tacticalvest":
                        botToUpdate.inventory.equipment.TacticalVest.AddUnique(inventoryItem._tpl);
                        break;
                    case "backpack":
                        botToUpdate.inventory.equipment.Backpack.AddUnique(inventoryItem._tpl);
                        break;
                    case "firstprimaryweapon":
                        botToUpdate.inventory.equipment.FirstPrimaryWeapon.AddUnique(inventoryItem._tpl);
                        break;
                    case "secondprimaryweapon":
                        botToUpdate.inventory.equipment.SecondPrimaryWeapon.AddUnique(inventoryItem._tpl);
                        break;
                    case "holster":
                        botToUpdate.inventory.equipment.Holster.AddUnique(inventoryItem._tpl);
                        break;
                    case "scabbard":
                        botToUpdate.inventory.equipment.Scabbard.AddUnique(inventoryItem._tpl);
                        break;
                    case "pockets":
                        botToUpdate.inventory.equipment.Pockets.AddUnique(inventoryItem._tpl);
                        break;
                    case "securedcontainer":
                        botToUpdate.inventory.equipment.SecuredContainer.AddUnique(inventoryItem._tpl);
                        break;
                    default:
                        break;
                }
            }
        }


        public static void AddCartridges(Bot botToUpdate, Datum rawParsedBot)
        {
            var cartridgesInRawBot = rawParsedBot.Inventory.items
                .Where(x => x.slotId?.StartsWith("cartridges") == true).ToList();
            var cartridgeParentIds = cartridgesInRawBot.Select(x => x.parentId).ToList();
            var itemsThatTakeCartridges = rawParsedBot.Inventory.items.Where(x => cartridgeParentIds.Contains(x._id)).ToList();

            var itemsThatTakeCartridgesDict = CreateDictionaryPopulateWithMagazinesAndCartridges(itemsThatTakeCartridges, cartridgesInRawBot);
            foreach (var item in itemsThatTakeCartridgesDict)
            {
                // Item exists update
                if (botToUpdate.inventory.mods.ContainsKey(item.Key))
                {
                    UpdateExistingMagazine(botToUpdate, item);
                }
                else // No item found, add fresh
                {
                    AddNewMagazine(botToUpdate, item);
                }
            }
        }

        private static void AddItemToDictionary(
            Item itemToAdd,
            List<Item> modsToAdd,
            Dictionary<string, Dictionary<string, List<string>>> itemsWithModsDict)
        {
            // item key already exists, need to merge mods
            if (itemsWithModsDict.ContainsKey(itemToAdd._tpl))
            {
                foreach (var modItem in modsToAdd)
                {
                    var itemToAddModsTo = itemsWithModsDict[itemToAdd._tpl];
                    // Item doesnt have this mod, add it then add template id
                    if (!itemToAddModsTo.ContainsKey(modItem.slotId))
                    {
                        // Mod doesnt exist on item
                        itemToAddModsTo.Add(modItem.slotId, new List<string>()); // add mod
                        itemToAddModsTo[modItem.slotId].AddUnique(modItem._tpl);
                    }

                    itemToAddModsTo[modItem.slotId].AddUnique(modItem._tpl); // add template id to it
                }
            }
            else // item doesnt exist, create it
            {
                // Add base item
                itemsWithModsDict.Add(itemToAdd._tpl, new Dictionary<string, List<string>>());
                // Add mod types to item
                foreach (var modItem in modsToAdd)
                {
                    itemsWithModsDict[itemToAdd._tpl].Add(modItem.slotId, new List<string>());
                }

                // Get mod we're adding mod templateIds to
                var modItems = itemsWithModsDict[itemToAdd._tpl];
                foreach (var modItem in modsToAdd)
                {
                    var modToUpdate = modItems[modItem.slotId];
                    modToUpdate.Add(modItem._tpl);
                }
            }
        }


        private static void UpdateExistingMagazine(Bot botToUpdate, KeyValuePair<string, List<string>> item)
        {
            var existingmagazineItem = botToUpdate.inventory.mods[item.Key];
            var cartridges = existingmagazineItem["cartridges"];

            foreach (var itemToAdd in item.Value)
            {
                cartridges.AddUnique(itemToAdd);
            }
        }

        private static void AddNewMagazine(Bot botToUpdate, KeyValuePair<string, List<string>> item)
        {
            var cartridgeDict = new Dictionary<string, List<string>>
                {
                    { "cartridges", item.Value }
                };
            botToUpdate.inventory.mods.Add(item.Key, cartridgeDict);
        }

        private static Dictionary<string, List<string>> CreateDictionaryPopulateWithMagazinesAndCartridges(List<Item> itemsThatTakeCartridges, List<Item> cartridgesInRawBot)
        {
            var itemsThatTakeCartridgesDict = new Dictionary<string, List<string>>();
            foreach (var item in itemsThatTakeCartridges)
            {
                var cartridgeIdsToAdd = cartridgesInRawBot.Where(x => x.parentId == item._id).Select(x => x._tpl).ToList();

                // magazine id already exists, probably has cartridges in it already
                if (itemsThatTakeCartridgesDict.ContainsKey(item._tpl))
                {
                    //get existing magazine and add new cartridges to it
                    var existingMagazine = itemsThatTakeCartridgesDict[item._tpl];
                    foreach (var cartridge in cartridgeIdsToAdd)
                    {
                        existingMagazine.AddUnique(cartridge);
                    }
                }
                else // No magazine found, add new magazine + associated cartridges
                {
                    itemsThatTakeCartridgesDict.Add(item._tpl, cartridgeIdsToAdd);
                }
            }

            return itemsThatTakeCartridgesDict;
        }
    }
}
