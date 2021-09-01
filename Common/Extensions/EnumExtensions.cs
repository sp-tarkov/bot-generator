using Common.Models;
using System.Collections.Generic;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        private static readonly List<BotType> bossTypes = new List<BotType>(){
            BotType.bossbully,
            BotType.bossgluhar,
            BotType.bosskilla,
            BotType.bosskojaniy,
            BotType.bosssanitar,
            BotType.bosstagilla
            };

        public static bool IsBoss(this BotType self)
        {
            return bossTypes.Contains(self);
        }
    }
}
