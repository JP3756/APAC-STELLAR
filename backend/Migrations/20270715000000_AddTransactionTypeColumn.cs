using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApacStellar2026.Migrations
{
    public partial class AddTransactionTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Transaction",
                type: "integer",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transaction");
        }
    }
}