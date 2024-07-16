using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GridProjectPortals.DB.Migrations
{
    /// <inheritdoc />
    public partial class intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tblGridColumnDef",
                keyColumn: "Id",
                keyValue: 1,
                column: "GridName",
                value: "grdEmployeeDetails");

            migrationBuilder.UpdateData(
                table: "tblGridPages",
                keyColumn: "Id",
                keyValue: 1,
                column: "Page",
                value: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tblGridColumnDef",
                keyColumn: "Id",
                keyValue: 1,
                column: "GridName",
                value: "grdEmployee");

            migrationBuilder.UpdateData(
                table: "tblGridPages",
                keyColumn: "Id",
                keyValue: 1,
                column: "Page",
                value: "Employee");
        }
    }
}
