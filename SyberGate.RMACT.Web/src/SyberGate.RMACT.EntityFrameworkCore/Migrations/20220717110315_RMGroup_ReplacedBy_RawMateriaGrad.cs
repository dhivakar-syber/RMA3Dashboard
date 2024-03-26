using Microsoft.EntityFrameworkCore.Migrations;

namespace SyberGate.RMACT.Migrations
{
    public partial class RMGroup_ReplacedBy_RawMateriaGrad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_BaseRMRates_RMGroups_RMGroupId",
            //    table: "BaseRMRates");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_RMGroups_RMGroupId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterialMixtures_RMGroups_RMGroupId",
                table: "RawMaterialMixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_SubParts_RMGroups_RMGroupId",
                table: "SubParts");

            migrationBuilder.DropTable(
                name: "RMGrades");

            migrationBuilder.AddColumn<int>(
                name: "RMGroupId",
                table: "RawMaterialGrades",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RawMaterialGrades_RMGroupId",
                table: "RawMaterialGrades",
                column: "RMGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseRMRates_RawMaterialGrades_RMGroupId",
                table: "BaseRMRates",
                column: "RMGroupId",
                principalTable: "RawMaterialGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_RawMaterialGrades_RMGroupId",
                table: "Parts",
                column: "RMGroupId",
                principalTable: "RawMaterialGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterialGrades_RMGroups_RMGroupId",
                table: "RawMaterialGrades",
                column: "RMGroupId",
                principalTable: "RMGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterialMixtures_RawMaterialGrades_RMGroupId",
                table: "RawMaterialMixtures",
                column: "RMGroupId",
                principalTable: "RawMaterialGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction,
                onUpdate: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SubParts_RawMaterialGrades_RMGroupId",
                table: "SubParts",
                column: "RMGroupId",
                principalTable: "RawMaterialGrades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseRMRates_RawMaterialGrades_RMGroupId",
                table: "BaseRMRates");

            migrationBuilder.DropForeignKey(
                name: "FK_Parts_RawMaterialGrades_RMGroupId",
                table: "Parts");

            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterialGrades_RMGroups_RMGroupId",
                table: "RawMaterialGrades");

            migrationBuilder.DropForeignKey(
                name: "FK_RawMaterialMixtures_RawMaterialGrades_RMGroupId",
                table: "RawMaterialMixtures");

            migrationBuilder.DropForeignKey(
                name: "FK_SubParts_RawMaterialGrades_RMGroupId",
                table: "SubParts");

            migrationBuilder.DropIndex(
                name: "IX_RawMaterialGrades_RMGroupId",
                table: "RawMaterialGrades");

            migrationBuilder.DropColumn(
                name: "RMGroupId",
                table: "RawMaterialGrades");

            migrationBuilder.CreateTable(
                name: "RMGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RMGrades", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BaseRMRates_RMGroups_RMGroupId",
                table: "BaseRMRates",
                column: "RMGroupId",
                principalTable: "RMGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parts_RMGroups_RMGroupId",
                table: "Parts",
                column: "RMGroupId",
                principalTable: "RMGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RawMaterialMixtures_RMGroups_RMGroupId",
                table: "RawMaterialMixtures",
                column: "RMGroupId",
                principalTable: "RMGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubParts_RMGroups_RMGroupId",
                table: "SubParts",
                column: "RMGroupId",
                principalTable: "RMGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
