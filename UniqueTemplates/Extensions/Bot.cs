using Common.Models.Input;
using System.Collections.Generic;
using System.Linq;

namespace UniqueTemplates.Extensions
{
    public static class BotExtensions
    {
        public static bool ContainsBot(this List<Datum> list, Datum botToCheck)
        {
            foreach (var bot in list)
            {
                var botGear = GetEquippedGear(bot);
                if (botGear.CheckMatch(botToCheck.GetEquippedGear()))
                {
                    return true;
                }
            }

            return false;
        }

        public static EquippedGear GetEquippedGear(this Datum bot)
        {
            return new EquippedGear
            {
                ArmorVest = GeEquipmentItemTemplateIdOrNull(bot.Inventory.items, "ArmorVest"),
                Backpack = GeEquipmentItemTemplateIdOrNull(bot.Inventory.items, "Backpack"),
                Eyewear = GeEquipmentItemTemplateIdOrNull(bot.Inventory.items, "Eyewear"),
                FaceCover = GeEquipmentItemTemplateIdOrNull(bot.Inventory.items, "FaceCover"),
                FirstPrimaryWeapon = GeEquipmentItemTemplateIdOrNull(bot.Inventory.items, "FirstPrimaryWeapon"),
                Headwear = GeEquipmentItemTemplateIdOrNull(bot.Inventory.items, "Headwear"),
                Scabbard = GeEquipmentItemTemplateIdOrNull(bot.Inventory.items, "Scabbard"),
                TacticalVest = GeEquipmentItemTemplateIdOrNull(bot.Inventory.items, "TacticalVest"),
            };
        }

        private static string GeEquipmentItemTemplateIdOrNull(List<Item> inventoryItems, string itemTypeWanted)
        {
            var item = inventoryItems.FirstOrDefault(x => x.slotId == itemTypeWanted);
            if (item == null)
            {
                return null;
            }

            return item._tpl;
        }

        public class EquippedGear
        {
            public string FirstPrimaryWeapon { get; set; }
            public string TacticalVest { get; set; }
            public string Headwear { get; set; }
            public string Scabbard { get; set; }
            public string Backpack { get; set; }
            public string ArmorVest { get; set; }
            public string FaceCover { get; set; }
            public string Eyewear { get; set; }

            public bool CheckMatch(EquippedGear gearToCheck)
            {
                if (gearToCheck.FirstPrimaryWeapon == FirstPrimaryWeapon
                    && gearToCheck.TacticalVest == TacticalVest
                    && gearToCheck.Headwear == Headwear
                    && gearToCheck.Scabbard == Scabbard
                    && gearToCheck.Backpack == Backpack
                    && gearToCheck.ArmorVest == ArmorVest
                    && gearToCheck.FaceCover == FaceCover
                    && gearToCheck.Eyewear == Eyewear)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
