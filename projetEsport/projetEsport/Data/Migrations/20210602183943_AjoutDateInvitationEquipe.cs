using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEsport.Data.Migrations
{
    public partial class AjoutDateInvitationEquipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateAccepter",
                table: "InvitationsEquipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnvoi",
                table: "InvitationsEquipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAccepter",
                table: "InvitationsEquipes");

            migrationBuilder.DropColumn(
                name: "DateEnvoi",
                table: "InvitationsEquipes");
        }
    }
}
