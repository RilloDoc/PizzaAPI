using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaDB.Migrations
{
    /// <inheritdoc />
    public partial class Check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_OrderId",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OrderId",
                table: "Addresses",
                column: "OrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Addresses_OrderId",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OrderId",
                table: "Addresses",
                column: "OrderId");
        }
    }
}
