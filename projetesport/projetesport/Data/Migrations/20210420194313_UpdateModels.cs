using Microsoft.EntityFrameworkCore.Migrations;

namespace projetesport.Data.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassementCompetition_Competition_CompetitionId",
                table: "ClassementCompetition");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassementCompetition_Equipe_EquipeId",
                table: "ClassementCompetition");

            migrationBuilder.DropForeignKey(
                name: "FK_Competition_TypeCompetition_TypeCompetitionID",
                table: "Competition");

            migrationBuilder.DropColumn(
                name: "TypeCompetitiionId",
                table: "Competition");

            migrationBuilder.RenameColumn(
                name: "EquipeId",
                table: "ClassementCompetition",
                newName: "EquipeID");

            migrationBuilder.RenameColumn(
                name: "CompetitionId",
                table: "ClassementCompetition",
                newName: "CompetitionID");

            migrationBuilder.RenameIndex(
                name: "IX_ClassementCompetition_EquipeId",
                table: "ClassementCompetition",
                newName: "IX_ClassementCompetition_EquipeID");

            migrationBuilder.RenameIndex(
                name: "IX_ClassementCompetition_CompetitionId",
                table: "ClassementCompetition",
                newName: "IX_ClassementCompetition_CompetitionID");

            migrationBuilder.AlterColumn<int>(
                name: "TypeCompetitionID",
                table: "Competition",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassementCompetition_Competition_CompetitionID",
                table: "ClassementCompetition",
                column: "CompetitionID",
                principalTable: "Competition",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassementCompetition_Equipe_EquipeID",
                table: "ClassementCompetition",
                column: "EquipeID",
                principalTable: "Equipe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Competition_TypeCompetition_TypeCompetitionID",
                table: "Competition",
                column: "TypeCompetitionID",
                principalTable: "TypeCompetition",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassementCompetition_Competition_CompetitionID",
                table: "ClassementCompetition");

            migrationBuilder.DropForeignKey(
                name: "FK_ClassementCompetition_Equipe_EquipeID",
                table: "ClassementCompetition");

            migrationBuilder.DropForeignKey(
                name: "FK_Competition_TypeCompetition_TypeCompetitionID",
                table: "Competition");

            migrationBuilder.RenameColumn(
                name: "EquipeID",
                table: "ClassementCompetition",
                newName: "EquipeId");

            migrationBuilder.RenameColumn(
                name: "CompetitionID",
                table: "ClassementCompetition",
                newName: "CompetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassementCompetition_EquipeID",
                table: "ClassementCompetition",
                newName: "IX_ClassementCompetition_EquipeId");

            migrationBuilder.RenameIndex(
                name: "IX_ClassementCompetition_CompetitionID",
                table: "ClassementCompetition",
                newName: "IX_ClassementCompetition_CompetitionId");

            migrationBuilder.AlterColumn<int>(
                name: "TypeCompetitionID",
                table: "Competition",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TypeCompetitiionId",
                table: "Competition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassementCompetition_Competition_CompetitionId",
                table: "ClassementCompetition",
                column: "CompetitionId",
                principalTable: "Competition",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassementCompetition_Equipe_EquipeId",
                table: "ClassementCompetition",
                column: "EquipeId",
                principalTable: "Equipe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Competition_TypeCompetition_TypeCompetitionID",
                table: "Competition",
                column: "TypeCompetitionID",
                principalTable: "TypeCompetition",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
