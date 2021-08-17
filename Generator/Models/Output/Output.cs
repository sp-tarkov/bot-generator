using Newtonsoft.Json;
using System.Collections.Generic;

namespace Generator.Models.Output
{
    public class Bot
    {
        public Bot(BotType botType)
        {
            this.botType = botType;
            appearance = new Appearance();
            experience = new Experience();
            health = new Health();
            skills = new Skills();
            inventory = new Inventory();
            firstName = new List<string>();
            lastName = new List<string>();
            difficulty = new Difficulty.Difficulty();
            chances = new Chances();
            generation = new GenerationChances();
        }

        [JsonIgnore]
        public BotType botType { get;set;}
        public Appearance appearance { get; set; }
        public Experience experience { get; set; }
        public Health health { get; set; }
        public Skills skills { get; set; }
        public Inventory inventory { get; set; }
        public List<string> firstName { get; set; }
        public List<string> lastName { get; set; }
        public Difficulty.Difficulty difficulty { get; set;}
        public Chances chances { get; set; }
        public GenerationChances generation { get; set; }
    }
    public class Appearance
    {
        public Appearance()
        {
            body = new List<string>();
            feet = new List<string>();
            hands = new List<string>();
            head = new List<string>();
            voice = new List<string>();
        }

        public List<string> body { get; set; }
        public List<string> feet { get; set; }
        public List<string> hands { get; set; }
        public List<string> head { get; set; }
        public List<string> voice { get; set; }
    }

    public class Experience
    {
        public Experience()
        {
            level = new MinMax(0, 1);
            reward = new MinMax(-1, -1);
            standingForKill = -0.02;
            aggressorBonus = 0.01;
        }

        public MinMax level { get; set; }
        public MinMax reward { get; set; }
        public double standingForKill { get; set; }
        public double aggressorBonus { get; set; }
    }

    public class BodyParts
    {
        public BodyParts()
        {
            Head = new MinMax(35, 35);
            Chest = new MinMax(85, 85);
            Stomach = new MinMax(70, 70);
            LeftArm = new MinMax(60, 60);
            RightArm = new MinMax(60, 60);
            LeftLeg = new MinMax(65, 65);
            RightLeg = new MinMax(65, 65);
        }

        public MinMax Head { get; set; }
        public MinMax Chest { get; set; }
        public MinMax Stomach { get; set; }
        public MinMax LeftArm { get; set; }
        public MinMax RightArm { get; set; }
        public MinMax LeftLeg { get; set; }
        public MinMax RightLeg { get; set; }
    }

    public class Health
    {
        public Health()
        {
            Hydration = new MinMax(100, 100);
            Energy = new MinMax(100, 100);
            Temperature = new MinMax(36, 40);
            BodyParts = new BodyParts();
        }

        public MinMax Hydration { get; set; }
        public MinMax Energy { get; set; }
        public MinMax Temperature { get; set; }
        public BodyParts BodyParts { get; set; }
    }

    public class Skills
    {
        public Skills()
        {
            Common = new Dictionary<string, MinMax>();
        }

        public Dictionary<string, MinMax> Common { get; set; }
    }


    public class Chances
    {
        public Chances()
        {
            equipment = new EquipmentChances();
        }

        public EquipmentChances equipment { get; set; }
        public Mods mods { get; set; }
    }

    public class EquipmentChances
    {
        public EquipmentChances()
        {
        }

        public EquipmentChances(int head, int ear, int faceCover,
            int vest, int eyewear, int armband, int tacVest,
            int backpack, int firstPrimary, int secondPrimary,
            int holster, int scabbard, int pockets, int securedContainer)
        {
            Headwear = head;
            Earpiece = ear;
            FaceCover = faceCover;
            ArmorVest = vest;
            ArmBand = armband;
            Eyewear = eyewear;
            TacticalVest = tacVest;
            Backpack = backpack;
            FirstPrimaryWeapon = firstPrimary;
            SecondPrimaryWeapon = secondPrimary;
            Holster = holster;
            Scabbard = scabbard;
            Pockets = pockets;
            SecuredContainer = securedContainer;
        }

        public int Headwear { get; set; }
        public int Earpiece { get; set; }
        public int FaceCover { get; set; }
        public int ArmorVest { get; set; }
        public int Eyewear { get; set; }
        public int ArmBand { get; set; }
        public int TacticalVest { get; set; }
        public int Backpack { get; set; }
        public int FirstPrimaryWeapon { get; set; }
        public int SecondPrimaryWeapon { get; set; }
        public int Holster { get; set; }
        public int Scabbard { get; set; }
        public int Pockets { get; set; }
        public int SecuredContainer { get; set; }
    }

