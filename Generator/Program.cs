using System.IO;

namespace Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: pass these into functions to act as whitelist
            string[] botTypes = { "assault", "marksman", "pmcbot", "bossbully", "bosskilla" };

            // Read raw bot dumps from live and turn into c# objects
            var workingPath = Directory.GetCurrentDirectory();
            var dumpPath = $"{workingPath}//dumps";
            var botParser = new BotParser(dumpPath, botTypes);
            var parsedBots = botParser.Parse();

            if (parsedBots.Count == 0)
            {
                Helpers.LoggingHelpers.LogToConsole("no bots found, unable to continue");
                Helpers.LoggingHelpers.LogToConsole("Check your dumps are in 'Generator\\bin\\Debug\\netcoreapp3.1\\dumps' and start with 'resp.' NOT 'req.'");
                return;
            }

            // Generate the base bot class and add basic details (health/body part hp etc)
            var baseBotGenerator = new BaseBotGenerator(parsedBots, workingPath);
            var baseBots = baseBotGenerator.AddBaseDetails();

            // Add weapons/armor to bot
            var botGearGenerator = new BotGearGenerator(baseBots, parsedBots);
            var botsWithGear = botGearGenerator.AddGear();

            // Add loot to bot
            var botLootGenerator = new BotLootGenerator(botsWithGear, parsedBots);
            var botsWithGearAndLoot = botLootGenerator.AddLoot();

            // Output bot to json file
            var jsonWriter = new JsonWriter(workingPath, "output");
            jsonWriter.WriteJson(botsWithGearAndLoot);
        }
    }
}
