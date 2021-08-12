using Generator.Helpers;
using Generator.Models;
using Generator.Models.Input;
using Generator.Models.Output;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Generator
{
    public class BotGearGenerator
    {
        private readonly List<Bot> _baseBots;
        private readonly List<Datum> _rawParsedBots;

        public BotGearGenerator(List<Bot> baseBots, List<Datum> parsedBots)
        {
            _baseBots = baseBots;
            _rawParsedBots = parsedBots;
        }

        internal List<Bot> AddGear()
        {
            var stopwatch = Stopwatch.StartNew();
            LoggingHelpers.LogToConsole("Started processing bot gear");

            foreach (var bot in _baseBots)
            {
                AddEquipmentChances(bot);
                AddGenerationChances(bot);
                AddModChances(bot);
                foreach (var rawParsedBot in _rawParsedBots)
                {
                    AddEquippedGear(bot, rawParsedBot);
                    AddEquippedMods(bot, rawParsedBot);
                }
            }

            stopwatch.Stop();
            LoggingHelpers.LogToConsole($"Time taken to generate gear: {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");

            return _baseBots;
        }

        private void AddEquippedMods(Bot botToUpdate, Datum rawParsedBot)
        {
            var modItemsInRawBot = new List<Item>();
            var itemsWithModsInRawBot = new List<Item>();

            modItemsInRawBot = rawParsedBot.Inventory.items
                .Where(x => x.slotId?.StartsWith("mod_") == true).ToList();

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

        private void AddItemToDictionary(
            Item itemToAdd,
            List<Item> modsToAdd,
            Dictionary<string, Dictionary<string, List<string>>> itemsWithModsDictionary)
        {
            // item key already exists, need to merge mods
            if (itemsWithModsDictionary.ContainsKey(itemToAdd.slotId))
            {
                foreach (var modItem in modsToAdd)
                {
                    var itemToAddModsTo = itemsWithModsDictionary[itemToAdd.slotId];
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
                itemsWithModsDictionary.Add(itemToAdd.slotId, new Dictionary<string, List<string>>());
                // Add mod types to item
                foreach (var modItem in modsToAdd)
                {
                    itemsWithModsDictionary[itemToAdd.slotId].Add(modItem.slotId, new List<string>());
                }

                // Get item we're adding mod templateIds to
                var itemToUpdate = itemsWithModsDictionary[itemToAdd.slotId];
                foreach (var modItem in modsToAdd)
                {
                    var modToUpdate = itemToUpdate[modItem.slotId];
                    modToUpdate.Add(modItem._tpl);
                }

            }
            var result = JsonConvert.SerializeObject(itemsWithModsDictionary, Formatting.Indented);
        }

        private void AddModChances(Bot bot)
        {
            switch (bot.botType)
            {
                case BotType.assault:
                    bot.chances.mods = new Mods(muzzle: 18, barrel: 100, handguard: 100, stock: 66, magazine: 100,
                        mount: 15, flashlight: 100, tactical_001: 99, tactical_002: 0, tactical_003: 0,
                        mount_000: 15, pistol_grip: 97, tactical: 87, scope: 17, reciever: 92,
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

        private void AddGenerationChances(Bot bot)
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

        private void AddEquipmentChances(Bot bot)
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

        private void AddEquippedGear(Bot finalAssaultBot, Datum bot)
        {
            // add equipped gear
            foreach (var inventoryItem in bot.Inventory.items)
            {
                switch (inventoryItem.slotId?.ToLower())
                {
                    case "headwear":
                        finalAssaultBot.inventory.equipment.Headwear.AddUnique(inventoryItem._tpl);
                        break;
                    case "earpiece":
                        finalAssaultBot.inventory.equipment.Earpiece.AddUnique(inventoryItem._tpl);
                        break;
                    case "facecover":
                        finalAssaultBot.inventory.equipment.FaceCover.AddUnique(inventoryItem._tpl);
                        break;
                    case "armorvest":
                        finalAssaultBot.inventory.equipment.ArmorVest.AddUnique(inventoryItem._tpl);
                        break;
                    case "eyewear":
                        finalAssaultBot.inventory.equipment.Eyewear.AddUnique(inventoryItem._tpl);
                        break;
                    case "armband":
                        finalAssaultBot.inventory.equipment.ArmBand.AddUnique(inventoryItem._tpl);
                        break;
                    case "tacticalvest":
                        finalAssaultBot.inventory.equipment.TacticalVest.AddUnique(inventoryItem._tpl);
                        break;
                    case "backpack":
                        finalAssaultBot.inventory.equipment.Backpack.AddUnique(inventoryItem._tpl);
                        break;
                    case "firstprimaryweapon":
                        finalAssaultBot.inventory.equipment.FirstPrimaryWeapon.AddUnique(inventoryItem._tpl);
                        break;
                    case "secondprimaryweapon":
                        finalAssaultBot.inventory.equipment.SecondPrimaryWeapon.AddUnique(inventoryItem._tpl);
                        break;
                    case "holster":
                        finalAssaultBot.inventory.equipment.Holster.AddUnique(inventoryItem._tpl);
                        break;
                    case "scabbard":
                        finalAssaultBot.inventory.equipment.Scabbard.AddUnique(inventoryItem._tpl);
                        break;
                    case "pockets":
                        finalAssaultBot.inventory.equipment.Pockets.AddUnique(inventoryItem._tpl);
                        break;
                    case "securedcontainer":
                        finalAssaultBot.inventory.equipment.SecuredContainer.AddUnique(inventoryItem._tpl);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
