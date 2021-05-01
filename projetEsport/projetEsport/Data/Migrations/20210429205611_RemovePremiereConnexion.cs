using Microsoft.EntityFrameworkCore.Migrations;

namespace projetEsport.Data.Migrations
{
    public partial class RemovePremiereConnexion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PremierConnexion",
                table: "Licencie");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PremierConnexion",
                table: "Licencie",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
