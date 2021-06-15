using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guilds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Timezone = table.Column<string>(type: "text", nullable: false),
                    Realm = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Faction = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guilds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Sid = table.Column<string>(type: "text", nullable: false),
                    E = table.Column<long>(type: "bigint", nullable: false),
                    S = table.Column<long>(type: "bigint", nullable: false),
                    EndTime = table.Column<long>(type: "bigint", nullable: false),
                    ExportDate = table.Column<long>(type: "bigint", nullable: false),
                    HasForbiddenBuffs = table.Column<bool>(type: "boolean", nullable: false),
                    StartTime = table.Column<long>(type: "bigint", nullable: false),
                    TimeInfoE = table.Column<long>(type: "bigint", nullable: true),
                    TimeInfoS = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spells", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Actor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReportId = table.Column<long>(type: "bigint", nullable: false),
                    ActorId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Clazz = table.Column<string>(type: "text", nullable: false),
                    Guid = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actor_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReportId = table.Column<long>(type: "bigint", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    PowerType = table.Column<int>(type: "integer", nullable: false),
                    SpellId = table.Column<int>(type: "integer", nullable: true),
                    SubType = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    School = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: true),
                    Flags = table.Column<int>(type: "integer", nullable: true),
                    MissAmount = table.Column<int>(type: "integer", nullable: true),
                    Blocked = table.Column<int>(type: "integer", nullable: true),
                    Overheal = table.Column<int>(type: "integer", nullable: true),
                    Overkill = table.Column<int>(type: "integer", nullable: true),
                    Absorbed = table.Column<int>(type: "integer", nullable: true),
                    Resisted = table.Column<int>(type: "integer", nullable: true),
                    MissType = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchoolSpell",
                columns: table => new
                {
                    SchoolsId = table.Column<int>(type: "integer", nullable: false),
                    SpellsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolSpell", x => new { x.SchoolsId, x.SpellsId });
                    table.ForeignKey(
                        name: "FK_SchoolSpell_Schools_SchoolsId",
                        column: x => x.SchoolsId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolSpell_Spells_SpellsId",
                        column: x => x.SpellsId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlaggedActor",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReportId = table.Column<long>(type: "bigint", nullable: false),
                    FlaggedActorId = table.Column<int>(type: "integer", nullable: false),
                    ActorId = table.Column<long>(type: "bigint", nullable: false),
                    Flags = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlaggedActor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlaggedActor_Actor_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlaggedActor_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Line",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReportId = table.Column<long>(type: "bigint", nullable: false),
                    LineId = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false),
                    SourceFlaggedActorId = table.Column<long>(type: "bigint", nullable: true),
                    TargetFlaggedActorId = table.Column<long>(type: "bigint", nullable: true),
                    EventId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Line_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Line_FlaggedActor_SourceFlaggedActorId",
                        column: x => x.SourceFlaggedActorId,
                        principalTable: "FlaggedActor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Line_FlaggedActor_TargetFlaggedActorId",
                        column: x => x.TargetFlaggedActorId,
                        principalTable: "FlaggedActor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Line_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actor_ReportId_ActorId",
                table: "Actor",
                columns: new[] { "ReportId", "ActorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_ReportId_EventId",
                table: "Event",
                columns: new[] { "ReportId", "EventId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_SpellId",
                table: "Event",
                column: "SpellId");

            migrationBuilder.CreateIndex(
                name: "IX_FlaggedActor_ActorId",
                table: "FlaggedActor",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_FlaggedActor_ReportId_FlaggedActorId",
                table: "FlaggedActor",
                columns: new[] { "ReportId", "FlaggedActorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Line_EventId",
                table: "Line",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Line_ReportId_LineId",
                table: "Line",
                columns: new[] { "ReportId", "LineId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Line_SourceFlaggedActorId",
                table: "Line",
                column: "SourceFlaggedActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Line_TargetFlaggedActorId",
                table: "Line",
                column: "TargetFlaggedActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Sid",
                table: "Reports",
                column: "Sid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolSpell_SpellsId",
                table: "SchoolSpell",
                column: "SpellsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guilds");

            migrationBuilder.DropTable(
                name: "Line");

            migrationBuilder.DropTable(
                name: "SchoolSpell");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "FlaggedActor");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "Spells");

            migrationBuilder.DropTable(
                name: "Actor");

            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
