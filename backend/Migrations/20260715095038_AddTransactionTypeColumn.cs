using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApacStellar2026.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectedAccount_AspNetUsers_UserId",
                table: "ConnectedAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectedAccount_FinancialInstitution_FinancialInstitutionId",
                table: "ConnectedAccount");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Transaction",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ApiEndpoint",
                table: "FinancialInstitution",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ConnectedAccount",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "FinancialInstitutionId",
                table: "ConnectedAccount",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectedAccount_AspNetUsers_UserId",
                table: "ConnectedAccount",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectedAccount_FinancialInstitution_FinancialInstitutionId",
                table: "ConnectedAccount",
                column: "FinancialInstitutionId",
                principalTable: "FinancialInstitution",
                principalColumn: "FinancialInstitutionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectedAccount_AspNetUsers_UserId",
                table: "ConnectedAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectedAccount_FinancialInstitution_FinancialInstitutionId",
                table: "ConnectedAccount");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transaction");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Transaction",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ApiEndpoint",
                table: "FinancialInstitution",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ConnectedAccount",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FinancialInstitutionId",
                table: "ConnectedAccount",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectedAccount_AspNetUsers_UserId",
                table: "ConnectedAccount",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectedAccount_FinancialInstitution_FinancialInstitutionId",
                table: "ConnectedAccount",
                column: "FinancialInstitutionId",
                principalTable: "FinancialInstitution",
                principalColumn: "FinancialInstitutionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
