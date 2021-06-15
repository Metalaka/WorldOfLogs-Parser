using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Json;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Domain.Wol;
using Jil;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FileImporter
{
    public class Parser
    {
        public (int, (Guild, Report, CombatLog, SimpleQuery)) Run(string path, int startOffset, bool checkJson = false)
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
            var guild = JSON.Deserialize<Guild>(guildJson.ToString());
            var report = JSON.Deserialize<Report>(reportJson.ToString());
            var combatLog = JSON.Deserialize<CombatLog>(combatLogJson.ToString());
            var simpleQuery = JSON.Deserialize<SimpleQuery>(simpleQueryJson.ToString());


            return (page, (guild, report, combatLog, simpleQuery));
        }
        
        private static ReadOnlySpan<char> ExtractString(ReadOnlySpan<char> content, ReadOnlySpan<char> from, ReadOnlySpan<char> to)
        {
            for (int x = 0; x <= content.Length - from.Length; x++)
            {
                if (content.Slice(x, from.Length).SequenceEqual(from))
                {
                    var result = content[(x+from.Length)..];
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

                JsonObject jo = (JsonObject)o;
                
                keys.AddRange(jo.Keys/*.Where(_ => !keys.Contains(_))*/);
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