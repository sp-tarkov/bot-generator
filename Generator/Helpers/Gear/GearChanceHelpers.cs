using Generator.Models;
using Generator.Models.Output;

namespace Generator.Helpers.Gear
{
    public static class GearChanceHelpers
    {
        public static void AddModChances(Bot bot)
        {
            switch (bot.botType)
            {
                case BotType.assault:
                    bot.chances.mods = new Mods(muzzle: 18, barrel: 100, handguard: 100, stock: 66, magazine: 100,
                        mount: 15, flashlight: 100, tactical_001: 99, tactical_002: 0, tactical_003: 0,
                        mount_000: 56, pistol_grip: 97, tactical: 87, scope: 17, reciever: 92,
                        sight_rear: 56, charge: 13, mount_001: 0, equipment: 30, gas_block: 100,
                        launcher: 0, sight_front: 25, stock_000: 0, foregrip: 0, tactical_000: 0,
                        nvg: 0, pistol_grip_akms: 100, stock_akms: 100, equipment_000: 0, equipment_001: 0,
                        equipment_002: 0, bipod: 0);
                    break;
                case BotType.pmcBot:
                    bot.chances.mods = new Mods(muzzle: 67, barrel: 100, handguard: 97, stock: 81, magazine: 100,
                        mount: 35, flashlight: 100, tactical_001: 9, tactical_002: 0, tactical_003: 0,
                        mount_000: 28, pistol_grip: 97, tactical: 32, scope: 57, reciever: 100,
                        sight_rear: 58, charge: 81, mount_001: 38, equipment: 0, gas_block: 100,
                        launcher: 0, sight_front: 65, stock_000: 100, foregrip: 30, tactical_000: 18,
                        nvg: 25, pistol_grip_akms: 97, stock_akms: 0, equipment_000: 0, equipment_001: 0,
                        equipment_002: 0, bipod: 0);
                    break;
                case BotType.marksman:
                    bot.chances.mods = new Mods(muzzle: 0, barrel: 100, handguard: 0, stock: 73, magazine: 100,
                        mount: 100, flashlight: 0, tactical_001: 0, tactical_002: 0, tactical_003: 0,
                        mount_000: 0, pistol_grip: 0, tactical: 33, scope: 89, reciever: 0,
                        sight_rear: 17, charge: 0, mount_001: 0, equipment: 0, gas_block: 0,
                        launcher: 0, sight_front: 25, stock_000: 0, foregrip: 0, tactical_000: 33,
                        nvg: 100, pistol_grip_akms: 0, stock_akms: 0, equipment_000: 0, equipment_001: 0,
                        equipment_002: 0, bipod: 0);
                    break;
            }
        }

        public static void AddGenerationChances(Bot bot)
        {
            switch (bot.botType)
            {
                case BotType.assault:
                case BotType.pmcBot:
                case BotType.marksman:
                    bot.generation = new GenerationChances(0, 1, 1, 2, 0, 3, 2, 4, 0, 5);
                    break;
            }
        }

        public static void AddEquipmentChances(Bot bot)
        {
            switch (bot.botType)
            {
                case BotType.assault:
                    bot.chances.equipment = new EquipmentChances(73, 0, 62, 28, 36, 0, 100, 38, 95, 0, 5, 72, 100, 100);
                    break;
                case BotType.pmcBot:
                    bot.chances.equipment = new EquipmentChances(89, 56, 58, 49, 84, 0, 100, 58, 100, 0, 18, 0, 100, 100);
                    break;
                case BotType.marksman:
                    bot.chances.equipment = new EquipmentChances(8, 8, 8, 42, 0, 0, 100, 25, 100, 0, 0, 33, 100, 100);
                    break;
            }
        }

    }
}
