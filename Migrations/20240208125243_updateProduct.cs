using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class updateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_Product_tb_ProductCategory_ProductCategoryID",
                table: "tb_Product");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategoryID",
                table: "tb_Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_Product_tb_ProductCategory_ProductCategoryID",
                table: "tb_Product",
                column: "ProductCategoryID",
                principalTable: "tb_ProductCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_Product_tb_ProductCategory_ProductCategoryID",
                table: "tb_Product");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategoryID",
                table: "tb_Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_Product_tb_ProductCategory_ProductCategoryID",
                table: "tb_Product",
                column: "ProductCategoryID",
                principalTable: "tb_ProductCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
