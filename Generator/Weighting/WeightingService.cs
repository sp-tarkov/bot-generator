using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Generator.Weighting
{
    
    public class WeightingService
    {
        private readonly Dictionary<BotType, Dictionary<string, Dictionary<string, int>>> _weights;
        public WeightingService()
        {

            var itemsFilePath = $"{Directory.GetCurrentDirectory()}\\Assets\\weights.json";
            if (!File.Exists(itemsFilePath))
            {
                throw new Exception($"Missing weights.json in /assets ({itemsFilePath})");
            }

            var itemsJson = File.ReadAllText(itemsFilePath);
            _weights = JsonSerializer.Deserialize<Dictionary<BotType, Dictionary<string, Dictionary<string, int>>>>(itemsJson);
        }

        public int GetItemWeight(string tpl, BotType botType, string slot)
        {
            if (_weights.ContainsKey(botType))
            {
                var botWeights = _weights[botType];
                if (botWeights.Keys.Contains(slot, StringComparer.CurrentCultureIgnoreCase))
                {
                    var slotWeights = botWeights.FirstOrDefault(x => x.Key.ToLower() == slot).Value;
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
