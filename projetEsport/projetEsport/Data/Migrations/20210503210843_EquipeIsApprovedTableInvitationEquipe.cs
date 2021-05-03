using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEsport.Data.Migrations
{
    public partial class EquipeIsApprovedTableInvitationEquipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Equipe",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "InvitationEquipe",
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
                    table.PrimaryKey("PK_InvitationEquipe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InvitationEquipe_Equipe_EquipeID",
                        column: x => x.EquipeID,
                        principalTable: "Equipe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvitationEquipe_Licencie_LicencieID",
                        column: x => x.LicencieID,
                        principalTable: "Licencie",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvitationEquipe_EquipeID",
                table: "InvitationEquipe",
                column: "EquipeID");

            migrationBuilder.CreateIndex(
                name: "IX_InvitationEquipe_LicencieID",
                table: "InvitationEquipe",
                column: "LicencieID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvitationEquipe");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Equipe");
        }
    }
}
