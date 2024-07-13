using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class Fix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Contracts_ContractIdContract",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ContractIdContract",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ContractIdContract",
                table: "Transactions");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_IdContract",
                table: "Transactions",
                column: "IdContract");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Contracts_IdContract",
                table: "Transactions",
                column: "IdContract",
                principalTable: "Contracts",
                principalColumn: "IdContract",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Contracts_IdContract",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_IdContract",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "ContractIdContract",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ContractIdContract",
                table: "Transactions",
                column: "ContractIdContract");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Contracts_ContractIdContract",
                table: "Transactions",
                column: "ContractIdContract",
                principalTable: "Contracts",
                principalColumn: "IdContract",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
