﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreHouse360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_CountryOriginsTable_and_FKRtoProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                    name: "CountryOriginId",
                    table: "Products",
                    type: "int",
                    nullable: true
                );

            migrationBuilder.CreateTable(
                name: "CountryOrigins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryOrigins", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CountryOriginId",
                table: "Products",
                column: "CountryOriginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CountryOrigins_CountryOriginId",
                table: "Products",
                column: "CountryOriginId",
                principalTable: "CountryOrigins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CountryOrigins_CountryOriginId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "CountryOrigins");

            migrationBuilder.DropIndex(
                name: "IX_Products_CountryOriginId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CountryOriginId",
                table: "Products");
        }
    }
}
