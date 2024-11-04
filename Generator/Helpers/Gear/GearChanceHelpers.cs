using Common.Models.Input;
using Common.Models.Output;
using Common.Models;
using Generator.Weighting;

namespace Generator.Helpers.Gear
{
    public static class GearChanceHelpers
    {
        private static Dictionary<string, Dictionary<string, int>> weaponModCount = new Dictionary<string, Dictionary<string, int>>();
        private static Dictionary<string, Dictionary<string, int>> weaponSlotCount = new Dictionary<string, Dictionary<string, int>>();
        private static Dictionary<string, Dictionary<string, int>> equipmentModCount = new Dictionary<string, Dictionary<string, int>>();
        private static Dictionary<string, Dictionary<string, int>> equipmentSlotCount = new Dictionary<string, Dictionary<string, int>>();


        public static void AddModChances(Bot bot, Datum baseBot)
        {
            // TODO: Further split these counts by equipment slot? (ex. "FirstPrimaryWeapon", "Holster", etc.)
            var validSlots = new List<string> { "FirstPrimaryWeapon", "SecondPrimaryWeapon", "Holster" };

            if (!weaponModCount.TryGetValue(baseBot.Info.Settings.Role.ToLower(), out var modCounts))
            {
                modCounts = new Dictionary<string, int>();
                weaponModCount.Add(baseBot.Info.Settings.Role.ToLower(), modCounts);
            }

            if (!weaponSlotCount.TryGetValue(baseBot.Info.Settings.Role.ToLower(), out var slotCounts))
            {
                slotCounts = new Dictionary<string, int>();
                weaponSlotCount.Add(baseBot.Info.Settings.Role.ToLower(), slotCounts);
            }

            var validParents = new List<string>();
            foreach (var inventoryItem in baseBot.Inventory.items)
            {
                if (validSlots.Contains(inventoryItem.slotId))
                {
                    validParents.Add(inventoryItem._id);
                }
                else if (validParents.Contains(inventoryItem.parentId))
                {
                    validParents.Add(inventoryItem._id);
                }
                else
                {
                    continue;
                }

                var template = ItemTemplateHelper.GetTemplateById(inventoryItem._tpl);
                var parentTemplate = ItemTemplateHelper.GetTemplateById(baseBot.Inventory.items.Single(i => i._id == inventoryItem.parentId)._tpl);

                if (!(parentTemplate?._props?.Slots?.FirstOrDefault(slot => slot._name == inventoryItem.slotId)?._required ?? false))
                {
                    if (modCounts.ContainsKey(inventoryItem.slotId.ToLower()))
                    {
                        modCounts[inventoryItem.slotId.ToLower()]++;
                    }
                    else
                    {
                        modCounts.Add(inventoryItem.slotId.ToLower(), 1);
                    }
                }

                if ((template?._props?.Slots?.Count ?? 0) < 1)
                {
                    // Item has no slots, nothing to count here
                    continue;
                }

                foreach (var slot in template._props.Slots)
                {
                    if (slot._required)
                    {
                        continue;
                    }

                    if (slot._name.StartsWith("camora"))
                    {
                        continue;
                    }

                    if (slotCounts.ContainsKey(slot._name.ToLower()))
                    {
                        slotCounts[slot._name.ToLower()]++;
                    }
                    else
                    {
                        slotCounts.Add(slot._name.ToLower(), 1);
                    }
                }
            }
        }

        public static void CalculateModChances(Bot bot)
        {
            if (!weaponModCount.TryGetValue(bot.botType.ToString(), out var modCounts))
            {
                modCounts = new Dictionary<string, int>();
                weaponModCount.Add(bot.botType.ToString(), modCounts);
            }

            if (!weaponSlotCount.TryGetValue(bot.botType.ToString(), out var slotCounts))
            {
                slotCounts = new Dictionary<string, int>();
                weaponSlotCount.Add(bot.botType.ToString(), slotCounts);
            }

            bot.chances.weaponMods = slotCounts.ToDictionary(
                kvp => kvp.Key,
                kvp => GetPercent(kvp.Value, modCounts.GetValueOrDefault(kvp.Key)));
        }

