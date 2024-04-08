using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Common.Models.Output;

[JsonSerializable(typeof(Bot))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Serialization, WriteIndented = true)]
public partial class BotJsonContext : JsonSerializerContext
{
}

public class Bot
{
    public Bot()
    {
    }

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
    public BotType botType { get; set; }
    public Appearance appearance { get; set; }
    public Experience experience { get; set; }
    public Health health { get; set; }
    public Skills skills { get; set; }
    public Inventory inventory { get; set; }
    public List<string> firstName { get; set; }
    public List<string> lastName { get; set; }
    public Difficulty.Difficulty difficulty { get; set; }
    public Chances chances { get; set; }
    public GenerationChances generation { get; set; }
}
public class Appearance
{
    public Appearance()
    {
        body = new Dictionary<string, int>();
        feet = new Dictionary<string, int>();
        hands = new Dictionary<string, int>();
        head = new Dictionary<string, int>();
        voice = new Dictionary<string, int>();
    }

    public Dictionary<string, int> body { get; set; }
    public Dictionary<string, int> feet { get; set; }
    public Dictionary<string, int> hands { get; set; }
    public Dictionary<string, int> head { get; set; }
    public Dictionary<string, int> voice { get; set; }
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
        BodyParts = new List<BodyParts>();
    }

    public MinMax Hydration { get; set; }
    public MinMax Energy { get; set; }
    public MinMax Temperature { get; set; }
    public List<BodyParts> BodyParts { get; set; }
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
    public Dictionary<string, int> weaponMods { get; set; }
    public Dictionary<string, int> equipmentMods { get; set; }
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

public class GenerationChances
{
    public GenerationChances(
        GenerationWeightData specialItems,
        GenerationWeightData healingItems,
        GenerationWeightData drugItems,
        GenerationWeightData stimItems,
        GenerationWeightData foodItems,
        GenerationWeightData drinkItems,
        GenerationWeightData backpackLootItems,
        GenerationWeightData pocketLootItems,
        GenerationWeightData vestLootItems,
        GenerationWeightData magazineItems,
        GenerationWeightData grenadeItems)
    {
        items = new ItemChances
        {
            specialItems = specialItems,
            healing = healingItems,
            drugs = drugItems,
            stims = stimItems,
            food = foodItems,
            drink = drinkItems,
            backpackLoot = backpackLootItems,
            pocketLoot = pocketLootItems,
            vestLoot = vestLootItems,
            magazines = magazineItems,
            grenades = grenadeItems
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
        specialItems = new GenerationWeightData(); // 0,1
        healing = new GenerationWeightData(); // 1, 2
        drugs = new GenerationWeightData(); // 0, 1
        stims = new GenerationWeightData(); // 0, 1
        backpackLoot = new GenerationWeightData(); //0,3
        pocketLoot = new GenerationWeightData();
        vestLoot = new GenerationWeightData();
        magazines = new GenerationWeightData(); //2,4
        grenades = new GenerationWeightData(); //0,5
    }

    public GenerationWeightData specialItems { get; set; }
    public GenerationWeightData healing { get; set; }
    public GenerationWeightData drugs { get; set; }
    public GenerationWeightData stims { get; set; }
    public GenerationWeightData food { get; set; }
    public GenerationWeightData drink { get; set; }
    public GenerationWeightData backpackLoot { get; set; }
    public GenerationWeightData pocketLoot { get; set; }
    public GenerationWeightData vestLoot { get; set; }
    public GenerationWeightData magazines { get; set; }
    public GenerationWeightData grenades { get; set; }
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
