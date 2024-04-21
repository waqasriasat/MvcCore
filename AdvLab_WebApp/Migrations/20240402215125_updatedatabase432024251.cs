using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvLab_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase432024251 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DescCashiers_PatRegs_MRNO",
                table: "DescCashiers");

            migrationBuilder.DropForeignKey(
                name: "FK_DescLabs_DescCashiers_LabNo",
                table: "DescLabs");

            migrationBuilder.AlterColumn<int>(
                name: "LabNo",
                table: "DescLabs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MRNO",
                table: "DescCashiers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DescCashiers_PatRegs_MRNO",
                table: "DescCashiers",
                column: "MRNO",
                principalTable: "PatRegs",
                principalColumn: "MRNO",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DescLabs_DescCashiers_LabNo",
                table: "DescLabs",
                column: "LabNo",
                principalTable: "DescCashiers",
                principalColumn: "LabNo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DescCashiers_PatRegs_MRNO",
                table: "DescCashiers");

            migrationBuilder.DropForeignKey(
                name: "FK_DescLabs_DescCashiers_LabNo",
                table: "DescLabs");

            migrationBuilder.AlterColumn<int>(
                name: "LabNo",
                table: "DescLabs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MRNO",
                table: "DescCashiers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DescCashiers_PatRegs_MRNO",
                table: "DescCashiers",
                column: "MRNO",
                principalTable: "PatRegs",
                principalColumn: "MRNO");

            migrationBuilder.AddForeignKey(
                name: "FK_DescLabs_DescCashiers_LabNo",
                table: "DescLabs",
                column: "LabNo",
                principalTable: "DescCashiers",
                principalColumn: "LabNo");
        }
    }
}
