using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GBank.Infrastructure.Migrations
{
    public partial class _20211014_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          //  migrationBuilder.DropTable(
           //     name: "Transaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        /*    migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    billID = table.Column<int>(type: "int", nullable: true),
                    datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    direction = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
        */
        }
    }
}
