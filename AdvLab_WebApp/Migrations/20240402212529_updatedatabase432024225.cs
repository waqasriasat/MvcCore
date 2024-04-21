using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvLab_WebApp.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase432024225 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalA",
                table: "DescCashiers",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateIndex(
                name: "IX_DescCashiers_MRNO",
                table: "DescCashiers",
                column: "MRNO");

            migrationBuilder.AddForeignKey(
                name: "FK_DescCashiers_PatRegs_MRNO",
                table: "DescCashiers",
                column: "MRNO",
                principalTable: "PatRegs",
                principalColumn: "MRNO",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DescCashiers_PatRegs_MRNO",
                table: "DescCashiers");

            migrationBuilder.DropIndex(
                name: "IX_DescCashiers_MRNO",
                table: "DescCashiers");

            migrationBuilder.AlterColumn<double>(
                name: "TotalA",
                table: "DescCashiers",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
