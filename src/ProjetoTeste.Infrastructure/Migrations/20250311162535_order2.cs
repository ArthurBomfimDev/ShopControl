using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoTeste.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class order2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "pedido",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "pedido",
                newName: "data");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "total",
                table: "pedido",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "data",
                table: "pedido",
                newName: "OrderDate");
        }
    }
}
