using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoTeste.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedido_cliente_cliente_id",
                table: "pedido");

            migrationBuilder.AddForeignKey(
                name: "fkey_cliente_id",
                table: "pedido",
                column: "cliente_id",
                principalTable: "cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fkey_cliente_id",
                table: "pedido");

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_cliente_cliente_id",
                table: "pedido",
                column: "cliente_id",
                principalTable: "cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
