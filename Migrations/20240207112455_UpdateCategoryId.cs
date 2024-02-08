using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_New_tb_Category_CategoryID",
                table: "tb_New");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_Post_tb_Category_CategoryId",
                table: "tb_Post");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "tb_Post",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "tb_New",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_New_tb_Category_CategoryID",
                table: "tb_New",
                column: "CategoryID",
                principalTable: "tb_Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_Post_tb_Category_CategoryId",
                table: "tb_Post",
                column: "CategoryId",
                principalTable: "tb_Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_New_tb_Category_CategoryID",
                table: "tb_New");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_Post_tb_Category_CategoryId",
                table: "tb_Post");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "tb_Post",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "tb_New",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_New_tb_Category_CategoryID",
                table: "tb_New",
                column: "CategoryID",
                principalTable: "tb_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_Post_tb_Category_CategoryId",
                table: "tb_Post",
                column: "CategoryId",
                principalTable: "tb_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
