using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoTeste.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fkey_cliente_id",
                table: "pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_pedido_de_produto_pedido_pedido_id",
                table: "pedido_de_produto");

            migrationBuilder.DropForeignKey(
                name: "FK_pedido_de_produto_produto_produto_id",
                table: "pedido_de_produto");

            migrationBuilder.DropForeignKey(
                name: "fkey_id_marca",
                table: "produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_produto",
                table: "produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pedido_de_produto",
                table: "pedido_de_produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pedido",
                table: "pedido");

            migrationBuilder.RenameTable(
                name: "produto",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "pedido_de_produto",
                newName: "ProductOrder");

            migrationBuilder.RenameTable(
                name: "pedido",
                newName: "Order");

            migrationBuilder.RenameColumn(
                name: "cpf",
                table: "cliente",
                newName: "CPF");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Product",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id_marca",
                table: "Product",
                newName: "BrandId");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "Product",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "codigo",
                table: "Product",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_produto_id_marca",
                table: "Product",
                newName: "IX_Product_BrandId");

            migrationBuilder.RenameColumn(
                name: "subtotal",
                table: "ProductOrder",
                newName: "SubTotal");

            migrationBuilder.RenameColumn(
                name: "quantidade",
                table: "ProductOrder",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "produto_id",
                table: "ProductOrder",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "preco_unitario",
                table: "ProductOrder",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "pedido_id",
                table: "ProductOrder",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_pedido_de_produto_produto_id",
                table: "ProductOrder",
                newName: "IX_ProductOrder_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_pedido_de_produto_pedido_id",
                table: "ProductOrder",
                newName: "IX_ProductOrder_OrderId");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "Order",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "data_do_pedido",
                table: "Order",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "cliente_id",
                table: "Order",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_pedido_cliente_id",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "telefone",
                table: "cliente",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "cliente",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "cliente",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "cliente",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15)
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_cliente_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_marca_BrandId",
                table: "Product",
                column: "BrandId",
                principalTable: "marca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Order_OrderId",
                table: "ProductOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrder_Product_ProductId",
                table: "ProductOrder",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_cliente_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_marca_BrandId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Order_OrderId",
                table: "ProductOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrder_Product_ProductId",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductOrder",
                table: "ProductOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "ProductOrder",
                newName: "pedido_de_produto");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "produto");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "pedido");

            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "cliente",
                newName: "cpf");

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
                newName: "produto_id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "pedido_de_produto",
                newName: "pedido_id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrder_ProductId",
                table: "pedido_de_produto",
                newName: "IX_pedido_de_produto_produto_id");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrder_OrderId",
                table: "pedido_de_produto",
                newName: "IX_pedido_de_produto_pedido_id");

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
                newName: "id_marca");

            migrationBuilder.RenameIndex(
                name: "IX_Product_BrandId",
                table: "produto",
                newName: "IX_produto_id_marca");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "pedido",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "pedido",
                newName: "data_do_pedido");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "pedido",
                newName: "cliente_id");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "pedido",
                newName: "IX_pedido_cliente_id");

            migrationBuilder.AlterColumn<string>(
                name: "telefone",
                table: "cliente",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "cliente",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "cliente",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "cpf",
                table: "cliente",
                type: "varchar(15)",
                maxLength: 15,
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_pedido",
                table: "pedido",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "fkey_cliente_id",
                table: "pedido",
                column: "cliente_id",
                principalTable: "cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_de_produto_pedido_pedido_id",
                table: "pedido_de_produto",
                column: "pedido_id",
                principalTable: "pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_de_produto_produto_produto_id",
                table: "pedido_de_produto",
                column: "produto_id",
                principalTable: "produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fkey_id_marca",
                table: "produto",
                column: "id_marca",
                principalTable: "marca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
