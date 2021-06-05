using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEsport.Data.Migrations
{
    public partial class AjoutCreateurEquipeDansLicencie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CreateurEquipe",
                table: "Licencies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateurEquipe",
                table: "Licencies");
        }
    }
}
