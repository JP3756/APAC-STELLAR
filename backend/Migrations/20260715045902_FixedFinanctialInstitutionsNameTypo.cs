using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApacStellar2026.Migrations
{
    /// <inheritdoc />
    public partial class FixedFinanctialInstitutionsNameTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectedAccount_FinanctialInstitution_FinanctialInstitutio~",
                table: "ConnectedAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinanctialInstitution",
                table: "FinanctialInstitution");

            migrationBuilder.RenameTable(
                name: "FinanctialInstitution",
                newName: "FinancialInstitution");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialInstitution",
                table: "FinancialInstitution",
                column: "FinanctialInstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectedAccount_FinancialInstitution_FinanctialInstitution~",
                table: "ConnectedAccount",
                column: "FinanctialInstitutionId",
                principalTable: "FinancialInstitution",
                principalColumn: "FinanctialInstitutionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectedAccount_FinancialInstitution_FinanctialInstitution~",
                table: "ConnectedAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialInstitution",
                table: "FinancialInstitution");

            migrationBuilder.RenameTable(
                name: "FinancialInstitution",
                newName: "FinanctialInstitution");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinanctialInstitution",
                table: "FinanctialInstitution",
                column: "FinanctialInstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectedAccount_FinanctialInstitution_FinanctialInstitutio~",
                table: "ConnectedAccount",
                column: "FinanctialInstitutionId",
                principalTable: "FinanctialInstitution",
                principalColumn: "FinanctialInstitutionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
