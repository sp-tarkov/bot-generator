using Common.Models;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Add a string to a list only if it doesnt already exist
        /// </summary>
        public static bool IsBoss(this BotType self)
        {
            return self.HasFlag(BotType.bossbully | BotType.bossgluhar | BotType.bosskilla | BotType.bosskojaniy | BotType.bosssanitar);
        }
    }
}
