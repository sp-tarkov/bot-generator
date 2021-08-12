using System;
using System.Collections.Generic;

namespace Generator
{
    public static class StringToolsExtensions
    {
        public static void AddUnique(this IList<string> self, string item)
        {
            if (!self.Contains(item))
                self.Add(item);
        }
    }
}
