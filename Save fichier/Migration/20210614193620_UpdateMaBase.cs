using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEsport.Data.Migrations
{
    public partial class UpdateMaBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypesDeCompetition_TypesDeCompetition_TypeCompetitionID",
                table: "TypesDeCompetition");

            migrationBuilder.DropIndex(
                name: "IX_TypesDeCompetition_TypeCompetitionID",
                table: "TypesDeCompetition");

            migrationBuilder.DropColumn(
                name: "TypeCompetitionID",
                table: "TypesDeCompetition");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeCompetitionID",
                table: "TypesDeCompetition",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypesDeCompetition_TypeCompetitionID",
                table: "TypesDeCompetition",
                column: "TypeCompetitionID");

            migrationBuilder.AddForeignKey(
                name: "FK_TypesDeCompetition_TypesDeCompetition_TypeCompetitionID",
                table: "TypesDeCompetition",
                column: "TypeCompetitionID",
                principalTable: "TypesDeCompetition",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
