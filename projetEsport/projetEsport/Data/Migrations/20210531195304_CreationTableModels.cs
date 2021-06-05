using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEsport.Data.Migrations
{
    public partial class CreationTableModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jeux",
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
                    table.PrimaryKey("PK_Jeux", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypesDeCompetition",
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
                    table.PrimaryKey("PK_TypesDeCompetition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TypesDeCompetition_TypesDeCompetition_TypeCompetitionID",
                        column: x => x.TypeCompetitionID,
                        principalTable: "TypesDeCompetition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TypesDeMatche",
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
                    table.PrimaryKey("PK_TypesDeMatche", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CompetitionJeu",
                columns: table => new
                {
                    CompetitionsJeuSelectionneID = table.Column<int>(type: "int", nullable: false),
                    JeuxDeLaCompetitionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitionJeu", x => new { x.CompetitionsJeuSelectionneID, x.JeuxDeLaCompetitionID });
                    table.ForeignKey(
                        name: "FK_CompetitionJeu_Jeux_JeuxDeLaCompetitionID",
                        column: x => x.JeuxDeLaCompetitionID,
                        principalTable: "Jeux",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifieeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JeuID = table.Column<int>(type: "int", nullable: false),
                    CompetitionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Equipes_Jeux_JeuID",
                        column: x => x.JeuID,
                        principalTable: "Jeux",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Licencies",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pseudo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifieeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UtilisateurID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EquipeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licencies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Licencies_AspNetUsers_UtilisateurID",
                        column: x => x.UtilisateurID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Licencies_Equipes_EquipeID",
                        column: x => x.EquipeID,
                        principalTable: "Equipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifieeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeCompetitionID = table.Column<int>(type: "int", nullable: false),
                    ProprietaireID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Competitions_Licencies_ProprietaireID",
                        column: x => x.ProprietaireID,
                        principalTable: "Licencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Competitions_TypesDeCompetition_TypeCompetitionID",
                        column: x => x.TypeCompetitionID,
                        principalTable: "TypesDeCompetition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvitationsEquipes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipeID = table.Column<int>(type: "int", nullable: false),
                    LicencieID = table.Column<int>(type: "int", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitationsEquipes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InvitationsEquipes_Equipes_EquipeID",
                        column: x => x.EquipeID,
                        principalTable: "Equipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvitationsEquipes_Licencies_LicencieID",
                        column: x => x.LicencieID,
                        principalTable: "Licencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeMatcheID = table.Column<int>(type: "int", nullable: false),
                    CompetitionID = table.Column<int>(type: "int", nullable: false),
                    JeuID = table.Column<int>(type: "int", nullable: false),
                    VictoireAEquipe1 = table.Column<int>(type: "int", nullable: false),
                    VictoireAEquipe2 = table.Column<int>(type: "int", nullable: false),
                    CreeLe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifieeLe = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Matches_Competitions_CompetitionID",
                        column: x => x.CompetitionID,
                        principalTable: "Competitions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Jeux_JeuID",
                        column: x => x.JeuID,
                        principalTable: "Jeux",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_TypesDeMatche_TypeMatcheID",
                        column: x => x.TypeMatcheID,
                        principalTable: "TypesDeMatche",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipeMatche",
                columns: table => new
                {
                    EquipesDisputesID = table.Column<int>(type: "int", nullable: false),
                    MatchesDisputesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipeMatche", x => new { x.EquipesDisputesID, x.MatchesDisputesID });
                    table.ForeignKey(
                        name: "FK_EquipeMatche_Equipes_EquipesDisputesID",
                        column: x => x.EquipesDisputesID,
                        principalTable: "Equipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipeMatche_Matches_MatchesDisputesID",
                        column: x => x.MatchesDisputesID,
                        principalTable: "Matches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompetitionJeu_JeuxDeLaCompetitionID",
                table: "CompetitionJeu",
                column: "JeuxDeLaCompetitionID");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_ProprietaireID",
                table: "Competitions",
                column: "ProprietaireID");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_TypeCompetitionID",
                table: "Competitions",
                column: "TypeCompetitionID");

            migrationBuilder.CreateIndex(
                name: "IX_EquipeMatche_MatchesDisputesID",
                table: "EquipeMatche",
                column: "MatchesDisputesID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_CompetitionID",
                table: "Equipes",
                column: "CompetitionID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipes_JeuID",
                table: "Equipes",
                column: "JeuID");

            migrationBuilder.CreateIndex(
                name: "IX_InvitationsEquipes_EquipeID",
                table: "InvitationsEquipes",
                column: "EquipeID");

            migrationBuilder.CreateIndex(
                name: "IX_InvitationsEquipes_LicencieID",
                table: "InvitationsEquipes",
                column: "LicencieID");

            migrationBuilder.CreateIndex(
                name: "IX_Licencies_EquipeID",
                table: "Licencies",
                column: "EquipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Licencies_UtilisateurID",
                table: "Licencies",
                column: "UtilisateurID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CompetitionID",
                table: "Matches",
                column: "CompetitionID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_JeuID",
                table: "Matches",
                column: "JeuID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TypeMatcheID",
                table: "Matches",
                column: "TypeMatcheID");

            migrationBuilder.CreateIndex(
                name: "IX_TypesDeCompetition_TypeCompetitionID",
                table: "TypesDeCompetition",
                column: "TypeCompetitionID");

            migrationBuilder.AddForeignKey(
                name: "FK_CompetitionJeu_Competitions_CompetitionsJeuSelectionneID",
                table: "CompetitionJeu",
                column: "CompetitionsJeuSelectionneID",
                principalTable: "Competitions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipes_Competitions_CompetitionID",
                table: "Equipes",
                column: "CompetitionID",
                principalTable: "Competitions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            //Rôles
            migrationBuilder.Sql("INSERT INTO AspNetRoles values(1, 'Administrateur', 'ADMINISTRATEUR', '');");
            migrationBuilder.Sql("INSERT INTO AspNetRoles values(2, 'Organisateur', 'ORGANISATEUR', '');");
            migrationBuilder.Sql("INSERT INTO AspNetRoles values(3, 'Licencie', 'LICENCIE', '');");

            //Users
            migrationBuilder.Sql("INSERT INTO AspNetUsers values('1'," +
                " 'Admin', 'ADMIN'," +
                "'admin@projetEsport.com', 'ADMIN@PROJETESPORT.COM'," +
                "1," +
                "'AQAAAAEAACcQAAAAEE6G6YNincCbxLQX7ZldxmMRX6Y18UwU84HjtX/3yByfe/V/b94+E6hVroyjVOR86Q=='," +
                "'HUJRX2J5YIOLZU5FSR5ZHOI6N5QAIOMG'," +
                "'28d74e71-d475-440f-b4a5-7663a022c855'," +
                "''," +
                "0," +
                "0," +
                "null," +
                "1," +
                "0)");

            //UserRole
            migrationBuilder.Sql("INSERT INTO AspNetUserRoles values('1', '1')");

            //Licencie
            migrationBuilder.Sql("INSERT INTO Licencies(Pseudo, Prenom, Nom, CreeLe, ModifieeLe, UtilisateurID, EquipeID) values('Admin', 'Admin', 'Admin', GETDATE(), GETDATE(), '1', null);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipes_Competitions_CompetitionID",
                table: "Equipes");

            migrationBuilder.DropTable(
                name: "CompetitionJeu");

            migrationBuilder.DropTable(
                name: "EquipeMatche");

            migrationBuilder.DropTable(
                name: "InvitationsEquipes");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "TypesDeMatche");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Licencies");

            migrationBuilder.DropTable(
                name: "TypesDeCompetition");

            migrationBuilder.DropTable(
                name: "Equipes");

            migrationBuilder.DropTable(
                name: "Jeux");
        }
    }
}
