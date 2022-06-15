using Microsoft.EntityFrameworkCore.Migrations;

namespace AWS_E_Commerce.Migrations
{
    public partial class addnewtabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_ProductDTO_productId",
                table: "ShoppingCartItems");

            migrationBuilder.AlterColumn<int>(
                name: "productId",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Products_productId",
                table: "ShoppingCartItems",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Products_productId",
                table: "ShoppingCartItems");

            migrationBuilder.AlterColumn<int>(
                name: "productId",
                table: "ShoppingCartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_ProductDTO_productId",
                table: "ShoppingCartItems",
                column: "productId",
                principalTable: "ProductDTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
