using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class Fix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IndividualClientId",
                table: "IndividualClients",
                newName: "IdIndividualClient");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Companies",
                newName: "IdCompany");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "IndividualClients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "IndividualClients",
                keyColumn: "IdIndividualClient",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "IndividualClients",
                keyColumn: "IdIndividualClient",
                keyValue: 2,
                column: "IsDeleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "IndividualClients");

            migrationBuilder.RenameColumn(
                name: "IdIndividualClient",
                table: "IndividualClients",
                newName: "IndividualClientId");

            migrationBuilder.RenameColumn(
                name: "IdCompany",
                table: "Companies",
                newName: "CompanyId");
        }
    }
}
