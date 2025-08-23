using SPTarkov.Server.Core.Models.Eft.Bot.GlobalSettings;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using System.Collections.Generic;
using System.Text.Json;

namespace Common.Models.Output.Difficulty
{
    public class Difficulty
    {
        public DifficultyCategories easy { get; set; }
        public DifficultyCategories normal { get; set; }
        public DifficultyCategories hard { get; set; }
        public DifficultyCategories impossible { get; set; }
    }
}
