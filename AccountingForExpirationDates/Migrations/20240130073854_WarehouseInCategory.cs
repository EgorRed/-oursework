using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingForExpirationDates.Migrations
{
    /// <inheritdoc />
    public partial class WarehouseInCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_WarehouseId",
                table: "Category",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Warehouses_WarehouseId",
                table: "Category",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Warehouses_WarehouseId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_WarehouseId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Category");
        }
    }
}
