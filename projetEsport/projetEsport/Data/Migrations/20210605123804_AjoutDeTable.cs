using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEsport.Data.Migrations
{
    public partial class AjoutDeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipes_Competitions_CompetitionID",
                table: "Equipes");

            migrationBuilder.DropIndex(
                name: "IX_Equipes_CompetitionID",
                table: "Equipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipeMatche",
                table: "EquipeMatche");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetitionJeu",
                table: "CompetitionJeu");

            migrationBuilder.DropColumn(
                name: "CompetitionID",
                table: "Equipes");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "EquipeMatche",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "CompetitionJeu",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipeMatche",
                table: "EquipeMatche",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetitionJeu",
                table: "CompetitionJeu",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "CompetitionEquipe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipeID = table.Column<int>(type: "int", nullable: false),
                    CompetitionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionEquipe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CompetitionEquipe_Competitions_CompetitionID",
                        column: x => x.CompetitionID,
                        principalTable: "Competitions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionEquipe_Equipes_EquipeID",
                        column: x => x.EquipeID,
                        principalTable: "Equipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipeMatche_EquipesDisputesID",
                table: "EquipeMatche",
                column: "EquipesDisputesID");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionJeu_CompetitionsJeuSelectionneID",
                table: "CompetitionJeu",
                column: "CompetitionsJeuSelectionneID");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEquipe_CompetitionID",
                table: "CompetitionEquipe",
                column: "CompetitionID");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEquipe_EquipeID",
                table: "CompetitionEquipe",
                column: "EquipeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompetitionEquipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipeMatche",
                table: "EquipeMatche");

            migrationBuilder.DropIndex(
                name: "IX_EquipeMatche_EquipesDisputesID",
                table: "EquipeMatche");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetitionJeu",
                table: "CompetitionJeu");

            migrationBuilder.DropIndex(
                name: "IX_CompetitionJeu_CompetitionsJeuSelectionneID",
                table: "CompetitionJeu");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "EquipeMatche");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "CompetitionJeu");

            migrationBuilder.AddColumn<int>(
                name: "CompetitionID",
                table: "Equipes",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipeMatche",
                table: "EquipeMatche",
                columns: new[] { "EquipesDisputesID", "MatchesDisputesID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetitionJeu",
                table: "CompetitionJeu",
                columns: new[] { "CompetitionsJeuSelectionneID", "JeuxDeLaCompetitionID" });

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_CompetitionID",
                table: "Equipes",
                column: "CompetitionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipes_Competitions_CompetitionID",
                table: "Equipes",
                column: "CompetitionID",
                principalTable: "Competitions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
