using System.Collections.Generic;

namespace Common.Models
{
    public class Item
    {
        public string _id { get; set; }
        public string _name { get; set; }
        public string _parent { get; set; }
        public string _type { get; set; }
        public Props _props { get; set; }
    }

    public class Props
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public List<Chamber> Chambers { get; set; }
        public List<Slot> Slots { get; set; }
        public string defAmmo { get; set; }
    }

    public class Chamber
    {
        public string _name { get; set; }
        public string _id { get; set; }
        public string _parent { get; set; }
        public ChamberProps _props { get; set; }
        public bool _required { get; set; }
        public bool _mergeSlotWithChildren { get; set; }
        public string _proto { get; set; }
    }

    public class Slot
    {
        public string _name { get; set; }
        public bool _required { get; set; }
    }

    public class ChamberProps
    {
        public List<Filter> filters { get; set; }
    }

    public class Filter
    {
        public List<string> filter { get; set; }
    }
}
