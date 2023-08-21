using System.Collections.Generic;

namespace Common.Models
{
    public class GenerationWeightData
    {
        public Dictionary<string, int> weights { get; set; }
        public List<string> whitelist { get; set; }
    }
}
