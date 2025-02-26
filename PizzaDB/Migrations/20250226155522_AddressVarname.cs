using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaDB.Migrations
{
    /// <inheritdoc />
    public partial class AddressVarname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Addresses",
                newName: "_Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_Address",
                table: "Addresses",
                newName: "Address");
        }
    }
}
