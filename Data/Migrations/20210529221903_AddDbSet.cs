using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Reports_ReportId",
                table: "Actor");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Reports_ReportId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Spells_ExtraSpellId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Spells_SpellId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_FlaggedActor_Actor_ActorId",
                table: "FlaggedActor");

            migrationBuilder.DropForeignKey(
                name: "FK_FlaggedActor_Reports_ReportId",
                table: "FlaggedActor");

            migrationBuilder.DropForeignKey(
                name: "FK_Line_Event_EventId",
                table: "Line");

            migrationBuilder.DropForeignKey(
                name: "FK_Line_FlaggedActor_SourceFlaggedActorId",
                table: "Line");

            migrationBuilder.DropForeignKey(
                name: "FK_Line_FlaggedActor_TargetFlaggedActorId",
                table: "Line");

            migrationBuilder.DropForeignKey(
                name: "FK_Line_Reports_ReportId",
                table: "Line");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Line",
                table: "Line");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlaggedActor",
                table: "FlaggedActor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Event",
                table: "Event");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actor",
                table: "Actor");

            migrationBuilder.RenameTable(
                name: "Line",
                newName: "Lines");

            migrationBuilder.RenameTable(
                name: "FlaggedActor",
                newName: "FlaggedActors");

            migrationBuilder.RenameTable(
                name: "Event",
                newName: "Events");

            migrationBuilder.RenameTable(
                name: "Actor",
                newName: "Actors");

            migrationBuilder.RenameIndex(
                name: "IX_Line_TargetFlaggedActorId",
                table: "Lines",
                newName: "IX_Lines_TargetFlaggedActorId");

            migrationBuilder.RenameIndex(
                name: "IX_Line_SourceFlaggedActorId",
                table: "Lines",
                newName: "IX_Lines_SourceFlaggedActorId");

            migrationBuilder.RenameIndex(
                name: "IX_Line_ReportId_LineId",
                table: "Lines",
                newName: "IX_Lines_ReportId_LineId");

            migrationBuilder.RenameIndex(
                name: "IX_Line_EventId",
                table: "Lines",
                newName: "IX_Lines_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_FlaggedActor_ReportId_FlaggedActorId",
                table: "FlaggedActors",
                newName: "IX_FlaggedActors_ReportId_FlaggedActorId");

            migrationBuilder.RenameIndex(
                name: "IX_FlaggedActor_ActorId",
                table: "FlaggedActors",
                newName: "IX_FlaggedActors_ActorId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_SpellId",
                table: "Events",
                newName: "IX_Events_SpellId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_ReportId_EventId",
                table: "Events",
                newName: "IX_Events_ReportId_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_ExtraSpellId",
                table: "Events",
                newName: "IX_Events_ExtraSpellId");

            migrationBuilder.RenameIndex(
                name: "IX_Actor_ReportId_ActorId",
                table: "Actors",
                newName: "IX_Actors_ReportId_ActorId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Reports_ReportId",
                table: "Actors",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Reports_ReportId",
                table: "Events",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Spells_ExtraSpellId",
                table: "Events",
                column: "ExtraSpellId",
                principalTable: "Spells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Spells_SpellId",
                table: "Events",
                column: "SpellId",
                principalTable: "Spells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlaggedActors_Actors_ActorId",
                table: "FlaggedActors",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlaggedActors_Reports_ReportId",
                table: "FlaggedActors",
                column: "ReportId",
                principalTable: "Reports",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_Reports_ReportId",
                table: "Lines",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Reports_ReportId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Reports_ReportId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Spells_ExtraSpellId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Spells_SpellId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_FlaggedActors_Actors_ActorId",
                table: "FlaggedActors");

            migrationBuilder.DropForeignKey(
                name: "FK_FlaggedActors_Reports_ReportId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Lines_Reports_ReportId",
                table: "Lines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lines",
                table: "Lines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlaggedActors",
                table: "FlaggedActors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Events",
                table: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actors",
                table: "Actors");

            migrationBuilder.RenameTable(
                name: "Lines",
                newName: "Line");

            migrationBuilder.RenameTable(
                name: "FlaggedActors",
                newName: "FlaggedActor");

            migrationBuilder.RenameTable(
                name: "Events",
                newName: "Event");

            migrationBuilder.RenameTable(
                name: "Actors",
                newName: "Actor");

            migrationBuilder.RenameIndex(
                name: "IX_Lines_TargetFlaggedActorId",
                table: "Line",
                newName: "IX_Line_TargetFlaggedActorId");

            migrationBuilder.RenameIndex(
                name: "IX_Lines_SourceFlaggedActorId",
                table: "Line",
                newName: "IX_Line_SourceFlaggedActorId");

            migrationBuilder.RenameIndex(
                name: "IX_Lines_ReportId_LineId",
                table: "Line",
                newName: "IX_Line_ReportId_LineId");

            migrationBuilder.RenameIndex(
                name: "IX_Lines_EventId",
                table: "Line",
                newName: "IX_Line_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_FlaggedActors_ReportId_FlaggedActorId",
                table: "FlaggedActor",
                newName: "IX_FlaggedActor_ReportId_FlaggedActorId");

            migrationBuilder.RenameIndex(
                name: "IX_FlaggedActors_ActorId",
                table: "FlaggedActor",
                newName: "IX_FlaggedActor_ActorId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_SpellId",
                table: "Event",
                newName: "IX_Event_SpellId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_ReportId_EventId",
                table: "Event",
                newName: "IX_Event_ReportId_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_ExtraSpellId",
                table: "Event",
                newName: "IX_Event_ExtraSpellId");

            migrationBuilder.RenameIndex(
                name: "IX_Actors_ReportId_ActorId",
                table: "Actor",
                newName: "IX_Actor_ReportId_ActorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Line",
                table: "Line",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlaggedActor",
                table: "FlaggedActor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Event",
                table: "Event",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actor",
                table: "Actor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Reports_ReportId",
                table: "Actor",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Reports_ReportId",
                table: "Event",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Spells_ExtraSpellId",
                table: "Event",
                column: "ExtraSpellId",
                principalTable: "Spells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Spells_SpellId",
                table: "Event",
                column: "SpellId",
                principalTable: "Spells",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlaggedActor_Actor_ActorId",
                table: "FlaggedActor",
                column: "ActorId",
                principalTable: "Actor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlaggedActor_Reports_ReportId",
                table: "FlaggedActor",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Line_Event_EventId",
                table: "Line",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Line_FlaggedActor_SourceFlaggedActorId",
                table: "Line",
                column: "SourceFlaggedActorId",
                principalTable: "FlaggedActor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Line_FlaggedActor_TargetFlaggedActorId",
                table: "Line",
                column: "TargetFlaggedActorId",
                principalTable: "FlaggedActor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Line_Reports_ReportId",
                table: "Line",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
