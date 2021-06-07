using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEsport.Data.Migrations
{
    public partial class AjoutDateMatche : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VictoireAEquipe2",
                table: "Matches",
                newName: "VictoireEquipeB");

            migrationBuilder.RenameColumn(
                name: "VictoireAEquipe1",
                table: "Matches",
                newName: "VictoireEquipeA");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMatche",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateMatche",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "VictoireEquipeB",
                table: "Matches",
                newName: "VictoireAEquipe2");

            migrationBuilder.RenameColumn(
                name: "VictoireEquipeA",
                table: "Matches",
                newName: "VictoireAEquipe1");
        }
    }
}
