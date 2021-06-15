using System.Linq;

namespace Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            if (context.Guilds.Any())
            {
                return; // DB has been seeded
            }

            context.SaveChanges();
        }
    }
}