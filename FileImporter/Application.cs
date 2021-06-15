using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data;
using Domain.Wol;
using Mdl.HostedConsoleApplication;
using Microsoft.Extensions.Configuration;

namespace FileImporter
{
    public class Application : IHostedConsoleApplication
    {
        private readonly Parser _parser;
        private readonly Mapper _mapper;
        private readonly DataAdapter _dataAdapter;
        private readonly DataContext _db;
        private readonly IConfiguration _configuration;

        public Application(Parser parser, Mapper mapper, DataContext db, DataAdapter dataAdapter, IConfiguration configuration)
        {
            _parser = parser;
            _mapper = mapper;
            _db = db;
            _dataAdapter = dataAdapter;
            _configuration = configuration;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync("begin");

            Task loadTableTask = _dataAdapter.LoadGlobalsTablesAsync(cancellationToken);
            
            // Parse
            Queue<(Guild, Report, CombatLog, SimpleQuery)> queue;
            try
            {
                var stopwatch = Stopwatch.StartNew();

                string[] files = Directory.GetFiles(_configuration.GetValue<string>("path"), "*.html");
                // var orderedFiles = GetFiles(@"C:\Users\Metalaka\Downloads\wow\wol\Mdl\halion\out");

                queue = new Queue<(Guild, Report, CombatLog, SimpleQuery)>(ParseFilesAsParallel(files));

                stopwatch.Stop();
#pragma warning disable 4014
                Console.Out.WriteLineAsync($"parse time: {stopwatch.Elapsed}");
#pragma warning restore 4014
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

            try
            {
                var sid = queue.Peek().Item2.sid;

                if (await _dataAdapter.ReportExists(sid))
                {
                    await Console.Out.WriteLineAsync($"Report {sid} was already imported, exiting.");
                    return;
                }
            }
            finally
            {
                await loadTableTask;
            }

            // Mapping
            try
            {
                var stopwatch = Stopwatch.StartNew();

                MapSynchronously(queue);
                        
                stopwatch.Stop();
#pragma warning disable 4014
                Console.Out.WriteLineAsync($"map time: {stopwatch.Elapsed}");
#pragma warning restore 4014
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            // Save to db
            try
            {
                var stopwatch = Stopwatch.StartNew();
                
                await _db.SaveChangesAsync(cancellationToken);
                
                await Console.Out.WriteLineAsync($"save globals time: {stopwatch.Elapsed}");

                await _dataAdapter.Save(_mapper.GetData().ToArray());
                
                stopwatch.Stop();
                await Console.Out.WriteLineAsync($"total save time: {stopwatch.Elapsed}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return;
        }

        /// <summary>
        /// get files ordered by creation time
        /// </summary>
        private static string[] GetFiles(string path)
        {
            return new DirectoryInfo(path)
                .GetFileSystemInfos()
                .Where(f => f.Extension == ".html")
                .OrderBy(f => f.CreationTime)
                .Select(f => f.FullName)
                .ToArray();
        }

        private (Guild, Report, CombatLog, SimpleQuery)[] ParseFilesAsParallel(string[] files)
        {
            bool hasError = false;
            var i = 0;
            var length = files.Length;
            var dtos = new (Guild, Report, CombatLog, SimpleQuery)[length];

            Parallel.ForEach(files /*.Take(10)*/, file =>
            {
                LogProgression(ref i, length, 500);
                
                try
                {
                    var (page, tuple) = _parser.Run(file, 2400);
                    dtos[page] = tuple;
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

        private (Guild, Report, CombatLog, SimpleQuery)[] ParseFilesSynchronously(string[] files)
        {
            var i = 0;
            var length = files.Length;
            var dtos = new (Guild, Report, CombatLog, SimpleQuery)[length];
            foreach (var file in files)
            {
                LogProgression(ref i, length, 500);
                
                try
                {
                    var (page, tuple) = _parser.Run(file, 2400);
                    dtos[page] = tuple;
                }
                catch (Exception e)
                {
                    Console.Error.WriteLineAsync($"Error parsing {file}: {e.Message}");
                }
            }
    
            return dtos;
        }

        private void MapSynchronously(Queue<(Guild, Report, CombatLog, SimpleQuery)> queue)
        {
            var i = 0;
            var length = queue.Count;
            
            while (queue.Count > 0)
            {
                LogProgression(ref i, length, 500);
                
                try
                {
                    var (guild, report, combatLog, simpleQuery) = queue.Dequeue();

                    if (guild is null)
                    {
                        continue;
                    }
                    
                    _mapper.Map(guild, report, combatLog, simpleQuery);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        public static void LogProgression(ref int i, int length, int step)
        {
            if (++i % step == 0)
            {
                Console.Out.WriteLineAsync($"{i}/{length} {(i*100/length):D}%");
            }
        }
    }
}