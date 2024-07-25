using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gerenciar_pedidos.Migrations
{
    /// <inheritdoc />
    public partial class AddProductNameToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "ProductName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Name");
        }
    }
}