        public static void AddEquipmentModChances(Bot bot, Datum baseBot)
        {
            if (!equipmentModCount.TryGetValue(baseBot.Info.Settings.Role.ToLower(), out var modCounts))
            {
                modCounts = new Dictionary<string, int>();
                equipmentModCount.Add(baseBot.Info.Settings.Role.ToLower(), modCounts);
            }

            if (!equipmentSlotCount.TryGetValue(baseBot.Info.Settings.Role.ToLower(), out var slotCounts))
            {
                slotCounts = new Dictionary<string, int>();
                equipmentSlotCount.Add(baseBot.Info.Settings.Role.ToLower(), slotCounts);
            }

            // TODO: Further split these counts by equipment slot? (ex. "FirstPrimaryWeapon", "Holster", etc.)
            var validSlots = new List<string> { "Headwear", "ArmorVest", "TacticalVest" };

            var validParents = new List<string>();

            foreach (var inventoryItem in baseBot.Inventory.items)
            {
                if (validSlots.Contains(inventoryItem.slotId))
                {
                    validParents.Add(inventoryItem._id);
                }
                else if (validParents.Contains(inventoryItem.parentId))
                {
                    validParents.Add(inventoryItem._id);
                }
                else
                {
                    continue;
                }

                var template = ItemTemplateHelper.GetTemplateById(inventoryItem._tpl);
                var parentTemplate = ItemTemplateHelper.GetTemplateById(baseBot.Inventory.items.Single(i => i._id == inventoryItem.parentId)._tpl);

                if (!(parentTemplate?._props?.Slots?.FirstOrDefault(slot => slot._name == inventoryItem.slotId)?._required ?? false))
                {
                    if (modCounts.ContainsKey(inventoryItem.slotId.ToLower()))
                    {
                        modCounts[inventoryItem.slotId.ToLower()]++;
                    }
                    else
                    {
                        modCounts.Add(inventoryItem.slotId.ToLower(), 1);
                    }
                }

                if ((template?._props?.Slots?.Count ?? 0) < 1)
                {
                    // Item has no slots, nothing to count here
                    continue;
                }

                foreach (var slot in template._props.Slots)
                {
                    if (slot._required)
                    {
                        continue;
                    }

                    if (slot._name.StartsWith("camora"))
                    {
                        continue;
                    }

                    if (slotCounts.ContainsKey(slot._name.ToLower()))
                    {
                        slotCounts[slot._name.ToLower()]++;
                    }
                    else
                    {
                        slotCounts.Add(slot._name.ToLower(), 1);
                    }
                }
            }
        }

        public static void CalculateEquipmentModChances(Bot bot)
        {
            if (!equipmentModCount.TryGetValue(bot.botType.ToString(), out var modCounts))
            {
                modCounts = new Dictionary<string, int>();
                equipmentModCount.Add(bot.botType.ToString(), modCounts);
            }

            if (!equipmentSlotCount.TryGetValue(bot.botType.ToString(), out var slotCounts))
            {
                slotCounts = new Dictionary<string, int>();
                equipmentSlotCount.Add(bot.botType.ToString(), slotCounts);
            }

            bot.chances.equipmentMods = slotCounts.ToDictionary(
                kvp => kvp.Key,
                kvp => GetPercent(kvp.Value, modCounts.GetValueOrDefault(kvp.Key)));
        }

        internal static void ApplyEquipmentChanceOverrides(Bot botToUpdate)
        {
            switch (botToUpdate.botType)
            {
                case BotType.bosstagilla:
                    botToUpdate.chances.equipment.FaceCover = 100;
                    break;
            }
        }

