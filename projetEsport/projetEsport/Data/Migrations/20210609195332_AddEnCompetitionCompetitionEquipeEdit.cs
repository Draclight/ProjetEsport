using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEsport.Data.Migrations
{
    public partial class AddEnCompetitionCompetitionEquipeEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnCompetition",
                table: "CompetitionEquipe",
                newName: "EncoreEnCompetition");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EncoreEnCompetition",
                table: "CompetitionEquipe",
                newName: "EnCompetition");
        }
    }
}
