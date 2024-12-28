using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreHouse360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountsTableToDb_and_ModifyStoragePlacesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_storagePlaces_Warehouses_WarehouseId",
                table: "storagePlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_storagePlaces_storagePlaces_ContainerId",
                table: "storagePlaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_storagePlaces",
                table: "storagePlaces");

            migrationBuilder.RenameTable(
                name: "storagePlaces",
                newName: "StoragePlaces");

            migrationBuilder.RenameIndex(
                name: "IX_storagePlaces_WarehouseId",
                table: "StoragePlaces",
                newName: "IX_StoragePlaces_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_storagePlaces_ContainerId",
                table: "StoragePlaces",
                newName: "IX_StoragePlaces_ContainerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoragePlaces",
                table: "StoragePlaces",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoragePlaces_StoragePlaces_ContainerId",
                table: "StoragePlaces",
                column: "ContainerId",
                principalTable: "StoragePlaces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoragePlaces_Warehouses_WarehouseId",
                table: "StoragePlaces",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoragePlaces_StoragePlaces_ContainerId",
                table: "StoragePlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_StoragePlaces_Warehouses_WarehouseId",
                table: "StoragePlaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoragePlaces",
                table: "StoragePlaces");

            migrationBuilder.RenameTable(
                name: "StoragePlaces",
                newName: "storagePlaces");

            migrationBuilder.RenameIndex(
                name: "IX_StoragePlaces_WarehouseId",
                table: "storagePlaces",
                newName: "IX_storagePlaces_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_StoragePlaces_ContainerId",
                table: "storagePlaces",
                newName: "IX_storagePlaces_ContainerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_storagePlaces",
                table: "storagePlaces",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_storagePlaces_Warehouses_WarehouseId",
                table: "storagePlaces",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_storagePlaces_storagePlaces_ContainerId",
                table: "storagePlaces",
                column: "ContainerId",
                principalTable: "storagePlaces",
                principalColumn: "Id");
        }
    }
}
