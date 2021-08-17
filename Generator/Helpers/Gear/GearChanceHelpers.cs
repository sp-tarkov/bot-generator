using Generator.Models;
using Generator.Models.Input;
using Generator.Models.Output;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Helpers.Gear
{
    public static class GearChanceHelpers
    {
        public static void CalculateModChances(Bot bot, List<Datum> baseBots)
        {
            int totalBotsCount = baseBots.Count;
            int muzzleCount = 0, barrelCount = 0, handguardCount = 0, stockCount = 0, magazineCount = 0,
                mountCount = 0, flashlightCount = 0, tactical001Count = 0, tactical002Count = 0, tactical003Count = 0,
                mount000Count = 0, pistolGripCount = 0, tacticalCount = 0, scopeCount = 0, recieverCount = 0,
                sightRearCount = 0, chargeCount = 0, mount001Count = 0, equipmentCount = 0, gasBlockCount = 0,
                launcherCount = 0, sightFrontCount = 0, stock000Count = 0, foregripCount = 0, tactical000Count = 0,
                nvgCount = 0, pistolGripAkmsCount = 0, stockAkmsCount = 0, equipment000Count = 0, equipment001Count = 0,
                equipment002Count = 0, bipodCount = 0;

            foreach (var baseBot in baseBots)
            {
                muzzleCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_muzzle");
                barrelCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_barrel");
                handguardCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_handguard");
                stockCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_stock");
                magazineCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_magazine");
                mountCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_mount");
                flashlightCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_flashlight");
                tactical001Count += baseBot.Inventory.items.Count(x => x.slotId == "mod_tactical_001");
                tactical002Count += baseBot.Inventory.items.Count(x => x.slotId == "mod_tactical_002");
                tactical003Count += baseBot.Inventory.items.Count(x => x.slotId == "mod_tactical_003");
                mount000Count += baseBot.Inventory.items.Count(x => x.slotId == "mod_mount_000");
                pistolGripCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_pistol_grip");
                tacticalCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_tactical");
                scopeCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_scope");
                recieverCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_reciever");
                sightRearCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_sight_rear");
                chargeCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_charge");
                mount001Count += baseBot.Inventory.items.Count(x => x.slotId == "mod_mount_001");
                equipmentCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_equipment");
                gasBlockCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_gas_block");
                launcherCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_launcher");
                sightFrontCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_sight_front");
                stock000Count += baseBot.Inventory.items.Count(x => x.slotId == "mod_stock_000");
                foregripCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_foregrip");
                tactical000Count += baseBot.Inventory.items.Count(x => x.slotId == "mod_tactical_000");
                nvgCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_nvg");
                pistolGripAkmsCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_pistol_grip_akms");
                stockAkmsCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_stock_akms");
                equipment000Count += baseBot.Inventory.items.Count(x => x.slotId == "mod_equipment_000");
                equipment001Count += baseBot.Inventory.items.Count(x => x.slotId == "mod_equipment_001");
                equipment002Count += baseBot.Inventory.items.Count(x => x.slotId == "mod_equipment_002");
                bipodCount += baseBot.Inventory.items.Count(x => x.slotId == "mod_bipod");
            }

            bot.chances.mods = new Mods(
                GetPercent(totalBotsCount, muzzleCount),
                GetPercent(totalBotsCount, barrelCount),
                GetPercent(totalBotsCount, handguardCount),
                GetPercent(totalBotsCount, stockCount),
                GetPercent(totalBotsCount, magazineCount),
                GetPercent(totalBotsCount, mountCount),
                GetPercent(totalBotsCount, flashlightCount),
                GetPercent(totalBotsCount, tactical001Count),
                GetPercent(totalBotsCount, tactical002Count),
                GetPercent(totalBotsCount, tactical003Count),
                GetPercent(totalBotsCount, mount000Count),
                GetPercent(totalBotsCount, pistolGripCount),
                GetPercent(totalBotsCount, tacticalCount),
                GetPercent(totalBotsCount, scopeCount),
                GetPercent(totalBotsCount, recieverCount),
                GetPercent(totalBotsCount, sightRearCount),
                GetPercent(totalBotsCount, chargeCount),
                GetPercent(totalBotsCount, mount001Count),
                GetPercent(totalBotsCount, equipmentCount),
                GetPercent(totalBotsCount, gasBlockCount),
                GetPercent(totalBotsCount, launcherCount),
                GetPercent(totalBotsCount, sightFrontCount),
                GetPercent(totalBotsCount, stock000Count),
                GetPercent(totalBotsCount, foregripCount),
                GetPercent(totalBotsCount, tactical000Count),
                GetPercent(totalBotsCount, nvgCount),
                GetPercent(totalBotsCount, pistolGripAkmsCount),
                GetPercent(totalBotsCount, stockAkmsCount),
                GetPercent(totalBotsCount, equipment000Count),
                GetPercent(totalBotsCount, equipment001Count),
                GetPercent(totalBotsCount, equipment002Count),
                GetPercent(totalBotsCount, bipodCount));
        }

        public static void AddGenerationChances(Bot bot)
        {
            switch (bot.botType)
            {
                case BotType.assault:
                case BotType.pmcBot:
                case BotType.marksman:
                    bot.generation = new GenerationChances(0, 1, 1, 2, 0, 3, 2, 4, 0, 5); //TODO get dynamically
                    break;
            }
        }

        public static void CalculateEquipmentChances(Bot bot, List<Datum> baseBots)
        {
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
            return ((200 * count) + 1) / (total * 2);
        }
    }
}
