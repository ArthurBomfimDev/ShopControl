using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopControl.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class orderfinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedido_cliente_CustomerId",
                table: "pedido");

            migrationBuilder.RenameColumn(
                name: "data",
                table: "pedido",
                newName: "data_do_pedido");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "pedido",
                newName: "id_do_cliente");

            migrationBuilder.RenameIndex(
                name: "IX_pedido_CustomerId",
                table: "pedido",
                newName: "IX_pedido_id_do_cliente");

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_cliente_id_do_cliente",
                table: "pedido",
                column: "id_do_cliente",
                principalTable: "cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedido_cliente_id_do_cliente",
                table: "pedido");

            migrationBuilder.RenameColumn(
                name: "id_do_cliente",
                table: "pedido",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "data_do_pedido",
                table: "pedido",
                newName: "data");

            migrationBuilder.RenameIndex(
                name: "IX_pedido_id_do_cliente",
                table: "pedido",
                newName: "IX_pedido_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_cliente_CustomerId",
                table: "pedido",
                column: "CustomerId",
                principalTable: "cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
