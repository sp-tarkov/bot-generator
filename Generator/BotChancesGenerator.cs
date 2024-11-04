using Generator.Helpers.Gear;
using Common.Models.Input;
using Common.Models.Output;
using System.Diagnostics;
using Generator.Weighting;

namespace Generator
{
    public static class BotChancesGenerator
    {
        public static void AddChances(Bot botToUpdate, Datum rawBot)
        {
            var weightHelper = new WeightingService();

            // TODO: Add check to make sure incoming bot list has gear
            GearChanceHelpers.AddEquipmentChances(botToUpdate, rawBot);
            GearChanceHelpers.AddGenerationChances(botToUpdate, weightHelper);
            GearChanceHelpers.AddModChances(botToUpdate, rawBot);
            GearChanceHelpers.AddEquipmentModChances(botToUpdate, rawBot);
        }
    }
}
