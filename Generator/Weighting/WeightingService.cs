using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Generator.Weighting
{

    public class Weightings
    {
        public Dictionary<string, Dictionary<string, int>> Equipment { get; set; }

        // Ammo type + (dict of ammo + weight)
        public Dictionary <string, Dictionary<string, int>> Ammo { get; set; }
    }
    
    public class WeightingService
    {
        private readonly Dictionary<BotType, Weightings> _weights;
        public WeightingService()
        {

            var weightsFilePath = $"{Directory.GetCurrentDirectory()}\\Assets\\weights.json";
            if (!File.Exists(weightsFilePath))
            {
                throw new Exception($"Missing weights.json in /assets ({weightsFilePath})");
            }

            var weightJson = File.ReadAllText(weightsFilePath);
            _weights = JsonSerializer.Deserialize<Dictionary<BotType, Weightings>>(weightJson);
        }

        public int GetAmmoWeight(string tpl, BotType botType, string caliber)
        {
            if (_weights.ContainsKey(botType))
            {
               var botWeights = _weights[botType];

                if (botWeights.Ammo.ContainsKey(caliber))
                {
                    var calibers = botWeights.Ammo[caliber];

                    if (calibers.ContainsKey(tpl))
                    {
                        return calibers[tpl];
                    }
                    
                }
            }

            return 1;
        }

        public int GetItemWeight(string tpl, BotType botType, string slot)
        {
            if (_weights.ContainsKey(botType))
            {
                var botItemList = _weights[botType];

                if (botItemList.Equipment.Keys.Contains(slot, StringComparer.CurrentCultureIgnoreCase))
                {
                    var slotWeights = botItemList.Equipment.FirstOrDefault(x => x.Key.ToLower() == slot).Value;
                    if (slotWeights.Keys.Contains(tpl, StringComparer.CurrentCultureIgnoreCase))
                    {
                        var itemWeight = slotWeights[tpl];
                        return itemWeight;
                    }
                }
            }

            return 1;
        }
    }
}