        public static void ApplyModChanceOverrides(Bot botToUpdate)
        {
            switch (botToUpdate.botType)
            {
                case BotType.bosskojaniy:
                    botToUpdate.chances.weaponMods["mod_stock"] = 100;
                    botToUpdate.chances.weaponMods["mod_scope"] = 100;
                    break;
                case BotType.bosstagilla:
                    botToUpdate.chances.weaponMods["mod_tactical"] = 100; // force ultima thermal camera
                    botToUpdate.chances.weaponMods["mod_stock"] = 100;
                    break;
                case BotType.bossbully:
                    botToUpdate.chances.weaponMods["mod_stock"] = 100;
                    break;
                case BotType.bosskilla:
                    botToUpdate.chances.weaponMods["mod_stock"] = 100;
                    botToUpdate.chances.weaponMods["mod_stock_001"] = 100;
                    break;
                case BotType.bosssanitar:
                    botToUpdate.chances.weaponMods["mod_scope"] = 100;
                    break;
                case BotType.pmcbot:
                    botToUpdate.chances.weaponMods["mod_stock"] = 100;
                    break;
                case BotType.followerbully:
                    botToUpdate.chances.weaponMods["mod_stock"] = 100;
                    botToUpdate.chances.weaponMods["mod_stock_000"] = 100;
                    break;
                case BotType.followergluharassault:
                case BotType.followergluharscout:
                case BotType.followergluharsecurity:
                case BotType.followergluharsnipe:
                    botToUpdate.chances.weaponMods["mod_stock"] = 100;
                    break;
                case BotType.followerkojaniy:
                    botToUpdate.chances.weaponMods["mod_stock"] = 100;
                    break;
                case BotType.sectantpriest:
                    botToUpdate.chances.weaponMods["mod_stock"] = 100;
                    break;
                case BotType.sectantwarrior:
                    botToUpdate.chances.weaponMods["mod_stock"] = 100;
                    break;
                case BotType.marksman:
                    botToUpdate.chances.weaponMods["mod_scope"] = 100;
                    botToUpdate.chances.weaponMods["mod_stock"] = 100;
                    break;
                case BotType.exusec:
                    botToUpdate.chances.weaponMods["mod_stock"] = 100;
                    botToUpdate.chances.weaponMods["mod_stock_000"] = 100;
                    botToUpdate.chances.weaponMods["mod_stock_001"] = 100;
                    break;

            }
        }

        public static void AddGenerationChances(Bot bot, WeightingService weightingService)
        {
            var weightsData = weightingService.GetBotGenerationWeights(bot.botType);
            bot.generation = new GenerationChances(
                weightsData["specialItems"],
                weightsData["healing"],
                weightsData["drugs"],
                weightsData["stims"],
                weightsData["food"],
                weightsData["drinks"],
                weightsData["currency"],
                weightsData["backpackLoot"],
                weightsData["pocketLoot"],
                weightsData["vestLoot"],
                weightsData["magazines"],
                weightsData["grenades"]);
        }

        public static void AddEquipmentChances(Bot bot, Datum baseBot)
        {
            bot.chances.equipment.Headwear += baseBot.Inventory.items.Count(x => x.slotId == "Headwear");
            bot.chances.equipment.Earpiece += baseBot.Inventory.items.Count(x => x.slotId == "Earpiece");
            bot.chances.equipment.FaceCover += baseBot.Inventory.items.Count(x => x.slotId == "FaceCover");
            bot.chances.equipment.ArmorVest += baseBot.Inventory.items.Count(x => x.slotId == "ArmorVest");
            bot.chances.equipment.Eyewear += baseBot.Inventory.items.Count(x => x.slotId == "Eyewear");
            bot.chances.equipment.ArmBand += baseBot.Inventory.items.Count(x => x.slotId == "ArmBand");
            bot.chances.equipment.TacticalVest += baseBot.Inventory.items.Count(x => x.slotId == "TacticalVest");
            bot.chances.equipment.Backpack += baseBot.Inventory.items.Count(x => x.slotId == "Backpack");
            bot.chances.equipment.FirstPrimaryWeapon += baseBot.Inventory.items.Count(x => x.slotId == "FirstPrimaryWeapon");
            bot.chances.equipment.SecondPrimaryWeapon += baseBot.Inventory.items.Count(x => x.slotId == "SecondPrimaryWeapon");
            bot.chances.equipment.Holster += baseBot.Inventory.items.Count(x => x.slotId == "Holster");
            bot.chances.equipment.Scabbard += baseBot.Inventory.items.Count(x => x.slotId == "Scabbard");
            bot.chances.equipment.Pockets += baseBot.Inventory.items.Count(x => x.slotId == "Pockets");
            bot.chances.equipment.SecuredContainer += baseBot.Inventory.items.Count(x => x.slotId == "SecuredContainer");
        }

