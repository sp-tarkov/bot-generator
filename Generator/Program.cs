using Common.Models.Input;
using Generator.Helpers;

namespace Generator;

internal static class Program
{
    internal static async Task Main(string[] args)
    {
        // Create list of bots we want to process
        string[] botTypes = {
        "assault",
        "assaultgroup",
        "marksman",
        "pmcbot",
        "exusec",

        "bossbully",
        "bossgluhar",
        "bosskilla",
        "bosskojaniy",
        "bosssanitar",
        "bosstagilla",
        "bossknight",
        "bosszryachiy",
        "bossboar",
        "bossboarsniper",
        "bosskolontay",
        "bosspartisan",

        "followerbully",
        "followergluharassault",
        "followergluharscout",
        "followergluharsecurity",
        "followergluharsnipe",
        "followerkojaniy",
        "followersanitar",
        "followerstormtrooper",
        "followerbirdeye",
        "followerbigpipe",
        "followerzryachiy",
        "followerboar",
        "followerboarclose1",
        "followerboarclose2",
        "followerkolontayassault",
        "followerkolontaysecurity",

        "ravangezryachiyevent",
        "peacefullzryachiyevent",
        "cursedassault",
        "sectantpriest",
        "sectantwarrior",
        "gifter",
        "arenafighterevent",
        "crazyassaultevent",

        "shooterbtr",

        "spiritspring",
        "spiritwinter",

        "skier",
        "peacemaker",

        "sectantpredvestnik",
        "sectantprizrak",
        "sectantoni",

        "infectedassault",
        "infectedpmc",
        "infectedcivil",
        "infectedlaborant",
        "infectedtagilla",
       };

        // Read raw bot dumps and turn into c# objects
        var workingPath = Directory.GetCurrentDirectory();
        var dumpPath = $"{workingPath}//dumps";
        var parsedBots = await BotParser.ParseAsync(dumpPath, botTypes.ToHashSet());

        // Put in dictionary for better use later on
        var rawBotsCache = new Dictionary<string, List<Datum>>(45);
        foreach (var rawBot in parsedBots)
        {
            if (rawBotsCache.TryGetValue(rawBot.Info.Settings.Role.ToLower(), out var botList))
            {
                botList.Add(rawBot);

                continue;
            }

            // Doesnt exist, add key and bot
            rawBotsCache.Add(rawBot.Info.Settings.Role.ToLower(), new List<Datum> { rawBot });
        }

        if (parsedBots.Count == 0)
        {
            LoggingHelpers.LogToConsole("No bots found, unable to continue");
            LoggingHelpers.LogToConsole("Check your dumps are in 'Generator\\bin\\Debug\\net6.0\\dumps' and start with 'resp.' NOT 'req.'");
            return;
        }

        // Generate the base bot class with basic details (health/body part hp etc) and then append everything else
        var bots = BaseBotGenerator.GenerateBaseDetails(parsedBots, workingPath, botTypes)
                                .AddGear(rawBotsCache) // Add weapons/armor
                                .AddLoot(rawBotsCache)
                                .AddChances(rawBotsCache); // Add mod/equipment chances

        // Output bot to json file
        var jsonWriter = new JsonWriter(workingPath, "output");
        jsonWriter.WriteJson(bots.ToList());
    }
}
