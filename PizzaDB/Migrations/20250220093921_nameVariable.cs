using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaDB.Migrations
{
    /// <inheritdoc />
    public partial class nameVariable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Orders_orderId",
                table: "Pizzas");

            migrationBuilder.RenameColumn(
                name: "orderId",
                table: "Pizzas",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Pizzas_orderId",
                table: "Pizzas",
                newName: "IX_Pizzas_OrderId");

            migrationBuilder.AddColumn<string>(
                name: "Gusto",
                table: "Pizzas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Orders_OrderId",
                table: "Pizzas",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Orders_OrderId",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "Gusto",
                table: "Pizzas");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Pizzas",
                newName: "orderId");

            migrationBuilder.RenameIndex(
                name: "IX_Pizzas_OrderId",
                table: "Pizzas",
                newName: "IX_Pizzas_orderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Orders_orderId",
                table: "Pizzas",
                column: "orderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
