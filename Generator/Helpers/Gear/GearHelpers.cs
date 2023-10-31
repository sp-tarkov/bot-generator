using Common.Extensions;
using Common.Models.Input;
using Common.Models.Output;
using Generator.Weighting;

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

            // Get items with Mods by iterating over mod items and getting the parent item
            itemsWithModsInRawBot.AddRange(modItemsInRawBot
                .Select(modItem => rawParsedBot.Inventory.items
                .Find(x => x._id == modItem.parentId)));

            var itemsWithModsDictionary = botToUpdate.inventory.mods;
            foreach (var itemToAdd in itemsWithModsInRawBot)
            {
                var modsToAdd = modItemsInRawBot.Where(x => x.parentId == itemToAdd._id).ToList();

                // fix pistolgrip that changes slot id name
                if (itemToAdd._tpl == "56e0598dd2720bb5668b45a6")
                {
                    var badMod = modsToAdd.FirstOrDefault(x => x.slotId == "mod_pistol_grip" && x._tpl == "56e05a6ed2720bd0748b4567");
                    if (badMod != null)
                    {
                        badMod.slotId = "mod_pistolgrip";
                    }
                }

                AddItemToDictionary(itemToAdd, modsToAdd, itemsWithModsDictionary);

                // check if these mods have sub-mods and add those
                foreach (var modAdded in modsToAdd.Where(x => x.slotId == "mod_magazine"))
                {
                    // look for items where parentId is this mods id
                    var subItems = rawParsedBot.Inventory.items.Where(x => x.parentId == modAdded._id && x.slotId != "cartridges").ToList();
                    if (subItems.Count > 0)
                    {
                        AddItemToDictionary(modAdded, subItems, itemsWithModsDictionary);
                    }
                }
            }

            botToUpdate.inventory.mods = itemsWithModsDictionary;
        }

        internal static void AddAmmo(Bot botToUpdate, Datum bot)
        {
            //var weightService = new WeightingService();
            foreach (var ammo in bot.Inventory.items.Where(
                x => x.slotId != null 
            && (x.slotId == "patron_in_weapon"
            || (x.slotId == "cartridges" && bot.Inventory.items.FirstOrDefault(parent => parent._id == x.parentId)?.slotId != "main") // Ignore cartridges in ammo boxes for ammo usage calc
            || x.slotId.StartsWith("camora"))))
            {
                var caliber = ItemTemplateHelper.GetTemplateById(ammo._tpl)._props.ammoCaliber;
                if (caliber == null)
                {
                    caliber = ItemTemplateHelper.GetTemplateById(ammo._tpl)._props.Caliber;
                }

                // Create key if caliber doesnt exist
                if (!botToUpdate.inventory.Ammo.ContainsKey(caliber))
                {
                    botToUpdate.inventory.Ammo[caliber] = new Dictionary<string, int>();
                }

                if (!botToUpdate.inventory.Ammo[caliber].ContainsKey(ammo._tpl))
                {
                    botToUpdate.inventory.Ammo[caliber][ammo._tpl] = 0;
                }

                botToUpdate.inventory.Ammo[caliber][ammo._tpl] ++;
            }
        }

        public static int CommonDivisor(List<int> numbers)
        {
            int result = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                result = GCD(result, numbers[i]);
            }
            return result;
        }

        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static void AddEquippedGear(Bot botToUpdate, Datum bot)
        {
            // add equipped gear
            var weightService = new WeightingService();
            foreach (var inventoryItem in bot.Inventory.items.Where(x=>x.slotId != null))
            {
                switch (inventoryItem.slotId?.ToLower())
                {
                    case "headwear":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.Headwear, inventoryItem._tpl);
                        break;
                    case "earpiece":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.Earpiece, inventoryItem._tpl);
                        break;
                    case "facecover":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.FaceCover, inventoryItem._tpl);
                        break;
                    case "armorvest":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.ArmorVest, inventoryItem._tpl);
                        break;
                    case "eyewear":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.Eyewear, inventoryItem._tpl);
                        break;
                    case "armband":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.ArmBand, inventoryItem._tpl);
                        break;
                    case "tacticalvest":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.TacticalVest, inventoryItem._tpl);
                        break;
                    case "backpack":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.Backpack, inventoryItem._tpl);
                        break;
                    case "firstprimaryweapon":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.FirstPrimaryWeapon, inventoryItem._tpl);
                        break;
                    case "secondprimaryweapon":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.SecondPrimaryWeapon, inventoryItem._tpl);
                        break;
                    case "holster":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.Holster, inventoryItem._tpl);
                        break;
                    case "scabbard":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.Scabbard, inventoryItem._tpl);
                        break;
                    case "pockets":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.Pockets, inventoryItem._tpl);
                        break;
                    case "securedcontainer":
                        IncrementDictionaryValue(botToUpdate.inventory.equipment.SecuredContainer, inventoryItem._tpl);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void IncrementDictionaryValue(Dictionary<string, int> dictToIncrement, string key)
        {
            if (!dictToIncrement.ContainsKey(key))
            {
                dictToIncrement[key] = 0;
            }

            dictToIncrement[key]++;
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

        internal static void ReduceAmmoWeightValues(Bot botToUpdate)
        {
            foreach (var caliber in botToUpdate.inventory.Ammo)
            {
                foreach (var cartridge in botToUpdate.inventory.Ammo.Keys)
                {
                    var cartridgeWithWeights = botToUpdate.inventory.Ammo[cartridge];
                    var weights = cartridgeWithWeights.Values.Select(x => x).ToList();
                    var commonAmmoDivisor = CommonDivisor(weights);

                    foreach (var cartridgeWeightKvP in cartridgeWithWeights)
                    {
                        botToUpdate.inventory.Ammo[cartridge][cartridgeWeightKvP.Key] /= commonAmmoDivisor;
                    }
                }
            }            
        }

        public static void ReduceEquipmentWeightValues(Equipment equipment)
        {
            ReduceWeightValues(equipment.Headwear);
            ReduceWeightValues(equipment.Earpiece);
            ReduceWeightValues(equipment.FaceCover);
            ReduceWeightValues(equipment.ArmorVest);
            ReduceWeightValues(equipment.Eyewear);
            ReduceWeightValues(equipment.ArmBand);
            ReduceWeightValues(equipment.TacticalVest);
            ReduceWeightValues(equipment.Backpack);
            ReduceWeightValues(equipment.FirstPrimaryWeapon);
            ReduceWeightValues(equipment.SecondPrimaryWeapon);
            ReduceWeightValues(equipment.Scabbard);
            ReduceWeightValues(equipment.Holster);
            ReduceWeightValues(equipment.Pockets);
            ReduceWeightValues(equipment.SecuredContainer);
        }

        public static void ReduceWeightValues(Dictionary<string, int> equipmentDict)
        {
            // No values, nothing to reduce
            if (equipmentDict.Count == 0)
            {
                return;
            }

            // Only one value, quickly set to 1 and exit
            if (equipmentDict.Count == 1)
            {
                equipmentDict[equipmentDict.First().Key] = 1;

                return;
            }

            var weights = equipmentDict.Values.Select(x => x).ToList();
            var commonAmmoDivisor = CommonDivisor(weights);

            // No point in dividing by 1
            if (commonAmmoDivisor == 1)
            {
                return;
            }

            foreach (var itemTplWithWeight in equipmentDict)
            {
                equipmentDict[itemTplWithWeight.Key] /= commonAmmoDivisor;
            }
        }
    }
}
