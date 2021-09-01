using Common;
using Common.Extensions;
using Newtonsoft.Json;
using PMCGenerator.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Models;

namespace PMCGenerator
{
    
    class Program
    {
        static void Main(string[] args)
        {
            var itemLibrary = GetItemLibrary();

            var parsedPresets = GetPresets();

            // Create flat lists of weapons + list of mods
            var flatPrimaryWeaponsList = GetWeaponsFromRawFile(parsedPresets);
            var flatSecondaryWeaponsList = GetSecondaryWeaponsFromRawFile(parsedPresets);
            var flatModList = GetModsFromRawFile(parsedPresets);

            // Add weapon mods to output
            var output = new {
                FirstPrimaryWeapon = new List<string>(),
                Holster = new List<string>(),
                mods = new Dictionary<string, Dictionary<string, List<string>>>() };
            output.FirstPrimaryWeapon.AddRange(flatPrimaryWeaponsList.Select(x => x.TemplateId).Distinct());
            output.Holster.AddRange(flatSecondaryWeaponsList.Select(x => x.TemplateId).Distinct());

            foreach (var weapon in flatPrimaryWeaponsList)
            {
                // add weapon if its not already here
                if (!output.mods.ContainsKey(weapon.TemplateId))
                {
                    // Add weapon to dictionary
                    output.mods.Add(weapon.TemplateId, new Dictionary<string, List<string>>());
                }

                // Get mods types for this gun, top level
                var uniqueModSlots = flatModList.Where(x => x.ParentId == weapon.Id).Select(x => x.SlotId).Distinct().ToList();
                var chamberedBulletModItemName = "patron_in_weapon";
                uniqueModSlots.AddUnique(chamberedBulletModItemName);
                foreach (var modSlotId in uniqueModSlots)
                {
                    Dictionary<string, List<string>> weaponModsToModify = output.mods[weapon.TemplateId];

                    if (!weaponModsToModify.ContainsKey(modSlotId))
                    {
                        weaponModsToModify.Add(modSlotId, new List<string>());
                    }
                }

                // Add compatible bullets to weapons gun chamber
                var modItemToAddBulletsTo = output.mods[weapon.TemplateId].FirstOrDefault(x=> x.Key == chamberedBulletModItemName);

                foreach (var bullet in GetCompatibileBullets(itemLibrary, weapon))
                {
                    if (BulletHelpers.BulletIsOnBlackList(bullet))
                    {
                        continue;
                    }

                    modItemToAddBulletsTo.Value.AddUnique(bullet);
                }
                
                // Add compatabible mods to weapon
                var modsForWeapon = flatModList.Where(x => x.ParentId == weapon.Id).ToList();
                Dictionary<string, List<string>> weaponMods = output.mods[weapon.TemplateId];
                foreach (var mod in modsForWeapon)
                {
                    weaponMods[mod.SlotId].AddUnique(mod.TemplateId);
                }
            }

            // Get mods where parent is not weapon and add to output
            foreach (var mod in flatModList.Where(x => x.ParentId != null 
                        && !flatPrimaryWeaponsList.Any(y => y.Id == x.ParentId)).ToList())
            {
                // No parent tempalte id found, create and add mods details
                if (!output.mods.ContainsKey(mod.ParentTemplateId))
                {
                    var templateIdsList = new List<string>{mod.TemplateId};
                    var subtype = new Dictionary<string, List<string>>{{ mod.SlotId, templateIdsList } };
                    output.mods.Add(mod.ParentTemplateId, subtype);
                }

                //Add subtype to item
                var subtypeToAddTo = output.mods[mod.ParentTemplateId];
                // No subtype, add it
                if (!subtypeToAddTo.ContainsKey(mod.SlotId))
                {
                    var valueToAdd = new List<string>(){ mod.TemplateId };
                    subtypeToAddTo.Add(mod.SlotId, valueToAdd);
                }

                // subtype exists, add to it
                subtypeToAddTo[mod.SlotId].AddUnique(mod.TemplateId);
            }

            // Create output dir
            var outputPath = CreateOutputFolder();

            // Turn into json
            var outputJson = JsonConvert.SerializeObject(output, Formatting.Indented);

            CreateJsonFile(outputPath, outputJson);
        }

        /// <summary>
        /// Get a strongly typed dictionary of BSGs items library
        /// </summary>
        private static Dictionary<string, Item> GetItemLibrary()
        {
            CreateInputFolder(string.Empty);

            var workingPath = Directory.GetCurrentDirectory();

            var itemsLibraryJson = File.ReadAllText(workingPath + "\\input" + "\\items.json");
            return JsonConvert.DeserializeObject<Dictionary<string, Item>>(itemsLibraryJson);

        }

        private static List<string> GetCompatibileBullets(Dictionary<string, Item> itemLibrary, WeaponDetails weapon)
        {
            // Lookup weapon in itemdb
            var weaponInLibrary = itemLibrary[weapon.TemplateId];

            // Find the guns chamber and the bullets it can use
            var bullets = weaponInLibrary._props.Chambers.FirstOrDefault()?._props.filters[0]?.filter.ToList();

            // return bullets or return default ammo type
            return bullets ?? new List<string>
                {
                    weaponInLibrary._props.defAmmo
                };
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
            List <ModDetails> result = new List<ModDetails>();
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
                foreach (var item in file.weaponbuilds.Where(x=> !x.Key.Contains("pistol")))
                {
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