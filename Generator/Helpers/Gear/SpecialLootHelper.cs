using Generator.Models;
using System.Collections.Generic;

namespace Generator.Helpers.Gear
{
    public static class SpecialLootHelper
    {
        public static IEnumerable<string> GetSpecialLootForBotType(BotType botType)
        {
            var results = new List<string>();
            switch (botType)
            {
                case BotType.assault:
                    break;
                case BotType.pmcBot:
                    break;
                case BotType.marksman:
                    break;
                case BotType.bossbully:
                    break;
                case BotType.bossgluhar:
                    break;
                case BotType.bosskilla:
                    break;
                case BotType.bosskojaniy:
                    results.Add("5d08d21286f774736e7c94c3"); // Shturman's stash key
                    break;
                case BotType.bosssanitar:
                    results.Add("5efde6b4f5448336730dbd61"); // Keycard with a blue marking
                    break;
                case BotType.bossstormtrooper:
                    break;
                case BotType.followerbully:
                    break;
                case BotType.followergluharassault:
                    break;
                case BotType.followergluharscout:
                    break;
                case BotType.followergluharsecurity:
                    break;
                case BotType.followergluharsnipe:
                    break;
                case BotType.followerkojaniy:
                    break;
                case BotType.followersanitar:
                    break;
                case BotType.followerstormtrooper:
                    break;
                case BotType.cursedassault:
                    break;
                case BotType.sectantpriest:
                    break;
                case BotType.sectantwarrior:
                    break;
                case BotType.usec:
                    break;
                default:
                    break;
            }

            return results;
        }
    }
}
