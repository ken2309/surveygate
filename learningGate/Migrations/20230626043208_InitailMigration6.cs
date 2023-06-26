using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learningGate.Migrations
{
    /// <inheritdoc />
    public partial class InitailMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "FavoriteDetail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "FavoriteDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
