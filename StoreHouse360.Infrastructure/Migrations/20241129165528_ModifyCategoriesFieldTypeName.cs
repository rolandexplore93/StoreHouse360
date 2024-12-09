using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreHouse360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCategoriesFieldTypeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CatId",
                table: "Categories",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CatId");
        }
    }
}
