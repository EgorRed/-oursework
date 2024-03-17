using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingForExpirationDates.Migrations
{
    /// <inheritdoc />
    public partial class NewUserID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessToWarehouseEntityWarehouseEntity_Warehouses_WarehouseIdId",
                table: "AccessToWarehouseEntityWarehouseEntity");

            migrationBuilder.RenameColumn(
                name: "WarehouseIdId",
                table: "AccessToWarehouseEntityWarehouseEntity",
                newName: "WarehousesId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessToWarehouseEntityWarehouseEntity_WarehouseIdId",
                table: "AccessToWarehouseEntityWarehouseEntity",
                newName: "IX_AccessToWarehouseEntityWarehouseEntity_WarehousesId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AccessToWarehouse",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessToWarehouseEntityWarehouseEntity_Warehouses_WarehousesId",
                table: "AccessToWarehouseEntityWarehouseEntity",
                column: "WarehousesId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessToWarehouseEntityWarehouseEntity_Warehouses_WarehousesId",
                table: "AccessToWarehouseEntityWarehouseEntity");

            migrationBuilder.RenameColumn(
                name: "WarehousesId",
                table: "AccessToWarehouseEntityWarehouseEntity",
                newName: "WarehouseIdId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessToWarehouseEntityWarehouseEntity_WarehousesId",
                table: "AccessToWarehouseEntityWarehouseEntity",
                newName: "IX_AccessToWarehouseEntityWarehouseEntity_WarehouseIdId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AccessToWarehouse",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessToWarehouseEntityWarehouseEntity_Warehouses_WarehouseIdId",
                table: "AccessToWarehouseEntityWarehouseEntity",
                column: "WarehouseIdId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
