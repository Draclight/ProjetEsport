using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projetesport.Data.Migrations
{
    public partial class MigrationReboot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licencie_Equipe_EquipeID",
                table: "Licencie");

            migrationBuilder.AlterColumn<string>(
                name: "IdUtilisateur",
                table: "Licencie",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "EquipeID",
                table: "Licencie",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "PremierConnexion",
                table: "Licencie",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Licencie_Equipe_EquipeID",
                table: "Licencie",
                column: "EquipeID",
                principalTable: "Equipe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licencie_Equipe_EquipeID",
                table: "Licencie");

            migrationBuilder.DropColumn(
                name: "PremierConnexion",
                table: "Licencie");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdUtilisateur",
                table: "Licencie",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EquipeID",
                table: "Licencie",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Licencie_Equipe_EquipeID",
                table: "Licencie",
                column: "EquipeID",
                principalTable: "Equipe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
