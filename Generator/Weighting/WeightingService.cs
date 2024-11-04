using Common.Models;
using System.Collections.Generic;
using System.Text.Json;

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
        private static Dictionary<BotType, Weightings> _weights = null;
        private static Dictionary<string, Dictionary<string, GenerationWeightData>> _generationWeights = null;

        public WeightingService()
        {
            // Cache the loaded  data
            if (_weights != null && _generationWeights != null) return;

            var assetsPath = $"{Directory.GetCurrentDirectory()}\\Assets";
            var weightsFilePath = $"{assetsPath}\\weights.json";
            if (!File.Exists(weightsFilePath))
            {
                throw new Exception($"Missing weights.json in /assets ({weightsFilePath})");
            }

            var weightJson = File.ReadAllText(weightsFilePath);
            _weights = JsonSerializer.Deserialize<Dictionary<BotType, Weightings>>(weightJson);

            // bot / itemtype / itemcount
            var generationWeightJson = File.ReadAllText($"{assetsPath}\\generationWeights.json");

            // assault - dict
            // speicalitems - object
            // weights + whitelsit
            _generationWeights = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, GenerationWeightData>>>(generationWeightJson);
        }

        public int GetAmmoWeight(string tpl, BotType botType, string caliber)
        {
            if (_weights.ContainsKey(botType))
            {
               var botWeights = _weights[botType];
                if (botWeights.Ammo == null)
                {
                    return 1;
                }

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

        public Dictionary<string, GenerationWeightData> GetBotGenerationWeights(BotType botType)
        {
            _generationWeights.TryGetValue(botType.ToString(), out var result);
            if (result == null)
            {
                return _generationWeights["default"];
            }

            return result;
        }
    }
}
