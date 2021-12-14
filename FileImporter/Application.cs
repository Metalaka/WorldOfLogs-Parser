using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data;
using Mdl.HostedConsoleApplication;
using Microsoft.Extensions.Configuration;

namespace FileImporter
{
    using Domain;
    using FileImporter.Parsers;
    using Microsoft.Extensions.DependencyInjection;

    public class Application : IHostedConsoleApplication
    {
        private readonly IServiceProvider _services;
        private readonly Mapper _mapper;
        private readonly DataAdapter _dataAdapter;
        private readonly DataContext _db;
        private readonly IConfiguration _configuration;

        public Application(Mapper mapper, DataContext db, DataAdapter dataAdapter,
            IConfiguration configuration, IServiceProvider services)
        {
            _mapper = mapper;
            _db = db;
            _dataAdapter = dataAdapter;
            _configuration = configuration;
            _services = services;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync("begin");

            Task loadTableTask = _dataAdapter.LoadGlobalsTablesAsync(cancellationToken);

            // Parse
            Queue<PageData> queue;
            try
            {
                var stopwatch = Stopwatch.StartNew();

                // todo: parametrize switch
                //IParser parser = _services.GetRequiredService<OnePagePerFileParser>();
                //string[] files = Directory.GetFiles(_configuration.GetValue<string>("path"), "*.html");
                IParser parser = _services.GetRequiredService<OneLogPerFileParser>();
                string[] files = {_configuration.GetValue<string>("path")};
                // var orderedFiles = GetFiles(@"C:\Users\Metalaka\Downloads\wow\wol\Mdl\halion\out");

                queue = new Queue<PageData>(parser.ParseFiles(files));

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
                string sid = queue.Peek().Report?.sid;

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

                _mapper.MapSynchronously(queue);

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
    }
}
