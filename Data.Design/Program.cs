using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Data.Design
{
    /// <summary>
    /// Entrypoint of EntityFramework command line. (Sdk.Web endpoint)
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<DataContext>(options => options.UseNpgsql(
                        hostContext.Configuration.GetConnectionString("DefaultConnection")
                    ));
                });
    }
}