using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEsport.Data.Migrations
{
    public partial class AjoutJeuEquipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JeuId",
                table: "Equipe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Equipe_JeuId",
                table: "Equipe",
                column: "JeuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipe_Jeu_JeuId",
                table: "Equipe",
                column: "JeuId",
                principalTable: "Jeu",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipe_Jeu_JeuId",
                table: "Equipe");

            migrationBuilder.DropIndex(
                name: "IX_Equipe_JeuId",
                table: "Equipe");

            migrationBuilder.DropColumn(
                name: "JeuId",
                table: "Equipe");
        }
    }
}
