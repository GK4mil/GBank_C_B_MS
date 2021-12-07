using Microsoft.EntityFrameworkCore.Migrations;

namespace GBank.Infrastructure.Migrations
{
    public partial class _2021101501 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "transactionid",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "transactionid",
                table: "Transaction");
        }
    }
}
