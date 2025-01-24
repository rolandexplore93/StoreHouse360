using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreHouse360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountTypeToAccountDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Accounts");
        }
    }
}
