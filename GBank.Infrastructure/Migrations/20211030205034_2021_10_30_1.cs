using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GBank.Infrastructure.Migrations
{
    public partial class _2021_10_30_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.CreateTable(
                name: "BillTransactions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    direction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    transactionid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    billID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BillTransactions_Bills_billID",
                        column: x => x.billID,
                        principalTable: "Bills",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillTransactions_billID",
                table: "BillTransactions",
                column: "billID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillTransactions");

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    billID = table.Column<int>(type: "int", nullable: true),
                    datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    direction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transactionid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transaction_Bills_billID",
                        column: x => x.billID,
                        principalTable: "Bills",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_billID",
                table: "Transaction",
                column: "billID");
        }
    }
}
