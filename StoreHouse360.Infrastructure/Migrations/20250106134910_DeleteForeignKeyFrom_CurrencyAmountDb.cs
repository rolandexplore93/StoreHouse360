using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreHouse360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteForeignKeyFrom_CurrencyAmountDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyAmounts_Payments_ObjectId",
                table: "CurrencyAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyAmounts_ProductMovements_ObjectId",
                table: "CurrencyAmounts");

            migrationBuilder.RenameColumn(
                name: "ObjectId",
                table: "CurrencyAmounts",
                newName: "ProductMovementDbId");

            migrationBuilder.RenameIndex(
                name: "IX_CurrencyAmounts_ObjectId",
                table: "CurrencyAmounts",
                newName: "IX_CurrencyAmounts_ProductMovementDbId");

            migrationBuilder.AddColumn<int>(
                name: "PaymentDbId",
                table: "CurrencyAmounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyAmounts_PaymentDbId",
                table: "CurrencyAmounts",
                column: "PaymentDbId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyAmounts_Payments_PaymentDbId",
                table: "CurrencyAmounts",
                column: "PaymentDbId",
                principalTable: "Payments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyAmounts_ProductMovements_ProductMovementDbId",
                table: "CurrencyAmounts",
                column: "ProductMovementDbId",
                principalTable: "ProductMovements",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyAmounts_Payments_PaymentDbId",
                table: "CurrencyAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyAmounts_ProductMovements_ProductMovementDbId",
                table: "CurrencyAmounts");

            migrationBuilder.DropIndex(
                name: "IX_CurrencyAmounts_PaymentDbId",
                table: "CurrencyAmounts");

            migrationBuilder.DropColumn(
                name: "PaymentDbId",
                table: "CurrencyAmounts");

            migrationBuilder.RenameColumn(
                name: "ProductMovementDbId",
                table: "CurrencyAmounts",
                newName: "ObjectId");

            migrationBuilder.RenameIndex(
                name: "IX_CurrencyAmounts_ProductMovementDbId",
                table: "CurrencyAmounts",
                newName: "IX_CurrencyAmounts_ObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyAmounts_Payments_ObjectId",
                table: "CurrencyAmounts",
                column: "ObjectId",
                principalTable: "Payments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyAmounts_ProductMovements_ObjectId",
                table: "CurrencyAmounts",
                column: "ObjectId",
                principalTable: "ProductMovements",
                principalColumn: "Id");
        }
    }
}
