using System.Collections.Generic;

namespace Generator.Models.Output
{
    public class Equipment
    {
        public Equipment()
        {
            Headwear = new List<string>();
            Earpiece = new List<string>();
            FaceCover = new List<string>();
            ArmorVest = new List<string>();
            Eyewear = new List<string>();
            ArmBand = new List<string>();
            TacticalVest = new List<string>();
            Backpack = new List<string>();
            FirstPrimaryWeapon = new List<string>();
            SecondPrimaryWeapon = new List<string>();
            Holster = new List<string>();
            Scabbard = new List<string>();
            Pockets = new List<string>();
            SecuredContainer = new List<string>();
        }

        public List<string> Headwear { get; set; }
        public List<string> Earpiece { get; set; }
        public List<string> FaceCover { get; set; }
        public List<string> ArmorVest { get; set; }
        public List<string> Eyewear { get; set; }
        public List<string> ArmBand { get; set; }
        public List<string> TacticalVest { get; set; }
        public List<string> Backpack { get; set; }
        public List<string> FirstPrimaryWeapon { get; set; }
        public List<string> SecondPrimaryWeapon { get; set; }
        public List<string> Holster { get; set; }
        public List<string> Scabbard { get; set; }
        public List<string> Pockets { get; set; }
        public List<string> SecuredContainer { get; set; }
    }

    public class Inventory
    {
        public Inventory()
        {
            equipment = new Equipment();
            items = new Items();
            mods = new Dictionary<string, Dictionary<string, List<string>>>();
        }

        public Equipment equipment { get; set; }
        public Dictionary<string, Dictionary<string, List<string>>> mods { get; set; }
        public Items items { get; set; }
    }

    public class Items
    {
        public Items()
        {
            TacticalVest = new List<string>();
            Pockets = new List<string>();
            Backpack = new List<string>();
            SecuredContainer = new List<string>();
            SpecialLoot = new List<string>();
        }

        public List<string> TacticalVest { get; set; }
        public List<string> Pockets { get; set; }
        public List<string> Backpack { get; set; }
        public List<string> SecuredContainer { get; set; }
        public List<string> SpecialLoot { get; set; }
    }
}
