using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreHouse360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationPropertiesToJournalDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Journals_AccountId",
                table: "Journals",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_CurrencyId",
                table: "Journals",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_SourceAccountId",
                table: "Journals",
                column: "SourceAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Accounts_AccountId",
                table: "Journals",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Accounts_SourceAccountId",
                table: "Journals",
                column: "SourceAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Currencies_CurrencyId",
                table: "Journals",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Accounts_AccountId",
                table: "Journals");

            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Accounts_SourceAccountId",
                table: "Journals");

            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Currencies_CurrencyId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_AccountId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_CurrencyId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_SourceAccountId",
                table: "Journals");
        }
    }
}
