using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FileImporter
{
    public class DataAdapter
    {
        private readonly DataContext _db;

        public DataAdapter(DataContext db)
        {
            _db = db;
        }

        public async Task Save(Report[] reports)
        {
            var connection = (NpgsqlConnection) _db.Database.GetDbConnection();
            try
            {
                // Save with EF: reports
                await _db.Reports.AddRangeAsync(reports);
                await _db.SaveChangesAsync();
                
                // with Bulk import: actors, flaggedActors, events, lines
                await connection.OpenAsync();

                foreach (var report in reports)
                {
                    await NpgsqlBinaryImporterUtils.Save(report.Actors.Values, "public.\"Actors\"", NpgsqlBinaryImporterUtils.WriteActor, connection);
                    await NpgsqlBinaryImporterUtils.Save(report.FlaggedActors.Values, "public.\"FlaggedActors\"", NpgsqlBinaryImporterUtils.WriteFlaggedActor, connection);
                    await NpgsqlBinaryImporterUtils.Save(report.Events.Values, "public.\"Events\"", NpgsqlBinaryImporterUtils.WriteEvent, connection, 10_000);
                    
                    try
                    {
                        await NpgsqlBinaryImporterUtils.Save(report.Lines, "public.\"Lines\"", NpgsqlBinaryImporterUtils.WriteLine, connection, 50_000);
                    }
                    catch (NpgsqlException e) when (e.InnerException is not null && e.InnerException.GetType() == typeof(TimeoutException))
                    {
                        // Timeout can occur when sending 1M lines...
                        Console.WriteLine($"Timeout on Complete call: {e.Message}");
                    }
                }
            }
            finally
            {
                await connection.CloseAsync();
            }
            
            return;
        }
        
        public async Task LoadGlobalsTablesAsync(CancellationToken cancellationToken)
        {
            await _db.Guilds.LoadAsync(cancellationToken);
            await _db.Schools.LoadAsync(cancellationToken);
            await _db.Spells.LoadAsync(cancellationToken);
            
            await Console.Out.WriteLineAsync($"Globals tables loaded");
        }

        public async Task<bool> ReportExists(string sid)
        {
            return await _db.Reports.AnyAsync(_ => _.Sid == sid);
        }
    }
    
    public static class NpgsqlBinaryImporterExtensions
    {
        public static void WriteNullable<T>(this NpgsqlBinaryImporter _, Nullable<T> v)
        where T : struct
        {
            if (v.HasValue)
            {
                _.Write(v.Value);
                
                return;
            }
            
            _.WriteNull();
        }

    }
    
    public static class NpgsqlBinaryImporterUtils
    {
        public static void WriteActor(Actor entry, NpgsqlBinaryImporter writer)
        {
            writer.Write<int>(entry.Id);
            writer.Write<long>(entry.Report.Id);
            writer.Write<string>(entry.Name);
            writer.Write<string>(entry.Clazz);
            writer.Write<long>(entry.Guid);
            writer.Write<string>(entry.Type);
        }
        public static void WriteFlaggedActor(FlaggedActor entry, NpgsqlBinaryImporter writer)
        {
            writer.Write<int>(entry.Id);
            writer.Write<long>(entry.Report.Id);
            writer.WriteNullable<int>(entry.Actor?.Id);
            writer.WriteNullable<int>(entry.Flags);
        }
        public static void WriteEvent(Event entry, NpgsqlBinaryImporter writer)
        {
            writer.Write<int>(entry.Id);
            writer.Write<long>(entry.Report.Id);
            writer.Write<int>(entry.PowerType);
            writer.WriteNullable<int>(entry.Spell?.Id);
            writer.Write<int>(entry.SubType);
            writer.Write<int>(entry.Type);
            writer.Write<int>(entry.School);
            writer.WriteNullable<int>(entry.Amount);
            writer.WriteNullable<int>(entry.Flags);
            writer.WriteNullable<int>(entry.MissAmount);
            writer.WriteNullable<int>(entry.Blocked);
            writer.WriteNullable<int>(entry.Overheal);
            writer.WriteNullable<int>(entry.Overkill);
            writer.WriteNullable<int>(entry.Absorbed);
            writer.WriteNullable<int>(entry.Resisted);
            writer.WriteNullable<int>(entry.MissType);
            writer.WriteNullable<int>(entry.EnvironmentType);
            writer.WriteNullable<int>(entry.ExtraSpell?.Id);
        }
        public static void WriteLine(Line entry, NpgsqlBinaryImporter writer)
        {
            writer.Write<int>(entry.Id);
            writer.Write<long>(entry.Report.Id);
            writer.Write<long>(entry.Timestamp);
            writer.WriteNullable<int>(entry.SourceFlaggedActor?.Id);
            writer.WriteNullable<int>(entry.TargetFlaggedActor?.Id);
            writer.Write<int>(entry.Event.Id);
        }
        
        public static async Task Save<T>(ICollection<T> entries, string table, Action<T, NpgsqlBinaryImporter> converter, NpgsqlConnection dbConnection, int step = 500)
        {
            var i = 0;
            var length = entries.Count;
            await Console.Out.WriteLineAsync($"Saving {length} lines to {table}...");
 
            await using NpgsqlBinaryImporter writer = dbConnection.BeginBinaryImport($"COPY {table} FROM STDIN (FORMAT BINARY)");
            writer.Timeout = TimeSpan.FromMinutes(1);

            foreach (var line in entries)
            {
                Application.LogProgression(ref i, length, step);
                
                await writer.StartRowAsync();
                converter(line, writer);
            }
            
            var n = await writer.CompleteAsync();
            await Console.Out.WriteLineAsync($"{n} entries completed");
        }

    }
}