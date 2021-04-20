using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projetesport.Data.Migrations
{
    public partial class AjoutModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifieeLe = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipe", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Jeu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pseudo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifieeLe = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jeu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeCompetition",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifieeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeCompetitionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeCompetition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TypeCompetition_TypeCompetition_TypeCompetitionID",
                        column: x => x.TypeCompetitionID,
                        principalTable: "TypeCompetition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Licencie",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pseudo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Penom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUtilisateur = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifieeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EquipeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licencie", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Licencie_Equipe_EquipeID",
                        column: x => x.EquipeID,
                        principalTable: "Equipe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competition",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifieeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeCompetitiionId = table.Column<int>(type: "int", nullable: false),
                    TypeCompetitionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Competition_TypeCompetition_TypeCompetitionID",
                        column: x => x.TypeCompetitionID,
                        principalTable: "TypeCompetition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassementCompetition",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    EquipeId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    NbVictoire = table.Column<int>(type: "int", nullable: false),
                    NbDefaite = table.Column<int>(type: "int", nullable: false),
                    NbNulle = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassementCompetition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClassementCompetition_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassementCompetition_Equipe_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionEquipe",
                columns: table => new
                {
                    CompetitionsID = table.Column<int>(type: "int", nullable: false),
                    EquipesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionEquipe", x => new { x.CompetitionsID, x.EquipesID });
                    table.ForeignKey(
                        name: "FK_CompetitionEquipe_Competition_CompetitionsID",
                        column: x => x.CompetitionsID,
                        principalTable: "Competition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionEquipe_Equipe_EquipesID",
                        column: x => x.EquipesID,
                        principalTable: "Equipe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionJeu",
                columns: table => new
                {
                    CompetitionsID = table.Column<int>(type: "int", nullable: false),
                    JeuxID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionJeu", x => new { x.CompetitionsID, x.JeuxID });
                    table.ForeignKey(
                        name: "FK_CompetitionJeu_Competition_CompetitionsID",
                        column: x => x.CompetitionsID,
                        principalTable: "Competition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetitionJeu_Jeu_JeuxID",
                        column: x => x.JeuxID,
                        principalTable: "Jeu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassementCompetition_CompetitionId",
                table: "ClassementCompetition",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassementCompetition_EquipeId",
                table: "ClassementCompetition",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Competition_TypeCompetitionID",
                table: "Competition",
                column: "TypeCompetitionID");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionEquipe_EquipesID",
                table: "CompetitionEquipe",
                column: "EquipesID");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionJeu_JeuxID",
                table: "CompetitionJeu",
                column: "JeuxID");

            migrationBuilder.CreateIndex(
                name: "IX_Licencie_EquipeID",
                table: "Licencie",
                column: "EquipeID");

            migrationBuilder.CreateIndex(
                name: "IX_TypeCompetition_TypeCompetitionID",
                table: "TypeCompetition",
                column: "TypeCompetitionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassementCompetition");

            migrationBuilder.DropTable(
                name: "CompetitionEquipe");

            migrationBuilder.DropTable(
                name: "CompetitionJeu");

            migrationBuilder.DropTable(
                name: "Licencie");

            migrationBuilder.DropTable(
                name: "Competition");

            migrationBuilder.DropTable(
                name: "Jeu");

            migrationBuilder.DropTable(
                name: "Equipe");

            migrationBuilder.DropTable(
                name: "TypeCompetition");
        }
    }
}
