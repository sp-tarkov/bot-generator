using Common;
using Common.Extensions;
using Newtonsoft.Json;
using PMCGenerator.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Models;
using Generator.Helpers;

namespace PMCGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Presets> parsedPresets = GetPresets();

            // Create flat lists of weapons + list of mods
            var flatPrimaryWeaponsList = GetWeaponsFromRawFile(parsedPresets);
            var flatSecondaryWeaponsList = GetSecondaryWeaponsFromRawFile(parsedPresets);
            var flatAllWeaponsList = CombinePrimaryAndSecondaryWeapons(flatPrimaryWeaponsList, flatSecondaryWeaponsList);

            var output = new
            {
                FirstPrimaryWeapon = AddWeaponsToOutput(flatPrimaryWeaponsList),
                Holster = AddWeaponsToOutput(flatSecondaryWeaponsList),
                mods = AddModsToOutput(flatAllWeaponsList, parsedPresets, flatPrimaryWeaponsList),
                Ammo = AddAmmoToOutput(flatAllWeaponsList, parsedPresets)
            };

            // Create output dir
            var outputPath = CreateOutputFolder();

            // Turn into json
            var outputJson = JsonConvert.SerializeObject(output, Formatting.Indented);

            CreateJsonFile(outputPath, outputJson);
        }

        private static Dictionary<string, int> AddWeaponsToOutput(List<WeaponDetails> flatPrimaryWeaponsList)
        {
            var results = new Dictionary<string, int>();

            var distinctPrimaryWeaponIds = flatPrimaryWeaponsList.Select(x => x.TemplateId).Distinct();
            foreach (var primaryWeapon in distinctPrimaryWeaponIds.Select(id => new KeyValuePair<string, int>(id, GetWeaponWeighting(id))))
            {
                results.Add(primaryWeapon.Key, primaryWeapon.Value);
            }

            return results;
        }

        private static int GetWeaponWeighting(string id)
        {
            // TODO get weighting data from styrr
            return 1;
        }

        private static Dictionary<string, Dictionary<string, List<string>>> AddModsToOutput(
            List<WeaponDetails> flatAllWeaponsList,
            List<Presets> parsedPresets,
            List<WeaponDetails> flatPrimaryWeaponsList)
        {
            var result = new Dictionary<string, Dictionary<string, List<string>>>();
            var itemLibrary = GetItemLibrary();
            var flatModList = GetModsFromRawFile(parsedPresets);

            // Time to generate mods for weapons
            foreach (var weapon in flatAllWeaponsList)
            {
                // add weapon id if its not already here
                if (!result.ContainsKey(weapon.TemplateId))
                {
                    // Add weapon to dictionary
                    result.Add(weapon.TemplateId, new Dictionary<string, List<string>>());
                }

                // Get top level mod types for this gun
                var uniqueModSlots = flatModList.Where(x => x.ParentId == weapon.Id).Select(x => x.SlotId).Distinct().ToList();

                var chamberedBulletModItemName = "patron_in_weapon";
                if (weapon.TemplateId != "60db29ce99594040e04c4a27" && weapon.TemplateId != "5580223e4bdc2d1c128b457f") // not shotgun revolver or double barrel
                {
                    uniqueModSlots.AddUnique(chamberedBulletModItemName);
                }

                if (weapon.TemplateId == "60db29ce99594040e04c4a27") // shotgun revolver
                {
                    // live file has: mod_barrel, mod_stock, mod_handguard, mod_magazine
                }

                if (weapon.TemplateId == "5580223e4bdc2d1c128b457f") // double barrel
                {
                    uniqueModSlots.AddUnique("patron_in_weapon_000");
                    uniqueModSlots.AddUnique("patron_in_weapon_001");
                }

                foreach (var modSlotId in uniqueModSlots)
                {
                    Dictionary<string, List<string>> weaponModsToModify = result[weapon.TemplateId];

                    if (!weaponModsToModify.ContainsKey(modSlotId))
                    {
                        weaponModsToModify.Add(modSlotId, new List<string>());
                    }
                }

                // Add compatible bullets to weapons gun chamber
                var compatibleBullets = GetCompatibileBullets(itemLibrary, weapon);
                var modItemToAddBulletsTo = result[weapon.TemplateId].FirstOrDefault(x => x.Key == chamberedBulletModItemName);
                if (modItemToAddBulletsTo.Key != null) // some guns dont have a mod you add bullets to (e.g. revolvers)
                {
                    modItemToAddBulletsTo.Value.AddUniqueRange(compatibleBullets);
                }

                // Add compatabible mods to weapon
                var modsForWeapon = flatModList.Where(x => x.ParentId == weapon.Id).ToList();
                Dictionary<string, List<string>> weaponMods = result[weapon.TemplateId];
                foreach (var mod in modsForWeapon)
                {
                    weaponMods[mod.SlotId].AddUnique(mod.TemplateId);

                    if (mod.SlotId == "mod_magazine")
                    {
                        // add special mod item for magazine that gives info on what cartridges can be used
                        AddCartridgeItemToModListWithCompatibileCartridges(result, compatibleBullets, mod);
                    }
                }
            }

            // Get mods where parent is not weapon and add to output
            foreach (var mod in flatModList.Where(x => x.ParentId != null
                        && !flatPrimaryWeaponsList.Any(y => y.Id == x.ParentId)).ToList())
            {
                // No parent tempalte id found, create and add mods details
                if (!result.ContainsKey(mod.ParentTemplateId))
                {
                    var templateIdsList = new List<string> { mod.TemplateId };
                    var subtype = new Dictionary<string, List<string>> { { mod.SlotId, templateIdsList } };
                    result.Add(mod.ParentTemplateId, subtype);
                }

                //Add subtype to item
                var subtypeToAddTo = result[mod.ParentTemplateId];
                // No subtype, add it
                if (!subtypeToAddTo.ContainsKey(mod.SlotId))
                {
                    var valueToAdd = new List<string>() { mod.TemplateId };
                    subtypeToAddTo.Add(mod.SlotId, valueToAdd);
                }

                // subtype exists, add to it
                subtypeToAddTo[mod.SlotId].AddUnique(mod.TemplateId);
            }

            return result;
        }

        private static Dictionary<string, Dictionary<string, int>> AddAmmoToOutput(List<WeaponDetails> flatAllWeaponsList, List<Presets> parsedPresets)
        {
            var result = new Dictionary<string, Dictionary<string, int>>();
            var itemLibrary = GetItemLibrary();

            foreach (var weapon in flatAllWeaponsList)
            {
                var weaponDetails = itemLibrary.FirstOrDefault(x => x.Key == weapon.TemplateId).Value;
                var caliber = weaponDetails._props.Caliber != null ? weaponDetails._props.Caliber : weaponDetails._props.ammoCaliber;

                List<string> cartridges = new List<string>();
                if (weaponDetails._props.Chambers?.Count > 0)
                {
                    cartridges.AddRange(weaponDetails._props.Chambers[0]._props.filters[0].filter);
                }
                else if (weaponDetails._props.Slots.Any(x => x._name == "mod_magazine"))
                {
                    var magazine = weaponDetails._props.Slots.FirstOrDefault(x => x._name == "mod_magazine");
                    cartridges.AddRange(magazine._props.filters[0].filter);
                }
                else if (weaponDetails._props.Slots.Any(x => x._name.StartsWith("camora")))
                {
                    var magazine = weaponDetails._props.Slots.FirstOrDefault(x => x._name.StartsWith("camora"));
                    cartridges.AddRange(magazine._props.filters[0].filter);
                }
                else
                {
                    // get default magazine, use the filter values from it
                    var defaultMagazineTpl = weaponDetails._props.defMagType;
                    var magazineDetails = itemLibrary.FirstOrDefault(x => x.Key == defaultMagazineTpl).Value;
                    cartridges.AddRange(magazineDetails._props.Cartridges[0]._props.filters[0].filter);
                }

                foreach (var cartridge in cartridges)
                {
                    if (result.ContainsKey(caliber))
                    {
                        result[caliber].AddUnique(cartridge, 1);
                    }
                    else
                    {
                        result[caliber] = new Dictionary<string, int>
                        {
                            { cartridge, 1 }
                        };
                    }
                }
            }

            return result;
    }

    private static List<WeaponDetails> CombinePrimaryAndSecondaryWeapons(List<WeaponDetails> flatPrimaryWeaponsList, List<WeaponDetails> flatSecondaryWeaponsList)
        {
            var result = new List<WeaponDetails>();
            result.AddRange(flatPrimaryWeaponsList);
            result.AddRange(flatSecondaryWeaponsList);

            return result;
        }

        private static void AddCartridgeItemToModListWithCompatibileCartridges(Dictionary<string, Dictionary<string, List<string>>> mods, List<string> compatibiltBullets, ModDetails mod)
        {
            var cartridges = new Dictionary<string, List<string>>
                        {
                            { "cartridges", compatibiltBullets }
                        };
            if (!mods.ContainsKey(mod.TemplateId))
            {
                mods.Add(mod.TemplateId, cartridges); // no item at all, create fresh
            }
            else
            {
                // Item exists, iterate over bullets and add if they dont exist
                foreach (var bullet in compatibiltBullets)
                {
                    mods[mod.TemplateId]["cartridges"].AddUnique(bullet);
                }
            }
        }

        /// <summary>
        /// Get a strongly typed dictionary of BSGs items library
        /// </summary>
        private static Dictionary<string, Item> GetItemLibrary()
        {
            CreateInputFolder(string.Empty);

            var workingPath = Directory.GetCurrentDirectory();

            var itemsLibraryJson = File.ReadAllText(workingPath + "\\Assets" + "\\items.json");
            return JsonConvert.DeserializeObject<Dictionary<string, Item>>(itemsLibraryJson);

        }

        /// <summary>
        /// Get combatible bullets for weapon that are not blacklisted
        /// </summary>
        private static List<string> GetCompatibileBullets(Dictionary<string, Item> itemLibrary, WeaponDetails weapon)
        {
            // Lookup weapon in itemdb
            var weaponInLibrary = itemLibrary[weapon.TemplateId];

            // Find the guns chamber and the bullets it can use
            var bullets = weaponInLibrary._props.Chambers.FirstOrDefault()?._props.filters[0]?.filter.ToList();

            // no bullets found, return the default bullet the gun can use
            if (bullets == null)
            {
                return new List<string>
                {
                    weaponInLibrary._props.defAmmo
                };
            }

            var nonBlacklistedBullets = new List<string>();
            foreach (var bullet in bullets)
            {
                //if (BulletHelpers.BulletIsOnBlackList(bullet))
                //{
                //    //continue;
                //}

                nonBlacklistedBullets.AddUnique(bullet);
            }

            return nonBlacklistedBullets;
        }

        /// <summary>
        /// Get a list of all the presets in the input/presets folder and return as a list of strongly typed objects
        /// </summary>
        private static List<Presets> GetPresets()
        {
            var presetPath = CreateInputFolder("presets");
            var presetFiles = GetPresetFileList(presetPath);

            var result = new List<Presets>();
            foreach (var presetFile in presetFiles)
            {
                var json = File.ReadAllText(presetFile);
                var parsedFile = JsonConvert.DeserializeObject<Presets>(json);
                result.Add(parsedFile);
            }

            int count = 0;
            foreach (var file in result)
            {
                count += file.weaponbuilds.Count;
            }
            LoggingHelpers.LogToConsole($"{count} presets parsed");

            return result;
        }

        private static List<ModDetails> GetModsFromRawFile(List<Presets> parsedPresets)
        {
            List<ModDetails> result = new List<ModDetails>();
            foreach (var file in parsedPresets)
            {
                foreach (var item in file.weaponbuilds)
                {
                    Weapon weapon = item.Value;

                    // Loop over weapons mods
                    foreach (var mod in weapon.items)
                    {
                        // Skip items with no parent (this is the weapon itself, first item in list)
                        if (mod.parentId == null)
                        {
                            continue;
                        }

                        Module parentMod = GetModsParent(file, mod.parentId);
                        if (parentMod != null)
                        {
                            result.Add(new ModDetails(mod.slotId, mod._id, mod._tpl, mod.parentId, parentMod._tpl));
                        }
                    }
                }
            }


            return result;
        }

        /// <summary>
        /// Find a mod where the supplied parentid equals the items id
        /// </summary>
        private static Module GetModsParent(Presets parsedPresets, string parentId)
        {
            foreach (var y in parsedPresets.weaponbuilds.Values)
            {
                var mod = y.items.Find(x => x._id == parentId);

                if (mod != null)
                {
                    return mod;
                }
            }

            return null;
        }

        private static List<WeaponDetails> GetWeaponsFromRawFile(List<Presets> parsedPresets)
        {
            var result = new List<WeaponDetails>();
            foreach (var file in parsedPresets)
            {
                foreach (var item in file.weaponbuilds)
                {
                    var itemBase = ItemTemplateHelper.GetTemplateById(item.Value.items[0]._tpl);
                    if (itemBase._props.weapUseType != "primary")
                    {
                        continue;
                    }

                    Weapon weapon = item.Value;
                    result.Add(new WeaponDetails(item.Key, weapon.items[0]._id, weapon.items[0]._tpl));
                }
            }

            return result;
        }

        private static List<WeaponDetails> GetSecondaryWeaponsFromRawFile(List<Presets> parsedPresets)
        {
            var result = new List<WeaponDetails>();
            foreach (var file in parsedPresets)
            {
                foreach (var item in file.weaponbuilds.Where(x => x.Key.Contains("pistol")))
                {
                    var itemBase = ItemTemplateHelper.GetTemplateById(item.Value.items[0]._tpl);
                    if (itemBase._props.weapUseType != "secondary")
                    {
                        continue;
                    }

                    Weapon weapon = item.Value;
                    result.Add(new WeaponDetails(item.Key, weapon.items[0]._id, weapon.items[0]._tpl));
                }
            }

            return result;
        }

        // Write json to a file
        private static void CreateJsonFile(string outputPath, string outputJson)
        {
            File.WriteAllText($"{outputPath}\\usec.json", outputJson);
        }

        /// <summary>
        /// Read json file names from preset folder
        /// </summary>
        /// <param name="presetPath">path to check for preset files</param>
        /// <returns></returns>
        private static List<string> GetPresetFileList(string presetPath)
        {
            var presetFiles = Directory.GetFiles(presetPath, "*.json", SearchOption.TopDirectoryOnly).ToList();
            LoggingHelpers.LogToConsole($"{presetFiles.Count} preset files found");

            return presetFiles;
        }

        /// <summary>
        /// Create folder structure to read from
        /// </summary>
        private static string CreateInputFolder(string folder)
        {
            var workingPath = Directory.GetCurrentDirectory();
            var presetPath = $"{workingPath}//input//{folder}";
            DiskHelpers.CreateDirIfDoesntExist(presetPath);

            return presetPath;
        }

        private static string CreateOutputFolder()
        {
            var workingPath = Directory.GetCurrentDirectory();
            var outputPath = $"{workingPath}\\output";
            DiskHelpers.CreateDirIfDoesntExist(outputPath);

            return outputPath;
        }
    }
}