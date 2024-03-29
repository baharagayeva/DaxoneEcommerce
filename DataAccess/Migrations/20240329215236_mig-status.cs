using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class migstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorProduct");

            migrationBuilder.DropTable(
                name: "ProductProductStatus");

            migrationBuilder.DropTable(
                name: "ProductSize");

            migrationBuilder.AlterColumn<bool>(
                name: "StockOut",
                table: "ProductStatuses",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "New",
                table: "ProductStatuses",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "InStock",
                table: "ProductStatuses",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ProductColors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    ColorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductColors_Colors_ColorID",
                        column: x => x.ColorID,
                        principalTable: "Colors",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductColors_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProductProductStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    ProductStatusID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductStatuses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductProductStatuses_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductProductStatuses_ProductStatuses_ProductStatusID",
                        column: x => x.ProductStatusID,
                        principalTable: "ProductStatuses",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProductSizes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    SizeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductSizes_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductSizes_Sizes_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Sizes",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ColorID",
                table: "ProductColors",
                column: "ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColors_ProductID",
                table: "ProductColors",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductStatuses_ProductID",
                table: "ProductProductStatuses",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductStatuses_ProductStatusID",
                table: "ProductProductStatuses",
                column: "ProductStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_ProductID",
                table: "ProductSizes",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_SizeID",
                table: "ProductSizes",
                column: "SizeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductColors");

            migrationBuilder.DropTable(
                name: "ProductProductStatuses");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.AlterColumn<string>(
                name: "StockOut",
                table: "ProductStatuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "New",
                table: "ProductStatuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "InStock",
                table: "ProductStatuses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "ColorProduct",
                columns: table => new
                {
                    ColorsID = table.Column<int>(type: "int", nullable: false),
                    ProductsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorProduct", x => new { x.ColorsID, x.ProductsID });
                    table.ForeignKey(
                        name: "FK_ColorProduct_Colors_ColorsID",
                        column: x => x.ColorsID,
                        principalTable: "Colors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColorProduct_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductStatus",
                columns: table => new
                {
                    ProductStatusesID = table.Column<int>(type: "int", nullable: false),
                    ProductsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductStatus", x => new { x.ProductStatusesID, x.ProductsID });
                    table.ForeignKey(
                        name: "FK_ProductProductStatus_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductStatus_ProductStatuses_ProductStatusesID",
                        column: x => x.ProductStatusesID,
                        principalTable: "ProductStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSize",
                columns: table => new
                {
                    ProductsID = table.Column<int>(type: "int", nullable: false),
                    SizesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSize", x => new { x.ProductsID, x.SizesID });
                    table.ForeignKey(
                        name: "FK_ProductSize_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSize_Sizes_SizesID",
                        column: x => x.SizesID,
                        principalTable: "Sizes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColorProduct_ProductsID",
                table: "ColorProduct",
                column: "ProductsID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductStatus_ProductsID",
                table: "ProductProductStatus",
                column: "ProductsID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSize_SizesID",
                table: "ProductSize",
                column: "SizesID");
        }
    }
}
