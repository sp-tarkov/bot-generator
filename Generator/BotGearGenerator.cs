using Common.Models.Input;
using Common.Models.Output;
using Generator.Helpers.Gear;
using System.Diagnostics;

namespace Generator
{
    public static class BotGearGenerator
    {
        public static void AddGear(Bot botToUpdate, Datum rawBotData)
        {
            GearHelpers.AddEquippedGear(botToUpdate, rawBotData);
            GearHelpers.AddAmmo(botToUpdate, rawBotData);
            GearHelpers.AddEquippedMods(botToUpdate, rawBotData);
            //GearHelpers.AddCartridges(botToUpdate, rawBotData);
        }
    }
}
