using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class Regenerated_BaseRMRate6431 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseRMRates_RMGrades_RMGradeId",
                table: "BaseRMRates");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseRMRates_UOMs_UOMId",
                table: "BaseRMRates");

            migrationBuilder.DropIndex(
                name: "IX_BaseRMRates_RMGradeId",
                table: "BaseRMRates");

            migrationBuilder.DropIndex(
                name: "IX_BaseRMRates_UOMId",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "RMGradeId",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "UOMId",
                table: "BaseRMRates");

            migrationBuilder.AddColumn<int>(
                name: "RawMaterialGradeId",
                table: "BaseRMRates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasurementId",
                table: "BaseRMRates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseRMRates_RawMaterialGradeId",
                table: "BaseRMRates",
                column: "RawMaterialGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseRMRates_UnitOfMeasurementId",
                table: "BaseRMRates",
                column: "UnitOfMeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseRMRates_RawMaterialGrades_RawMaterialGradeId",
                table: "BaseRMRates",
                column: "RawMaterialGradeId",
                principalTable: "RawMaterialGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseRMRates_UnitOfMeasurements_UnitOfMeasurementId",
                table: "BaseRMRates",
                column: "UnitOfMeasurementId",
                principalTable: "UnitOfMeasurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseRMRates_RawMaterialGrades_RawMaterialGradeId",
                table: "BaseRMRates");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseRMRates_UnitOfMeasurements_UnitOfMeasurementId",
                table: "BaseRMRates");

            migrationBuilder.DropIndex(
                name: "IX_BaseRMRates_RawMaterialGradeId",
                table: "BaseRMRates");

            migrationBuilder.DropIndex(
                name: "IX_BaseRMRates_UnitOfMeasurementId",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "RawMaterialGradeId",
                table: "BaseRMRates");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurementId",
                table: "BaseRMRates");

            migrationBuilder.AddColumn<int>(
                name: "RMGradeId",
                table: "BaseRMRates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UOMId",
                table: "BaseRMRates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseRMRates_RMGradeId",
                table: "BaseRMRates",
                column: "RMGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseRMRates_UOMId",
                table: "BaseRMRates",
                column: "UOMId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseRMRates_RMGrades_RMGradeId",
                table: "BaseRMRates",
                column: "RMGradeId",
                principalTable: "RMGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseRMRates_UOMs_UOMId",
                table: "BaseRMRates",
                column: "UOMId",
                principalTable: "UOMs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
