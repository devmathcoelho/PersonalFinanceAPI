using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceAPI.Migrations
{
    /// <inheritdoc />
    public partial class NewRowsonUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpireDate",
                table: "Bill",
                newName: "DueDate");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Bill",
                newName: "Value");

            migrationBuilder.AddColumn<float>(
                name: "TotalBalance",
                table: "Users",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalExpense",
                table: "Users",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "TotalRevenue",
                table: "Users",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Bill",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalBalance",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalExpense",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalRevenue",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Bill");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Bill",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "Bill",
                newName: "ExpireDate");
        }
    }
}
