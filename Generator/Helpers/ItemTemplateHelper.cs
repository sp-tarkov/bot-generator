using System;
using System.Collections.Generic;
using System.IO;
using Common;
using Newtonsoft.Json;
using Item = Common.Models.Item;

namespace Generator.Helpers
{
    public static class ItemTemplateHelper
    {
        private static Dictionary<string, Item> _itemCache;

        public static Dictionary<string, Item> Items
        {
            get
            {
                if (_itemCache == null)
                {
                    var itemsFilePath = $"{Directory.GetCurrentDirectory()}\\Assets\\items.json";
                    if (!File.Exists(itemsFilePath))
                    {
                        throw new Exception($"Missing items.json under assets ({itemsFilePath})");
                    }

                    var itemsJson = File.ReadAllText(itemsFilePath);
                    _itemCache = JsonConvert.DeserializeObject<Dictionary<string, Item>>(itemsJson);
                }

                return _itemCache;
            }
        }

        public static Item GetTemplateById(string templateId)
        {
            if (Items.ContainsKey(templateId))
            {
                return Items[templateId];
            }

            LoggingHelpers.LogToConsole($"Could not locate item template with id {templateId}", ConsoleColor.Red);
            return null;
        }
    }
}