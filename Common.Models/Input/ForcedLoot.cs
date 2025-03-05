namespace Common.Models.Input
{
    public record ForcedLoot
    {
        public string[] Backpack { get; set; }
        public string[] Pockets { get; set; }
        public string[] TacticalVest { get; set; }
    }
}