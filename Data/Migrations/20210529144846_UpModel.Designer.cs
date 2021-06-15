﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210529144846_UpModel")]
    partial class UpModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Domain.Entities.Actor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ActorId")
                        .HasColumnType("integer");

                    b.Property<string>("Clazz")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Guid")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("ReportId")
                        .HasColumnType("bigint");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ReportId", "ActorId")
                        .IsUnique();

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("Domain.Entities.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Absorbed")
                        .HasColumnType("integer");

                    b.Property<int?>("Amount")
                        .HasColumnType("integer");

                    b.Property<int?>("Blocked")
                        .HasColumnType("integer");

                    b.Property<int?>("EnvironmentType")
                        .HasColumnType("integer");

                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<int?>("ExtraSpellId")
                        .HasColumnType("integer");

                    b.Property<int?>("Flags")
                        .HasColumnType("integer");

                    b.Property<int?>("MissAmount")
                        .HasColumnType("integer");

                    b.Property<int?>("MissType")
                        .HasColumnType("integer");

                    b.Property<int?>("Overheal")
                        .HasColumnType("integer");

                    b.Property<int?>("Overkill")
                        .HasColumnType("integer");

                    b.Property<int>("PowerType")
                        .HasColumnType("integer");

                    b.Property<long>("ReportId")
                        .HasColumnType("bigint");

                    b.Property<int?>("Resisted")
                        .HasColumnType("integer");

                    b.Property<int>("School")
                        .HasColumnType("integer");

                    b.Property<int?>("SpellId")
                        .HasColumnType("integer");

                    b.Property<int>("SubType")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ExtraSpellId");

                    b.HasIndex("SpellId");

                    b.HasIndex("ReportId", "EventId")
                        .IsUnique();

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Domain.Entities.FlaggedActor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ActorId")
                        .HasColumnType("bigint");

                    b.Property<int>("FlaggedActorId")
                        .HasColumnType("integer");

                    b.Property<int?>("Flags")
                        .HasColumnType("integer");

                    b.Property<long>("ReportId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("ReportId", "FlaggedActorId")
                        .IsUnique();

                    b.ToTable("FlaggedActor");
                });

            modelBuilder.Entity("Domain.Entities.Guild", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Faction")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Realm")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Timezone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Guilds");
                });

            modelBuilder.Entity("Domain.Entities.Line", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("EventId")
                        .HasColumnType("bigint");

                    b.Property<int>("LineId")
                        .HasColumnType("integer");

                    b.Property<long>("ReportId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SourceFlaggedActorId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TargetFlaggedActorId")
                        .HasColumnType("bigint");

                    b.Property<long>("Timestamp")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("SourceFlaggedActorId");

                    b.HasIndex("TargetFlaggedActorId");

                    b.HasIndex("ReportId", "LineId")
                        .IsUnique();

                    b.ToTable("Line");
                });

            modelBuilder.Entity("Domain.Entities.Report", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("E")
                        .HasColumnType("bigint");

                    b.Property<long>("EndTime")
                        .HasColumnType("bigint");

                    b.Property<long>("ExportDate")
                        .HasColumnType("bigint");

                    b.Property<long?>("GuildId")
                        .HasColumnType("bigint");

                    b.Property<bool>("HasForbiddenBuffs")
                        .HasColumnType("boolean");

                    b.Property<long>("S")
                        .HasColumnType("bigint");

                    b.Property<string>("Sid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("StartTime")
                        .HasColumnType("bigint");

                    b.Property<long?>("TimeInfoE")
                        .HasColumnType("bigint");

                    b.Property<long?>("TimeInfoS")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.HasIndex("Sid")
                        .IsUnique();

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Domain.Entities.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("Domain.Entities.Spell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Spells");
                });

            modelBuilder.Entity("SchoolSpell", b =>
                {
                    b.Property<int>("SchoolsId")
                        .HasColumnType("integer");

                    b.Property<int>("SpellsId")
                        .HasColumnType("integer");

                    b.HasKey("SchoolsId", "SpellsId");

                    b.HasIndex("SpellsId");

                    b.ToTable("SchoolSpell");
                });

            modelBuilder.Entity("Domain.Entities.Actor", b =>
                {
                    b.HasOne("Domain.Entities.Report", "Report")
                        .WithMany()
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Domain.Entities.Event", b =>
                {
                    b.HasOne("Domain.Entities.Spell", "ExtraSpell")
                        .WithMany()
                        .HasForeignKey("ExtraSpellId");

                    b.HasOne("Domain.Entities.Report", "Report")
                        .WithMany()
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Spell", "Spell")
                        .WithMany()
                        .HasForeignKey("SpellId");

                    b.Navigation("ExtraSpell");

                    b.Navigation("Report");

                    b.Navigation("Spell");
                });

            modelBuilder.Entity("Domain.Entities.FlaggedActor", b =>
                {
                    b.HasOne("Domain.Entities.Actor", "Actor")
                        .WithMany("FlaggedActors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Report", "Report")
                        .WithMany()
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("Domain.Entities.Line", b =>
                {
                    b.HasOne("Domain.Entities.Event", "Event")
                        .WithMany("Lines")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Report", "Report")
                        .WithMany("Lines")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.FlaggedActor", "SourceFlaggedActor")
                        .WithMany("LinesSource")
                        .HasForeignKey("SourceFlaggedActorId");

                    b.HasOne("Domain.Entities.FlaggedActor", "TargetFlaggedActor")
                        .WithMany("LinesTarget")
                        .HasForeignKey("TargetFlaggedActorId");

                    b.Navigation("Event");

                    b.Navigation("Report");

                    b.Navigation("SourceFlaggedActor");

                    b.Navigation("TargetFlaggedActor");
                });

            modelBuilder.Entity("Domain.Entities.Report", b =>
                {
                    b.HasOne("Domain.Entities.Guild", "Guild")
                        .WithMany("Reports")
                        .HasForeignKey("GuildId");

                    b.Navigation("Guild");
                });

            modelBuilder.Entity("SchoolSpell", b =>
                {
                    b.HasOne("Domain.Entities.School", null)
                        .WithMany()
                        .HasForeignKey("SchoolsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Spell", null)
                        .WithMany()
                        .HasForeignKey("SpellsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Actor", b =>
                {
                    b.Navigation("FlaggedActors");
                });

            modelBuilder.Entity("Domain.Entities.Event", b =>
                {
                    b.Navigation("Lines");
                });

            modelBuilder.Entity("Domain.Entities.FlaggedActor", b =>
                {
                    b.Navigation("LinesSource");

                    b.Navigation("LinesTarget");
                });

            modelBuilder.Entity("Domain.Entities.Guild", b =>
                {
                    b.Navigation("Reports");
                });

            modelBuilder.Entity("Domain.Entities.Report", b =>
                {
                    b.Navigation("Lines");
                });
#pragma warning restore 612, 618
        }
    }
}
