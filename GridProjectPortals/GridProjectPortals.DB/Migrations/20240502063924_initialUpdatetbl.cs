using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GridProjectPortals.DB.Migrations
{
    /// <inheritdoc />
    public partial class initialUpdatetbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "tblEmployees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "tblEmployees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "tblEmployees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "tblEmployees");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "tblEmployees");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "tblEmployees");
        }
    }
}
