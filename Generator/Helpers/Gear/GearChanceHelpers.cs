using System;
using Common.Models.Input;
using Common.Models.Output;
using System.Collections.Generic;
using System.Linq;
using Common.Models;
using Generator.Weighting;

namespace Generator.Helpers.Gear
{
    public static class GearChanceHelpers
    {
        public static void CalculateModChances(Bot bot, List<Datum> baseBots)
        {
            // TODO: Further split these counts by equipment slot? (ex. "FirstPrimaryWeapon", "Holster", etc.)

            var validSlots = new List<string> { "FirstPrimaryWeapon", "SecondPrimaryWeapon", "Holster", "Headwear" };

            var modCounts = new Dictionary<string, int>();
            var slotCounts = new Dictionary<string, int>();

            foreach (var baseBot in baseBots)
            {
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

                    if ((inventoryItem.slotId?.StartsWith("mod_") ?? false) && !(parentTemplate?._props?.Slots?.FirstOrDefault(s => s._name == inventoryItem.slotId)?._required ?? false))
                    {
                        if (modCounts.ContainsKey(inventoryItem.slotId))
                        {
                            modCounts[inventoryItem.slotId]++;
                        }
                        else
                        {
                            modCounts.Add(inventoryItem.slotId, 1);
                        }
                    }

                    if ((template?._props?.Slots?.Count ?? 0) < 1)
                    {
                        // Item has no slots, nothing to count here
                        continue;
                    }

                    foreach (var slot in template._props.Slots.Where(s => s._name.StartsWith("mod_")))
                    {
                        if (slot._required)
                        {
                            continue;
                        }

                        if (slotCounts.ContainsKey(slot._name))
                        {
                            slotCounts[slot._name]++;
                        }
                        else
                        {
                            slotCounts.Add(slot._name, 1);
                        }
                    }
                }
            }

            bot.chances.mods = slotCounts.ToDictionary(
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
                    botToUpdate.chances.mods["mod_stock"] = 100;
                    botToUpdate.chances.mods["mod_scope"] = 100;
                    break;
                case BotType.bosstagilla:
                    botToUpdate.chances.mods["mod_tactical"] = 100; // force ultima thermal camera
                    botToUpdate.chances.mods["mod_stock"] = 100;
                    break;
                case BotType.bossbully:
                    botToUpdate.chances.mods["mod_stock"] = 100;
                    break;
                case BotType.bosskilla:
                    botToUpdate.chances.mods["mod_stock"] = 100;
                    botToUpdate.chances.mods["mod_stock_001"] = 100;
                    break;
                case BotType.bosssanitar:
                    botToUpdate.chances.mods["mod_scope"] = 100;
                    break;
                case BotType.pmcbot:
                    botToUpdate.chances.mods["mod_stock"] = 100;
                    break;
                case BotType.followerbully:
                    botToUpdate.chances.mods["mod_stock"] = 100;
                    botToUpdate.chances.mods["mod_stock_000"] = 100;
                    break;
                case BotType.followergluharassault:
                case BotType.followergluharscout:
                case BotType.followergluharsecurity:
                case BotType.followergluharsnipe:
                    botToUpdate.chances.mods["mod_stock"] = 100;
                    break;
                case BotType.followerkojaniy:
                    botToUpdate.chances.mods["mod_stock"] = 100;
                    break;
                case BotType.sectantpriest:
                    botToUpdate.chances.mods["mod_stock"] = 100;
                    break;
                case BotType.sectantwarrior:
                    botToUpdate.chances.mods["mod_stock"] = 100;
                    break;
                case BotType.marksman:
                    botToUpdate.chances.mods["mod_scope"] = 100;
                    botToUpdate.chances.mods["mod_stock"] = 100;
                    break;
                case BotType.exusec:
                    botToUpdate.chances.mods["mod_stock"] = 100;
                    botToUpdate.chances.mods["mod_stock_000"] = 100;
                    botToUpdate.chances.mods["mod_stock_001"] = 100;
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
                weightsData["backpackLoot"],
                weightsData["pocketLoot"],
                weightsData["vestLoot"],
                weightsData["magazines"],
                weightsData["grenades"]);

            // it makes some crazy values, one assault bot has 10 grenades!
            //AddGrenadeMinMax(bot, rawBots);

        }

        public static void CalculateEquipmentChances(Bot bot, List<Datum> baseBots)
        {
            // TODO: Convert to dynamic?
            var totalBotsCount = baseBots.Count;
            int headwearCount = 0, earCount = 0, faceCoverCount = 0, armorVestCount = 0, eyeWearCount = 0, armBandCount = 0,
                tacticalVestCount = 0, backpackCount = 0, firstPrimaryCount = 0, secondPrimaryCount = 0, holsterCount = 0,
                scabbardCount = 0, pocketsCount = 0, securedContainerCount = 0;

            foreach (var baseBot in baseBots)
            {
                headwearCount += baseBot.Inventory.items.Count(x => x.slotId == "Headwear");
                earCount += baseBot.Inventory.items.Count(x => x.slotId == "Earpiece");
                faceCoverCount += baseBot.Inventory.items.Count(x => x.slotId == "FaceCover");
                armorVestCount += baseBot.Inventory.items.Count(x => x.slotId == "ArmorVest");
                eyeWearCount += baseBot.Inventory.items.Count(x => x.slotId == "Eyewear");
                armBandCount += baseBot.Inventory.items.Count(x => x.slotId == "ArmBand");
                tacticalVestCount += baseBot.Inventory.items.Count(x => x.slotId == "TacticalVest");
                backpackCount += baseBot.Inventory.items.Count(x => x.slotId == "Backpack");
                firstPrimaryCount += baseBot.Inventory.items.Count(x => x.slotId == "FirstPrimaryWeapon");
                secondPrimaryCount += baseBot.Inventory.items.Count(x => x.slotId == "SecondPrimaryWeapon");
                holsterCount += baseBot.Inventory.items.Count(x => x.slotId == "Holster");
                scabbardCount += baseBot.Inventory.items.Count(x => x.slotId == "Scabbard");
                pocketsCount += baseBot.Inventory.items.Count(x => x.slotId == "Pockets");
                securedContainerCount += baseBot.Inventory.items.Count(x => x.slotId == "SecuredContainer");
            }

            bot.chances.equipment = new EquipmentChances(
                  GetPercent(totalBotsCount, headwearCount),
                  GetPercent(totalBotsCount, earCount),
                  GetPercent(totalBotsCount, faceCoverCount),
                  GetPercent(totalBotsCount, armorVestCount),
                  GetPercent(totalBotsCount, eyeWearCount),
                  GetPercent(totalBotsCount, armBandCount),
                  GetPercent(totalBotsCount, tacticalVestCount),
                  GetPercent(totalBotsCount, backpackCount),
                  GetPercent(totalBotsCount, firstPrimaryCount),
                  GetPercent(totalBotsCount, secondPrimaryCount),
                  GetPercent(totalBotsCount, holsterCount),
                  GetPercent(totalBotsCount, scabbardCount),
                  GetPercent(totalBotsCount, pocketsCount),
                  GetPercent(totalBotsCount, securedContainerCount));
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
