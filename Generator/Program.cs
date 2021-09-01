using Common;
using System.IO;
using System.Linq;

namespace Generator
{
    internal static class Program
    {
        internal static void Main(string[] args)
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

            // Generate the base bot class with basic details (health/body part hp etc) and then append everything else
            var bots = BaseBotGenerator.GenerateBaseDetails(parsedBots, workingPath, botTypes)
                .AddGear(parsedBots) // Add weapons/armor
                .AddLoot(parsedBots)
                .AddChances(parsedBots); // Add mod/equipment chances

            // Output bot to json file
            var jsonWriter = new JsonWriter(workingPath, "output");
            jsonWriter.WriteJson(bots.ToList());
        }
    }
}
