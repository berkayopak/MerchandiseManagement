using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MerchandiseManagementApi.Migrations
{
    public partial class AddMinStockQuantityColumnToCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinStockQuantity",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinStockQuantity",
                table: "Categories");
        }
    }
}
