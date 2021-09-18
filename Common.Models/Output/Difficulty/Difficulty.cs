using System.Collections.Generic;

namespace Common.Models.Output.Difficulty
{
    public class Difficulty
    {
        public DifficultySettings easy { get; set; }
        public DifficultySettings normal { get; set; }
        public DifficultySettings hard { get; set; }
        public DifficultySettings impossible { get; set; }
    }

    public class DifficultySettings
    {
        public Dictionary<string, object> Lay { get; set; }
        public Dictionary<string, object> Aiming { get; set; }
        public Dictionary<string, object> Look { get; set; }
        public Dictionary<string, object> Shoot { get; set; }
        public Dictionary<string, object> Move { get; set; }
        public Dictionary<string, object> Grenade { get; set; }
        public Dictionary<string, object> Change { get; set; }
        public Dictionary<string, object> Cover { get; set; }
        public Dictionary<string, object> Patrol { get; set; }
        public Dictionary<string, object> Hearing { get; set; }
        public Dictionary<string, object> Mind { get; set; }
        public Dictionary<string, object> Boss { get; set; }
        public Dictionary<string, object> Core { get; set; }
        public Dictionary<string, object> Scattering { get; set; }
    }
}