        public static void CalculateEquipmentChances(Bot bot)
        {
            if (bot.botCount == 0)
            {
                // No bots, don't do anything
                return;
            }

            bot.chances.equipment = new EquipmentChances(
                  GetPercent(bot.botCount, bot.chances.equipment.Headwear),
                  GetPercent(bot.botCount, bot.chances.equipment.Earpiece),
                  GetPercent(bot.botCount, bot.chances.equipment.FaceCover),
                  GetPercent(bot.botCount, bot.chances.equipment.ArmorVest),
                  GetPercent(bot.botCount, bot.chances.equipment.Eyewear),
                  GetPercent(bot.botCount, bot.chances.equipment.ArmBand),
                  GetPercent(bot.botCount, bot.chances.equipment.TacticalVest),
                  GetPercent(bot.botCount, bot.chances.equipment.Backpack),
                  GetPercent(bot.botCount, bot.chances.equipment.FirstPrimaryWeapon),
                  GetPercent(bot.botCount, bot.chances.equipment.SecondPrimaryWeapon),
                  GetPercent(bot.botCount, bot.chances.equipment.Holster),
                  GetPercent(bot.botCount, bot.chances.equipment.Scabbard),
                  GetPercent(bot.botCount, bot.chances.equipment.Pockets),
                  GetPercent(bot.botCount, bot.chances.equipment.SecuredContainer));
        }

        private static int GetPercent(int total, int count)
        {
            var percentChance = (int)Math.Ceiling((double)(((200 * count) + 1) / (total * 2)));
            return percentChance > 100 ? 100 : percentChance; // return 100 if value is > 100
        }

        private static MinMax GetMagazineCountByBotType(BotType botType)
        {
            int min;
            int max;

            switch (botType)
            {
                case BotType.bosskilla:
                    min = 3;
                    max = 3;
                    break;
                default:
                    min = 2;
                    max = 4;
                    break;
            }

            return new MinMax(min, max);
        }

        private static MinMax GetLooseLootCountByBotType(BotType botType)
        {
            int min;
            int max;

            switch (botType)
            {
                case BotType.assault:
                    min= 0;
                    max= 6;
                    break;
                case BotType.marksman:
                    min = 0;
                    max = 0;
                    break;
                case BotType.exusec:
                    min = 2;
                    max = 4;
                    break;
                case BotType.bossbully:
                    min = 3;
                    max= 7;
                    break;
                case BotType.bossgluhar:
                    min = 2;
                    max = 9;
                    break;
                case BotType.bosskilla:
                    min = 4;
                    max = 10;
                    break;
                case BotType.bosskojaniy:
                    min = 0;
                    max = 7;
                    break;
                case BotType.bosssanitar:
                case BotType.followersanitar:
                    min = 2;
                    max = 5;
                    break;
                default:
                    min = 1;
                    max = 4;
                    break;
            }

            return new MinMax(min, max);
        }

        private static MinMax GetMedicalItemCountByBotType(BotType botType)
        {
            int min;
            int max;

            switch (botType)
            {
                case BotType.bosssanitar:
                case BotType.followersanitar:
                    min = 4;
                    max = 7;
                    break;
                default:
                    min = 1;
                    max = 2;
                    break;
            }

            return new MinMax(min, max);
        }
    }
}
