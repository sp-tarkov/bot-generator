using Generator.Helpers;
using Generator.Models.Input;
using Generator.Models.Output;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Generator
{
    internal class BotParser
    {
        private readonly string _workingPath;
        private readonly string _dumpPath;
        private readonly string[] _botTypes;

        public BotParser(string workingPath, string dumpPath, string[] botTypes)
        {
            _workingPath = workingPath;
            _dumpPath = dumpPath;
            _botTypes = botTypes;
        }

        public List<Datum> Parse()
        {
            var stopwatch = Stopwatch.StartNew();

            var failedFiles = 0;
            CreateDirIfDoesntExist(_dumpPath);

            var botFiles = Directory.GetFiles(_dumpPath, "*.json", SearchOption.TopDirectoryOnly).ToList();
            Console.WriteLine($"{botFiles.Count} files found");

            var parsedBots = new List<Datum>();
            foreach (var file in botFiles)
            {
                var splitFile = file.Split("\\");
                failedFiles++;

                var json = File.ReadAllText(file);
                try
                {
                    var bots = ParseJson(json, file);
                    Console.WriteLine($"parsing: {bots.Count} bots in file {splitFile.Last()}");
                    foreach (var bot in bots)
                    {
                        parsedBots.Add(bot);
                    } 
                }
                catch (JsonException jex)
                {
                    
                    Console.WriteLine($"JSON Error message: {jex.Message} || file: {splitFile.Last()}");
                    continue;
                }
            }

            stopwatch.Stop();

            Console.WriteLine($"Parsed: {parsedBots.Count} Failed: {failedFiles}. Took {LoggingHelpers.LogTimeTaken(stopwatch.Elapsed.TotalSeconds)} seconds");



            return parsedBots;
        }

        private void CreateDirIfDoesntExist(string path)
        {
            if (!Directory.Exists($"{path}"))
            {
                //create dump dir
                Directory.CreateDirectory($"{path}");
            }
        }

        private static List<Datum> ParseJson(string json, string file)
        {
            //Console.WriteLine($"parsing file {file}");
            var serialisedObject = JsonConvert.DeserializeObject<Models.Input.Root>(json);
            return serialisedObject.data;
        }
    }
}