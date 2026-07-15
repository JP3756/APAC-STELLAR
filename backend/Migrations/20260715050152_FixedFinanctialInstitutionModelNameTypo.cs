using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApacStellar2026.Migrations
{
    /// <inheritdoc />
    public partial class FixedFinanctialInstitutionModelNameTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectedAccount_FinancialInstitution_FinanctialInstitution~",
                table: "ConnectedAccount");

            migrationBuilder.RenameColumn(
                name: "FinanctialInstitutionId",
                table: "FinancialInstitution",
                newName: "FinancialInstitutionId");

            migrationBuilder.RenameColumn(
                name: "FinanctialInstitutionId",
                table: "ConnectedAccount",
                newName: "FinancialInstitutionId");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectedAccount_FinanctialInstitutionId",
                table: "ConnectedAccount",
                newName: "IX_ConnectedAccount_FinancialInstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectedAccount_FinancialInstitution_FinancialInstitutionId",
                table: "ConnectedAccount",
                column: "FinancialInstitutionId",
                principalTable: "FinancialInstitution",
                principalColumn: "FinancialInstitutionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectedAccount_FinancialInstitution_FinancialInstitutionId",
                table: "ConnectedAccount");

            migrationBuilder.RenameColumn(
                name: "FinancialInstitutionId",
                table: "FinancialInstitution",
                newName: "FinanctialInstitutionId");

            migrationBuilder.RenameColumn(
                name: "FinancialInstitutionId",
                table: "ConnectedAccount",
                newName: "FinanctialInstitutionId");

            migrationBuilder.RenameIndex(
                name: "IX_ConnectedAccount_FinancialInstitutionId",
                table: "ConnectedAccount",
                newName: "IX_ConnectedAccount_FinanctialInstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectedAccount_FinancialInstitution_FinanctialInstitution~",
                table: "ConnectedAccount",
                column: "FinanctialInstitutionId",
                principalTable: "FinancialInstitution",
                principalColumn: "FinanctialInstitutionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
