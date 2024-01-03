using Common.Models.Input;
using System.Text.Json;

namespace Generator.Helpers.Gear
{
    public class ForcedLootHelper
    {
        static readonly JsonSerializerOptions serialiserOptions = new() { };
        private static Dictionary<string, ForcedLoot> forcedLoot;

        public static Dictionary<string, ForcedLoot> GetForcedLoot()
        {
            if (forcedLoot == null)
            {
                var workingPath = Directory.GetCurrentDirectory();
                var assetPath = $"{workingPath}//assets";
                var forcedLootFile = Directory.GetFiles(assetPath, "forcedLoot.json", SearchOption.TopDirectoryOnly).FirstOrDefault();
                var parsedLoot = ParseJson(File.ReadAllText(forcedLootFile));
                forcedLoot = parsedLoot;
            }

            return forcedLoot;
        }

        private static Dictionary<string, ForcedLoot> ParseJson(string json)
        {
            var deSerialisedObject = JsonSerializer.Deserialize<Dictionary<string, ForcedLoot>>(json, serialiserOptions);
            return deSerialisedObject;
        }
    }
}
