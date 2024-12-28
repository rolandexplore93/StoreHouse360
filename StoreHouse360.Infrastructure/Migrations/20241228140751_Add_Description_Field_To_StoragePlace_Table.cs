using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreHouse360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Description_Field_To_StoragePlace_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "StoragePlaces",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "StoragePlaces");
        }
    }
}
