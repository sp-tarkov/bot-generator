using System.Collections.Generic;

namespace Common.Models.Output
{
    public class Equipment
    {
        public Equipment()
        {
            Headwear = new Dictionary<string, int>();
            Earpiece = new Dictionary<string, int>();
            FaceCover = new Dictionary<string, int>();
            ArmorVest = new Dictionary<string, int>();
            Eyewear = new Dictionary<string, int>();
            ArmBand = new Dictionary<string, int>();
            TacticalVest = new Dictionary<string, int>();
            Backpack = new Dictionary<string, int>();
            FirstPrimaryWeapon = new Dictionary<string, int>();
            SecondPrimaryWeapon = new Dictionary<string, int>();
            Holster = new Dictionary<string, int>();
            Scabbard = new Dictionary<string, int>();
            Pockets = new Dictionary<string, int>();
            SecuredContainer = new Dictionary<string, int>();
        }

        public Dictionary<string, int> Headwear { get; set; }
        public Dictionary<string, int> Earpiece { get; set; }
        public Dictionary<string, int> FaceCover { get; set; }
        public Dictionary<string, int> ArmorVest { get; set; }
        public Dictionary<string, int> Eyewear { get; set; }
        public Dictionary<string, int> ArmBand { get; set; }
        public Dictionary<string, int> TacticalVest { get; set; }
        public Dictionary<string, int> Backpack { get; set; }
        public Dictionary<string, int> FirstPrimaryWeapon { get; set; }
        public Dictionary<string, int> SecondPrimaryWeapon { get; set; }
        public Dictionary<string, int> Holster { get; set; }
        public Dictionary<string, int> Scabbard { get; set; }
        public Dictionary<string, int> Pockets { get; set; }
        public Dictionary<string, int> SecuredContainer { get; set; }
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
