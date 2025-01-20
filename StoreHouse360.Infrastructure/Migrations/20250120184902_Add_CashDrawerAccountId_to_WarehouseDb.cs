using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreHouse360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_CashDrawerAccountId_to_WarehouseDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CashDrawerAccountId",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashDrawerAccountId",
                table: "Warehouses");
        }
    }
}
