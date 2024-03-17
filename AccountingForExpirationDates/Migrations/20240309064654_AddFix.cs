using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingForExpirationDates.Migrations
{
    /// <inheritdoc />
    public partial class AddFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "AccessToWarehouse");

            migrationBuilder.CreateTable(
                name: "AccessToWarehouseEntityWarehouseEntity",
                columns: table => new
                {
                    AccessToWarehouseId = table.Column<int>(type: "int", nullable: false),
                    WarehouseIdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessToWarehouseEntityWarehouseEntity", x => new { x.AccessToWarehouseId, x.WarehouseIdId });
                    table.ForeignKey(
                        name: "FK_AccessToWarehouseEntityWarehouseEntity_AccessToWarehouse_AccessToWarehouseId",
                        column: x => x.AccessToWarehouseId,
                        principalTable: "AccessToWarehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessToWarehouseEntityWarehouseEntity_Warehouses_WarehouseIdId",
                        column: x => x.WarehouseIdId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessToWarehouseEntityWarehouseEntity_WarehouseIdId",
                table: "AccessToWarehouseEntityWarehouseEntity",
                column: "WarehouseIdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessToWarehouseEntityWarehouseEntity");

            migrationBuilder.AddColumn<string>(
                name: "WarehouseId",
                table: "AccessToWarehouse",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
