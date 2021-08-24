using Common;
using Common.Extensions;
using Newtonsoft.Json;
using PMCGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PMCGenerator
{
    
    class Program
    {
        static void Main(string[] args)
        {
            // take a list of preset files and spit out a drop-in-replacement mod list

            var presetPath = CreateInputFolder();

            var presetFiles = GetPresetFileList(presetPath);

            // Parse into list of strongly typed objects
            var parsedPresets = new Presets();
            foreach (var presetFile in presetFiles)
            {
                var json = File.ReadAllText(presetFile);
                parsedPresets = JsonConvert.DeserializeObject<Presets>(json);
            }

            // Create flat lists of weapons + list of mods
            var flatWeaponsList = GetWeaponsFromRawFile(parsedPresets);
            var flatModList = GetModsFromRawFile(parsedPresets);

            // Add weapon mods to output
            var output = new { FirstPrimaryWeapon = new List<string>(), Mods = new Dictionary<string, Dictionary<string, List<string>>>() };
            output.FirstPrimaryWeapon.AddRange(flatWeaponsList.Select(x => x.TemplateId).Distinct());
            var mods = output.Mods;
            foreach (var weapon in flatWeaponsList)
            {
                // add weapon if its not already here
                if (!mods.ContainsKey(weapon.TemplateId))
                {
                    // Add weapon to dictionary
                    mods.Add(weapon.TemplateId, new Dictionary<string, List<string>>());
                }

                // Get mods types for this gun, top level
                var uniqueModSlots = flatModList.Where(x => x.ParentId == weapon.Id).Select(x => x.SlotId).Distinct().ToList();
                foreach (var modSlotId in uniqueModSlots)
                {
                    Dictionary<string, List<string>> weaponModsToModify = mods[weapon.TemplateId];

                    if (!weaponModsToModify.ContainsKey(modSlotId))
                    {
                        weaponModsToModify.Add(modSlotId, new List<string>());
                    }
                }

                var modsForWeapon = flatModList.Where(x => x.ParentId == weapon.Id).ToList();
                Dictionary<string, List<string>> weaponMods = mods[weapon.TemplateId];

                foreach (var mod in modsForWeapon)
                {
                    weaponMods[mod.SlotId].AddUnique(mod.TemplateId);
                }
            }

            // Get mods where parent is not weapon and add to output
            foreach (var mod in flatModList.Where(x => x.ParentId != null 
                        && !flatWeaponsList.Any(y => y.Id == x.ParentId)).ToList())
            {
                // No parent tempalte id found, create and add mods details
                if (!mods.ContainsKey(mod.ParentTemplateId))
                {
                    var templateIdsList = new List<string>{mod.TemplateId};
                    var subtype = new Dictionary<string, List<string>>{{ mod.SlotId, templateIdsList } };
                    mods.Add(mod.ParentTemplateId, subtype);
                }

                //Add subtype to item
                var subtypeToAddTo = mods[mod.ParentTemplateId];
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

        private static List<ModDetails> GetModsFromRawFile(Presets parsedPresets)
        {
            List <ModDetails> result = new List<ModDetails>();
            foreach (var item in parsedPresets.weaponbuilds)
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

                    Module parentMod = GetModsParent(parsedPresets, mod.parentId);
                    result.Add(new ModDetails(mod.slotId, mod._id, mod._tpl, mod.parentId, parentMod._tpl));
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

        private static List<WeaponDetails> GetWeaponsFromRawFile(Presets parsedPresets)
        {
            var result = new List<WeaponDetails>();
            foreach (var item in parsedPresets.weaponbuilds)
            {
                Weapon weapon = item.Value;
                result.Add(new WeaponDetails(item.Key, weapon.items[0]._id, weapon.items[0]._tpl));
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
            Console.WriteLine($"{presetFiles.Count} preset files found");

            return presetFiles;
        }

        /// <summary>
        /// Create folder structure to read from
        /// </summary>
        private static string CreateInputFolder()
        {
            var workingPath = Directory.GetCurrentDirectory();
            var presetPath = $"{workingPath}//input//presets";
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
