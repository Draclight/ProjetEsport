using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEsport.Data.Migrations
{
    public partial class AjoutPropriétaireCompetition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pseudo",
                table: "Jeu");

            migrationBuilder.AddColumn<int>(
                name: "ProprietaireID",
                table: "Competition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Competition_ProprietaireID",
                table: "Competition",
                column: "ProprietaireID");

            migrationBuilder.AddForeignKey(
                name: "FK_Competition_Licencie_ProprietaireID",
                table: "Competition",
                column: "ProprietaireID",
                principalTable: "Licencie",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competition_Licencie_ProprietaireID",
                table: "Competition");

            migrationBuilder.DropIndex(
                name: "IX_Competition_ProprietaireID",
                table: "Competition");

            migrationBuilder.DropColumn(
                name: "ProprietaireID",
                table: "Competition");

            migrationBuilder.AddColumn<string>(
                name: "Pseudo",
                table: "Jeu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
