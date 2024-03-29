using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class migproductStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockOut",
                table: "ProductStatuses",
                newName: "IsStockOut");

            migrationBuilder.RenameColumn(
                name: "New",
                table: "ProductStatuses",
                newName: "IsStock");

            migrationBuilder.RenameColumn(
                name: "InStock",
                table: "ProductStatuses",
                newName: "IsNew");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsStockOut",
                table: "ProductStatuses",
                newName: "StockOut");

            migrationBuilder.RenameColumn(
                name: "IsStock",
                table: "ProductStatuses",
                newName: "New");

            migrationBuilder.RenameColumn(
                name: "IsNew",
                table: "ProductStatuses",
                newName: "InStock");
        }
    }
}
