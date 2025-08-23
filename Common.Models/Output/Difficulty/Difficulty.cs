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

    public class DifficultySettings
    {
        public Dictionary<string, BotGlobalLayData> Lay { get; set; }
        public Dictionary<string, BotGlobalAimingSettings> Aiming { get; set; }
        public Dictionary<string, BotGlobalLookData> Look { get; set; }
        public Dictionary<string, BotGlobalShootData> Shoot { get; set; }
        public Dictionary<string, BotGlobalsMoveSettings> Move { get; set; }
        public Dictionary<string, BotGlobalsGrenadeSettings> Grenade { get; set; }
        public Dictionary<string, BotGlobalsChangeSettings> Change { get; set; }
        public Dictionary<string, BotGlobalsCoverSettings> Cover { get; set; }
        public Dictionary<string, BotGlobalPatrolSettings> Patrol { get; set; }
        public Dictionary<string, BotGlobalsHearingSettings> Hearing { get; set; }
        public Dictionary<string, object> Mind { get; set; }
        public Dictionary<string, object> Boss { get; set; }
        public Dictionary<string, BotGlobalCoreSettings> Core { get; set; }
        public Dictionary<string, object> Scattering { get; set; }
    }
}
