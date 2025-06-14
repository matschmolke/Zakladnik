using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zakladnik.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldsToZaklad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Podatek",
                table: "Zaklady",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Podatek",
                table: "Zaklady");
        }
    }
}
