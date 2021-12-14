using System.Threading.Tasks;
using Data;
using Mdl.HostedConsoleApplication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FileImporter
{
    using FileImporter.Parsers;

    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true);
                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<LogHelper>();
                    services.AddTransient<OneLogPerFileParser>();
                    services.AddTransient<OnePagePerFileParser>();
                    services.AddTransient<Mapper>();
                    services.AddTransient<DataAdapter>();

                    services.AddDbContext<DataContext>(options => options.UseNpgsql(
                        hostContext.Configuration.GetConnectionString("DefaultConnection")
                    ));

                    services.AddAutoMapper(
                        cfg =>
                        {
                            cfg.AddProfile(typeof(AutoMapperProfile));
                            // cfg.AddCollectionMappers();
                            // cfg.UseEntityFrameworkCoreModel<DataContext>(services);
                        });
                })
                .ConfigureLogging((hostingContext, logging) => {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            await builder.RunConsoleApplicationAsync<Application>();
        }
    }
}