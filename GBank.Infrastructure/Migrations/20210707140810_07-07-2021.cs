using Microsoft.EntityFrameworkCore.Migrations;

namespace GBank.Infrastructure.Migrations
{
    public partial class _07072021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Users_UserID",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_UserID",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Bills");

            migrationBuilder.CreateTable(
                name: "BillUser",
                columns: table => new
                {
                    BillsID = table.Column<int>(type: "int", nullable: false),
                    UsersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillUser", x => new { x.BillsID, x.UsersID });
                    table.ForeignKey(
                        name: "FK_BillUser_Bills_BillsID",
                        column: x => x.BillsID,
                        principalTable: "Bills",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillUser_Users_UsersID",
                        column: x => x.UsersID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillUser_UsersID",
                table: "BillUser",
                column: "UsersID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillUser");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Bills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserID",
                table: "Bills",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Users_UserID",
                table: "Bills",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
