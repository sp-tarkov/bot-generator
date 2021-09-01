using System.Collections.Generic;

namespace Common.Extensions
{
    public static class StringToolsExtensions
    {
        /// <summary>
        /// Add a string to a list only if it doesnt already exist
        /// </summary>
        public static void AddUnique(this IList<string> self, string item)
        {
            if (!self.Contains(item))
                self.Add(item);
        }

        public static void AddUniqueRange(this IList<string> self, IList<string> itemsToAdd)
        {
            foreach (var item in itemsToAdd)
            {
                if (!self.Contains(item))
                    self.Add(item);
            }
        }
    }
}
