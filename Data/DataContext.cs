using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataContext : DbContext
    {
#pragma warning disable 8618
        public DataContext()
            : base()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<School>()
                .Property(_ => _.Id)
                .ValueGeneratedNever()
                ;

            modelBuilder.Entity<Spell>()
                .Property(_ => _.Id)
                .ValueGeneratedNever()
                ;
            modelBuilder.Entity<Spell>()
                .HasMany(_ => _.Schools)
                .WithMany(_ => _.Spells)
                ;

            // Guild
            
            modelBuilder.Entity<Report>()
                .HasIndex(_ => _.Sid)
                .IsUnique()
                ;
            
            modelBuilder.Entity<Actor>()
                .HasKey(_ => new {_.ReportId, _.Id})
                ;
            
            modelBuilder.Entity<FlaggedActor>()
                .HasKey(_ => new {_.ReportId, _.Id})
                ;
            modelBuilder.Entity<FlaggedActor>()
                .HasOne(_ => _.Actor)
                .WithMany(_ => _.FlaggedActors)
                .HasForeignKey(_ => new {_.ReportId, _.ActorId})
                .IsRequired()
                ;

            modelBuilder.Entity<Event>()
                .HasKey(_ => new {_.ReportId, _.Id})
                ;
            
            modelBuilder.Entity<Line>()
                .HasKey(_ => new {_.ReportId, _.Id})
                ;
            modelBuilder.Entity<Line>()
                .HasOne(_ => _.SourceFlaggedActor)
                .WithMany(_ => _.LinesSource)
                .HasForeignKey(_ => new {_.ReportId, _.SourceFlaggedActorId})
                ;
            modelBuilder.Entity<Line>()
                .HasOne(_ => _.TargetFlaggedActor)
                .WithMany(_ => _.LinesTarget)
                .HasForeignKey(_ => new {_.ReportId, _.TargetFlaggedActorId})
                ;
            modelBuilder.Entity<Line>()
                .HasOne(_ => _.Event)
                .WithMany(_ => _.Lines)
                .HasForeignKey(_ => new {_.ReportId, _.EventId})
                .IsRequired()
                ;
        }

        public DbSet<Spell> Spells { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<FlaggedActor> FlaggedActors { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Line> Lines { get; set; }

#pragma warning restore 8618
    }
}