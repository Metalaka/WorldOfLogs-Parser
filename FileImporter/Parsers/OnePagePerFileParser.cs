namespace FileImporter.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Json;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Wol;
    using Jil;

    public class OnePagePerFileParser : IParser
    {
        private readonly LogHelper _logHelper;

        public OnePagePerFileParser(LogHelper logHelper)
        {
            _logHelper = logHelper;
        }

        public PageData[] ParseFiles(string[] files)
        {
            return ParseFilesAsParallel(files);
        }
        
        private PageData[] ParseFilesAsParallel(string[] files)
        {
            bool hasError = false;
            int i = 0;
            int length = files.Length;
            PageData[] dtos = new PageData[length];

            Parallel.ForEach(files /*.Take(10)*/, file =>
            {
                _logHelper.LogProgression(ref i, length, 500);

                try
                {
                    PageData dto = Run(file, 2400);
                    dtos[dto.Page] = dto;
                }
                catch (Exception e)
                {
                    hasError = true;
                    Console.Error.WriteLineAsync($"Error parsing {file}: {e.Message}");
                }
            });

            if (hasError)
            {
                Console.Out.WriteLine($"HasError: Exiting...");

                throw new ApplicationException();
            }

            return dtos;
        }

        private PageData[] ParseFilesSynchronously(string[] files)
        {
            var i = 0;
            var length = files.Length;
            var dtos = new PageData[length];
            foreach (var file in files)
            {
                _logHelper.LogProgression(ref i, length, 500);

                try
                {
                    var dto = Run(file, 2400);
                    dtos[dto.Page] = dto;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLineAsync($"Error parsing {file}: {e.Message}");
                }
            }

            return dtos;
        }

        private static PageData Run(string path, int startOffset, bool checkJson = false)
        {
            var content = File.ReadAllText(path).AsSpan(startOffset);

            var guildJson = ExtractString(content, "guild =", "\n");
            // string timeZoneJson = ExtractString(content, "tz =", "\n");
            var reportJson = ExtractString(content, "report =", "\n");
            var combatLogJson = ExtractString(content, "combatLog =", "\n");
            // todo: timeRange feature require to extract names of ranges and offset from html
            // string timeRangeJson = ExtractString(content, "resultTimeRanges =", "\n");
            var simpleQueryJson = ExtractString(content, "simpleQuery =", ";\n");
            var page = int.Parse(ExtractString(simpleQueryJson, "page\":", "}"));

            if (checkJson)
            {
                // checks fields of events entries because I'm not currently sur of the completeness of my list
                var eventProperties = typeof(Event).GetProperties()
                    .Select(p => p.Name.ToLowerInvariant())
                    .ToArray();
                CheckJson(combatLogJson.ToString(), 5, "events", eventProperties);
            }

            // Newtonsoft
            // var guild = JsonConvert.DeserializeObject<Guild>(guildJson.ToString());
            // var report = JsonConvert.DeserializeObject<Report>(reportJson.ToString());
            // var combatLog = JsonConvert.DeserializeObject<CombatLog>(combatLogJson.ToString());


            // System.Text.Json
            /*var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            var guild = JsonSerializer.Deserialize<Guild>(guildJson.ToString(), options);
            var report = JsonSerializer.Deserialize<Report>(reportJson.ToString(), options);
            var combatLog = JsonSerializer.Deserialize<CombatLog>(combatLogJson.ToString(), options);
            var simpleQuery = JsonConvert.DeserializeObject<SimpleQuery>(simpleQueryJson.ToString(), options);
            /**/

            // jil

            PageData dto = new();
            dto.Page = page;
            dto.Guild = JSON.Deserialize<Guild>(guildJson.ToString());
            dto.Report = JSON.Deserialize<Report>(reportJson.ToString());
            dto.CombatLog = JSON.Deserialize<CombatLog>(combatLogJson.ToString());
            dto.SimpleQuery = JSON.Deserialize<SimpleQuery>(simpleQueryJson.ToString());


            return dto;
        }

        private static ReadOnlySpan<char> ExtractString(ReadOnlySpan<char> content, ReadOnlySpan<char> from,
            ReadOnlySpan<char> to)
        {
            for (int x = 0; x <= content.Length - from.Length; x++)
            {
                if (content.Slice(x, from.Length).SequenceEqual(from))
                {
                    var result = content[(x + from.Length)..];
                    return result[..result.IndexOf(to)];
                }
            }

            return default;
        }

        private static void CheckJson(string json, int length, string key, IEnumerable<string> array)
        {
            JsonValue value = JsonValue.Parse(json);

            if (value.Count != length)
            {
                throw new ApplicationException("object isn't the right size");
            }

            var jsonValue = value[key];

            if (jsonValue is null)
            {
                throw new ApplicationException("key doesn't exists");
            }

            var keys = new List<string>();

            foreach (JsonValue o in jsonValue)
            {
                if (o.JsonType != JsonType.Object)
                {
                    throw new ApplicationException("a member of the array isn't an object");
                }

                JsonObject jo = (JsonObject) o;

                keys.AddRange(jo.Keys /*.Where(_ => !keys.Contains(_))*/);
            }

            var a = keys.Distinct().Select(_ => _.ToLowerInvariant()).Distinct()
                .Where(_ => !array.Contains(_)).ToArray();

            if (a.Any())
            {
                throw new ApplicationException($"unhandled event fields: {string.Join(", ", a)}");
            }
        }
    }
}
