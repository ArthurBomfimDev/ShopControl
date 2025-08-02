using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class finalUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_marca_BrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Product_ProductId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_pedido_OrderId",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrder_OrderId",
                table: "ProductOrder");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrder_ProductId",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_BrandId",
                table: "Product");

            migrationBuilder.RenameTable(
                name: "ProductOrder",
                newName: "pedido_de_produto");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "produto");

            migrationBuilder.RenameColumn(
                name: "SubTotal",
                table: "pedido_de_produto",
                newName: "subtotal");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "pedido_de_produto",
                newName: "preco_unitario");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "pedido_de_produto",
                newName: "quantidade");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "pedido_de_produto",
                newName: "id_de_produto");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "pedido_de_produto",
                newName: "id_de_pedido");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "produto",
                newName: "estoque");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "produto",
                newName: "preco");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "produto",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "produto",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "produto",
                newName: "codigo");

            migrationBuilder.RenameColumn(
                name: "BrandId",
                table: "produto",
                newName: "id_da_marca");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "cliente",
                type: "varchar(320)",
                maxLength: 320,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "produto",
                keyColumn: "nome",
                keyValue: null,
                column: "nome",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "produto",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "produto",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "produto",
                keyColumn: "codigo",
                keyValue: null,
                column: "codigo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "codigo",
                table: "produto",
                type: "varchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pedido_de_produto",
                table: "pedido_de_produto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_produto",
                table: "produto",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_produto",
                table: "produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pedido_de_produto",
                table: "pedido_de_produto");

            migrationBuilder.RenameTable(
                name: "produto",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "pedido_de_produto",
                newName: "ProductOrder");

            migrationBuilder.RenameColumn(
                name: "preco",
                table: "Product",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Product",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id_da_marca",
                table: "Product",
                newName: "BrandId");

            migrationBuilder.RenameColumn(
                name: "estoque",
                table: "Product",
                newName: "Stock");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "Product",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "codigo",
                table: "Product",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "subtotal",
                table: "ProductOrder",
                newName: "SubTotal");

            migrationBuilder.RenameColumn(
                name: "quantidade",
                table: "ProductOrder",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "preco_unitario",
                table: "ProductOrder",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "id_de_produto",
                table: "ProductOrder",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "id_de_pedido",
                table: "ProductOrder",
                newName: "OrderId");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "cliente",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(320)",
                oldMaxLength: 320)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Product",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(40)",
                oldMaxLength: 40)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Product",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldMaxLength: 6)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_OrderId",
                table: "ProductOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_ProductId",
                table: "ProductOrder",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_marca_BrandId",
                table: "Product",
                column: "BrandId",
                principalTable: "marca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Product_ProductId",
                table: "ProductOrder",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_pedido_OrderId",
                table: "ProductOrder",
                column: "OrderId",
                principalTable: "pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
