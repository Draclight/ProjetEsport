using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEsport.Data.Migrations
{
    public partial class AjoutNomJeu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "Jeu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nom",
                table: "Jeu");
        }
    }
}
