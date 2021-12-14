namespace FileImporter.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Domain;
    using Domain.Wol;
    using Jil;

    public class OneLogPerFileParser : IParser
    {
        private readonly LogHelper _logHelper;

        public OneLogPerFileParser(LogHelper logHelper)
        {
            _logHelper = logHelper;
        }
        
        public PageData[] ParseFiles(string[] files)
        {
            var i = 0;
            IDictionary<int, PageData> dtos = new Dictionary<int, PageData>();
            PageData dto = new();

            bool parseHeaders = true;
            string file = files.First();
            long fileLength = new FileInfo(file).Length;
            long length = 0;
            using StreamReader streamReader = File.OpenText(file);

            while (!streamReader.EndOfStream)
            {
                string? line = streamReader.ReadLine();
                if (line is null || line.Length < 5)
                {
                    continue;
                }

                length += line.Length;
                LineType type;
                object data;
                try
                {
                    (LineType Type, object Data)? extractString = ExtractString(line, parseHeaders);
                    if (extractString is null)
                    {
                        continue;
                    }

                    type = extractString.Value.Type;
                    data = extractString.Value.Data;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLineAsync($"Error parsing '{line}': {e.Message}");
                    continue;
                }

                switch (type)
                {
                    case LineType.PageNumber:
                        dto.Page = (int) data;
                        break;
                    case LineType.Guild:
                        dto.Guild = (Guild) data;
                        break;
                    case LineType.Report:
                        dto.Report = (Report) data;
                        
                        // one report per file, Guild and Report can be skipped for th e rest of the file.
                        parseHeaders = false;
                        break;
                    case LineType.CombatLog:
                        dto.CombatLog = (CombatLog) data;
                        break;
                    case LineType.SimpleQuery:
                        dto.SimpleQuery = (SimpleQuery) data;

                        _logHelper.LogProgression(ref i, length, fileLength, 500);

                        dtos[dto.Page] = dto;
                        dto = new();
                        break;
                }
            }

            return dtos.Values.ToArray();
        }

        private enum LineType : byte
        {
            PageNumber = 0,
            Guild,
            TimeZone,
            Report,
            CombatLog,
            ResultTimeRanges,
            SimpleQuery
        }

        private static readonly IDictionary<string, LineType> LinesStart = new Dictionary<string, LineType>(7)
        {
            {"========== PAGE ", LineType.PageNumber},
            {"guild", LineType.Guild},
            {"tz", LineType.TimeZone},
            {"report", LineType.Report},
            {"combatLog", LineType.CombatLog},
            {"resultTimeRanges", LineType.ResultTimeRanges},
            {"simpleQuery", LineType.SimpleQuery},
        };

        private static (LineType Type, object Data)? ExtractString(ReadOnlySpan<char> content, bool parseHeaders)
        {
            foreach ((ReadOnlySpan<char> key, LineType value) in LinesStart)
            {
                if (content.Slice(0, key.Length).SequenceEqual(key))
                {
                    switch (value)
                    {
                        case LineType.PageNumber:
                            return (value, int.Parse(ExtractString(content, key, " =")));
                        case LineType.Guild when parseHeaders:
                            return (value, JSON.Deserialize<Guild>(content.Slice(key.Length + 3).ToString()));
                        case LineType.Report when parseHeaders:
                            return (value, JSON.Deserialize<Report>(content.Slice(key.Length + 3).ToString()));
                        case LineType.CombatLog:
                            return (value, JSON.Deserialize<CombatLog>(content.Slice(key.Length + 3).ToString()));
                        case LineType.SimpleQuery:
                            int contentLength = content.Length - 1 - (key.Length + 3);
                            return (value,
                                JSON.Deserialize<SimpleQuery>(content.Slice(key.Length + 3, contentLength).ToString()));
                        case LineType.TimeZone:
                        case LineType.ResultTimeRanges:
                        default:
                            return default;
                    }
                }
            }

            return default;
        }

        private static ReadOnlySpan<char> ExtractString(ReadOnlySpan<char> content, ReadOnlySpan<char> from,
            ReadOnlySpan<char> to)
        {
            for (int x = 0; x <= content.Length - from.Length; x++)
            {
                if (content.Slice(x, from.Length).SequenceEqual(from))
                {
                    ReadOnlySpan<char> result = content[(x + from.Length)..];
                    return result[..result.IndexOf(to)];
                }
            }

            return default;
        }
    }
}
