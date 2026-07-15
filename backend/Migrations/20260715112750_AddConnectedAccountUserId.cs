using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApacStellar2026.Migrations
{
    /// <inheritdoc />
    public partial class AddConnectedAccountUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectedAccount_AspNetUsers_UserId",
                table: "ConnectedAccount");

            migrationBuilder.DropIndex(
                name: "IX_ConnectedAccount_UserId",
                table: "ConnectedAccount");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ConnectedAccount");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ConnectedAccount",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectedAccount_ApplicationUserId",
                table: "ConnectedAccount",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectedAccount_AspNetUsers_ApplicationUserId",
                table: "ConnectedAccount",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectedAccount_AspNetUsers_ApplicationUserId",
                table: "ConnectedAccount");

            migrationBuilder.DropIndex(
                name: "IX_ConnectedAccount_ApplicationUserId",
                table: "ConnectedAccount");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ConnectedAccount");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ConnectedAccount",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConnectedAccount_UserId",
                table: "ConnectedAccount",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectedAccount_AspNetUsers_UserId",
                table: "ConnectedAccount",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
