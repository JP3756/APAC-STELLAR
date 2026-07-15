using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApacStellar2026.Migrations
{
    /// <inheritdoc />
    public partial class AddStellarEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StellarWallet",
                columns: table => new
                {
                    StellarWalletId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: false),
                    PublicKey = table.Column<string>(type: "text", nullable: false),
                    SecretKeyEncrypted = table.Column<string>(type: "text", nullable: false),
                    Network = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StellarWallet", x => x.StellarWalletId);
                    table.ForeignKey(
                        name: "FK_StellarWallet_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StellarAsset",
                columns: table => new
                {
                    StellarAssetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StellarWalletId = table.Column<int>(type: "integer", nullable: false),
                    AssetCode = table.Column<string>(type: "text", nullable: false),
                    AssetIssuer = table.Column<string>(type: "text", nullable: true),
                    Balance = table.Column<decimal>(type: "numeric(18,7)", nullable: false),
                    IsIssuer = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StellarAsset", x => x.StellarAssetId);
                    table.ForeignKey(
                        name: "FK_StellarAsset_StellarWallet_StellarWalletId",
                        column: x => x.StellarWalletId,
                        principalTable: "StellarWallet",
                        principalColumn: "StellarWalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StellarPayment",
                columns: table => new
                {
                    StellarPaymentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StellarWalletId = table.Column<int>(type: "integer", nullable: false),
                    DestinationAddress = table.Column<string>(type: "text", nullable: false),
                    AssetCode = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,7)", nullable: false),
                    Memo = table.Column<string>(type: "text", nullable: true),
                    TransactionHash = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StellarPayment", x => x.StellarPaymentId);
                    table.ForeignKey(
                        name: "FK_StellarPayment_StellarWallet_StellarWalletId",
                        column: x => x.StellarWalletId,
                        principalTable: "StellarWallet",
                        principalColumn: "StellarWalletId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StellarAsset_StellarWalletId",
                table: "StellarAsset",
                column: "StellarWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_StellarPayment_StellarWalletId",
                table: "StellarPayment",
                column: "StellarWalletId");

            migrationBuilder.CreateIndex(
                name: "IX_StellarWallet_ApplicationUserId",
                table: "StellarWallet",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StellarAsset");

            migrationBuilder.DropTable(
                name: "StellarPayment");

            migrationBuilder.DropTable(
                name: "StellarWallet");
        }
    }
}
