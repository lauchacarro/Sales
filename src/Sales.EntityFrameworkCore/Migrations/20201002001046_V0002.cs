using Microsoft.EntityFrameworkCore.Migrations;

namespace Sales.EntityFrameworkCore.Migrations
{
    public partial class V0002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "sale",
                table: "Notifications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "sale",
                table: "Notifications");
        }
    }
}
