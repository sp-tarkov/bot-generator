using Common;
using System.IO;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create list of bots we want to process
            string[] botTypes = {
                "assault",
                "marksman",
                "pmcBot",
                "bossbully",
                "bossgluhar",
                "bosskilla",
                "bosskojaniy",
                "bosssanitar",
                "bosstagilla",
                //"bossstormtrooper",

                "followerbully",
                "followergluharassault",
                "followergluharscout",
                "followergluharsecurity",
                //"followergluharsnipe",
                "followerkojaniy",
                "followersanitar",
                //"followerstormtrooper",

                "cursedassault",

                "sectantpriest",
                "sectantwarrior",
            };

            // Read raw bot dumps and turn into c# objects
            var workingPath = Directory.GetCurrentDirectory();
            var dumpPath = $"{workingPath}//dumps";
            var botParser = new BotParser(dumpPath);
            var parsedBots = botParser.Parse();

            if (parsedBots.Count == 0)
            {
                LoggingHelpers.LogToConsole("no bots found, unable to continue");
                LoggingHelpers.LogToConsole("Check your dumps are in 'Generator\\bin\\Debug\\netcoreapp3.1\\dumps' and start with 'resp.' NOT 'req.'");
                return;
            }

            // Generate the base bot class and add basic details (health/body part hp etc)
            var baseBotGenerator = new BaseBotGenerator(parsedBots, workingPath, botTypes);
            var baseBots = baseBotGenerator.AddBaseDetails();

            // Add weapons/armor to bots
            var botGearGenerator = new BotGearGenerator(baseBots, parsedBots);
            var botsWithGear = botGearGenerator.AddGear();

            // Add loot to bots
            var botLootGenerator = new BotLootGenerator(botsWithGear, parsedBots);
            var botsWithGearAndLoot = botLootGenerator.AddLoot();

            // Add mod/equipment chances
            var botChancesGenerator = new BotChancesGenerator(botsWithGearAndLoot, parsedBots);
            var botsWithGearAndLootAndChances = botChancesGenerator.AddChances();

            // Output bot to json file
            var jsonWriter = new JsonWriter(workingPath, "output");
            jsonWriter.WriteJson(botsWithGearAndLootAndChances);
        }
    }
}
