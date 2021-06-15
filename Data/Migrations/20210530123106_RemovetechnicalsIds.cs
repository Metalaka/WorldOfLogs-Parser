using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Data.Migrations
{
    public partial class RemovetechnicalsIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlaggedActors_Actors_ActorId",
                table: "FlaggedActors");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Events_EventId",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_FlaggedActors_SourceFlaggedActorId",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_FlaggedActors_TargetFlaggedActorId",
                table: "Lines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lines",
                table: "Lines");

            migrationBuilder.DropIndex(
                name: "IX_Lines_EventId",
                table: "Lines");

            migrationBuilder.DropIndex(
                name: "IX_Lines_ReportId_LineId",
                table: "Lines");

            migrationBuilder.DropIndex(
                name: "IX_Lines_SourceFlaggedActorId",
                table: "Lines");

            migrationBuilder.DropIndex(
                name: "IX_Lines_TargetFlaggedActorId",
                table: "Lines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlaggedActors",
                table: "FlaggedActors");

            migrationBuilder.DropIndex(
                name: "IX_FlaggedActors_ActorId",
                table: "FlaggedActors");

            migrationBuilder.DropIndex(
                name: "IX_FlaggedActors_ReportId_FlaggedActorId",
                table: "FlaggedActors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ReportId_EventId",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actors",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_ReportId_ActorId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "LineId",
                table: "Lines");

            migrationBuilder.DropColumn(
                name: "FlaggedActorId",
                table: "FlaggedActors");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "Actors");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Spells",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Schools",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TargetFlaggedActorId",
                table: "Lines",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SourceFlaggedActorId",
                table: "Lines",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Lines",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Lines",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ActorId",
                table: "FlaggedActors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FlaggedActors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Events",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Actors",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lines",
                table: "Lines",
                columns: new[] { "ReportId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlaggedActors",
                table: "FlaggedActors",
                columns: new[] { "ReportId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                columns: new[] { "ReportId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actors",
                table: "Actors",
                columns: new[] { "ReportId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Lines_ReportId_EventId",
                table: "Lines",
                columns: new[] { "ReportId", "EventId" });

            migrationBuilder.CreateIndex(
                name: "IX_Lines_ReportId_SourceFlaggedActorId",
                table: "Lines",
                columns: new[] { "ReportId", "SourceFlaggedActorId" });

            migrationBuilder.CreateIndex(
                name: "IX_Lines_ReportId_TargetFlaggedActorId",
                table: "Lines",
                columns: new[] { "ReportId", "TargetFlaggedActorId" });

            migrationBuilder.CreateIndex(
                name: "IX_FlaggedActors_ReportId_ActorId",
                table: "FlaggedActors",
                columns: new[] { "ReportId", "ActorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FlaggedActors_Actors_ReportId_ActorId",
                table: "FlaggedActors",
                columns: new[] { "ReportId", "ActorId" },
                principalTable: "Actors",
                principalColumns: new[] { "ReportId", "Id" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Events_ReportId_EventId",
                table: "Lines",
                columns: new[] { "ReportId", "EventId" },
                principalTable: "Events",
                principalColumns: new[] { "ReportId", "Id" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_FlaggedActors_ReportId_SourceFlaggedActorId",
                table: "Lines",
                columns: new[] { "ReportId", "SourceFlaggedActorId" },
                principalTable: "FlaggedActors",
                principalColumns: new[] { "ReportId", "Id" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_FlaggedActors_ReportId_TargetFlaggedActorId",
                table: "Lines",
                columns: new[] { "ReportId", "TargetFlaggedActorId" },
                principalTable: "FlaggedActors",
                principalColumns: new[] { "ReportId", "Id" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlaggedActors_Actors_ReportId_ActorId",
                table: "FlaggedActors");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Events_ReportId_EventId",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_FlaggedActors_ReportId_SourceFlaggedActorId",
                table: "Lines");

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_FlaggedActors_ReportId_TargetFlaggedActorId",
                table: "Lines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lines",
                table: "Lines");

            migrationBuilder.DropIndex(
                name: "IX_Lines_ReportId_EventId",
                table: "Lines");

            migrationBuilder.DropIndex(
                name: "IX_Lines_ReportId_SourceFlaggedActorId",
                table: "Lines");

            migrationBuilder.DropIndex(
                name: "IX_Lines_ReportId_TargetFlaggedActorId",
                table: "Lines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlaggedActors",
                table: "FlaggedActors");

            migrationBuilder.DropIndex(
                name: "IX_FlaggedActors_ReportId_ActorId",
                table: "FlaggedActors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actors",
                table: "Actors");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Spells",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Schools",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "TargetFlaggedActorId",
                table: "Lines",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "SourceFlaggedActorId",
                table: "Lines",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EventId",
                table: "Lines",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Lines",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "LineId",
                table: "Lines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "ActorId",
                table: "FlaggedActors",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "FlaggedActors",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "FlaggedActorId",
                table: "FlaggedActors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Events",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Actors",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ActorId",
                table: "Actors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lines",
                table: "Lines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlaggedActors",
                table: "FlaggedActors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Events",
                table: "Events",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actors",
                table: "Actors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_EventId",
                table: "Lines",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_ReportId_LineId",
                table: "Lines",
                columns: new[] { "ReportId", "LineId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lines_SourceFlaggedActorId",
                table: "Lines",
                column: "SourceFlaggedActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_TargetFlaggedActorId",
                table: "Lines",
                column: "TargetFlaggedActorId");

            migrationBuilder.CreateIndex(
                name: "IX_FlaggedActors_ActorId",
                table: "FlaggedActors",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_FlaggedActors_ReportId_FlaggedActorId",
                table: "FlaggedActors",
                columns: new[] { "ReportId", "FlaggedActorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_ReportId_EventId",
                table: "Events",
                columns: new[] { "ReportId", "EventId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actors_ReportId_ActorId",
                table: "Actors",
                columns: new[] { "ReportId", "ActorId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FlaggedActors_Actors_ActorId",
                table: "FlaggedActors",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Events_EventId",
                table: "Lines",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_FlaggedActors_SourceFlaggedActorId",
                table: "Lines",
                column: "SourceFlaggedActorId",
                principalTable: "FlaggedActors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_FlaggedActors_TargetFlaggedActorId",
                table: "Lines",
                column: "TargetFlaggedActorId",
                principalTable: "FlaggedActors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
