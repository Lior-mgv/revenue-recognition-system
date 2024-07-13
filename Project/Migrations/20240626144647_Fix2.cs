using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class Fix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Contracts_ContractIdContract",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_ContractIdContract",
                table: "Transactions",
                newName: "IX_Transactions_ContractIdContract");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "IdTransaction");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Contracts_ContractIdContract",
                table: "Transactions",
                column: "ContractIdContract",
                principalTable: "Contracts",
                principalColumn: "IdContract",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Contracts_ContractIdContract",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_ContractIdContract",
                table: "Transaction",
                newName: "IX_Transaction_ContractIdContract");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "IdTransaction");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Contracts_ContractIdContract",
                table: "Transaction",
                column: "ContractIdContract",
                principalTable: "Contracts",
                principalColumn: "IdContract",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
