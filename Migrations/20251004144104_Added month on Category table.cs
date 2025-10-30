using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedmonthonCategorytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "Categories");
        }
    }
}
