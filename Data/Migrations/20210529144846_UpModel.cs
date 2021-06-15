using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GuildId",
                table: "Reports",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnvironmentType",
                table: "Event",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExtraSpellId",
                table: "Event",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_GuildId",
                table: "Reports",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_ExtraSpellId",
                table: "Event",
                column: "ExtraSpellId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Spells_ExtraSpellId",
                table: "Event",
                column: "ExtraSpellId",
                principalTable: "Spells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Guilds_GuildId",
                table: "Reports",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Spells_ExtraSpellId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Guilds_GuildId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_GuildId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Event_ExtraSpellId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "EnvironmentType",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "ExtraSpellId",
                table: "Event");
        }
    }
}
