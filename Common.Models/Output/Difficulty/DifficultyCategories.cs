using Common.Models.Output.GlobalSettings;

namespace Common.Models.Output.Difficulty;

public record DifficultyCategories
{
    public required BotGlobalAimingSettings Aiming { get; set; }

    public required BotGlobalsBossSettings Boss { get; set; }

    public required BotGlobalsChangeSettings Change { get; set; }

    public required BotGlobalCoreSettings Core { get; set; }

    public required BotGlobalsCoverSettings Cover { get; set; }

    public required BotGlobalsGrenadeSettings Grenade { get; set; }

    public required BotGlobalsHearingSettings Hearing { get; set; }

    public required BotGlobalLayData Lay { get; set; }

    public required BotGlobalLookData Look { get; set; }

    public required BotGlobalsMindSettings Mind { get; set; }

    public required BotGlobalsMoveSettings Move { get; set; }

    public required BotGlobalPatrolSettings Patrol { get; set; }

    public required BotGlobalsScatteringSettings Scattering { get; set; }

    public required BotGlobalShootData Shoot { get; set; }
}
