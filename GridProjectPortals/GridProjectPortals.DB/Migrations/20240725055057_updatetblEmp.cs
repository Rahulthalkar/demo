using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GridProjectPortals.DB.Migrations
{
    /// <inheritdoc />
    public partial class updatetblEmp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "tblEmployees",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "tblEmployees");
        }
    }
}