    public class Mods
    {
        public Mods(int muzzle, int barrel, int handguard, int stock,
            int magazine, int mount, int flashlight, int tactical_001,
            int tactical_002, int tactical_003, int mount_000, int pistol_grip,
            int tactical, int scope, int reciever, int sight_rear,
            int charge, int mount_001, int equipment, int gas_block,
            int launcher, int sight_front, int stock_000, int foregrip,
            int tactical_000, int nvg, int pistol_grip_akms, int stock_akms,
            int equipment_000, int equipment_001, int equipment_002, int bipod)
        {
            mod_muzzle = muzzle;
            mod_barrel = barrel;
            mod_handguard = handguard;
            mod_stock = stock;
            mod_magazine = magazine;
            mod_mount = mount;
            mod_flashlight = flashlight;
            mod_tactical_001 = tactical_001;
            mod_tactical_002 = tactical_002;
            mod_tactical_003 = tactical_003;
            mod_mount_000 = mount_000;
            mod_pistol_grip = pistol_grip;
            mod_tactical = tactical;
            mod_scope = scope;
            mod_reciever = reciever;
            mod_sight_rear = sight_rear;
            mod_charge = charge;
            mod_mount_001 = mount_001;
            mod_equipment = equipment;
            mod_gas_block = gas_block;
            mod_launcher = launcher;
            mod_sight_front = sight_front;
            mod_stock_000 = stock_000;
            mod_foregrip = foregrip;
            mod_tactical_000 = tactical_000;
            mod_nvg = nvg;
            mod_pistol_grip_akms = pistol_grip_akms;
            mod_stock_akms = stock_akms;
            mod_equipment_000 = equipment_000;
            mod_equipment_001 = equipment_001;
            mod_equipment_002 = equipment_002;
            mod_bipod = bipod;
        }

        public int mod_muzzle { get; set; }
        public int mod_barrel { get; set; }
        public int mod_handguard { get; set; }
        public int mod_stock { get; set; }
        public int mod_magazine { get; set; }
        public int mod_mount { get; set; }
        public int mod_flashlight { get; set; }
        public int mod_tactical_001 { get; set; }
        public int mod_tactical_002 { get; set; }
        public int mod_tactical_003 { get; set; }
        public int mod_mount_000 { get; set; }
        public int mod_pistol_grip { get; set; }
        public int mod_tactical { get; set; }
        public int mod_scope { get; set; }
        public int mod_reciever { get; set; }
        public int mod_sight_rear { get; set; }
        public int mod_charge { get; set; }
        public int mod_mount_001 { get; set; }
        public int mod_equipment { get; set; }
        public int mod_gas_block { get; set; }
        public int mod_launcher { get; set; }
        public int mod_sight_front { get; set; }
        public int mod_stock_000 { get; set; }
        public int mod_foregrip { get; set; }
        public int mod_tactical_000 { get; set; }
        public int mod_nvg { get; set; }
        public int mod_pistol_grip_akms { get; set; }
        public int mod_stock_akms { get; set; }
        public int mod_equipment_000 { get; set; }
        public int mod_equipment_001 { get; set; }
        public int mod_equipment_002 { get; set; }
        public int mod_bipod { get; set; }
    }

    public class GenerationChances
    {
        public GenerationChances(int specialMin, int SpecialMax,
            int healingMin, int healingMax,
            int looseLootMin, int looseLootMax,
            int magazinesMin, int MagazineMax,
            int grenandesMin, int grenadesMax)
        {
            items = new ItemChances
            {
                specialItems = new MinMax(specialMin, SpecialMax),
                healing = new MinMax(healingMin, healingMax),
                looseLoot = new MinMax(looseLootMin, looseLootMax),
                magazines = new MinMax(magazinesMin, MagazineMax),
                grenades = new MinMax(grenandesMin, grenadesMax)
            };
        }
        
        public GenerationChances()
        {
            items = new ItemChances();
        }

        public ItemChances items { get; set; }
    }

    public class ItemChances
    {
        public ItemChances()
        {
            specialItems = new MinMax(0, 1);
            healing = new MinMax(1, 2);
            looseLoot = new MinMax(0, 3);
            magazines = new MinMax(2, 4);
            grenades = new MinMax(0, 5);
        }

        public MinMax specialItems { get; set; }
        public MinMax healing { get; set; }
        public MinMax looseLoot { get; set; }
        public MinMax magazines { get; set; }
        public MinMax grenades { get; set; }
    }

    public class MinMax
    {
        public MinMax(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public int min { get; set; }
        public int max { get; set; }
    }
}
