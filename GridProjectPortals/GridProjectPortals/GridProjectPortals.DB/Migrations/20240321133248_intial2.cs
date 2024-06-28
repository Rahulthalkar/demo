using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GridProjectPortals.DB.Migrations
{
    /// <inheritdoc />
    public partial class intial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tblGridColumnDetails",
                keyColumn: "Id",
                keyValue: 2,
                column: "ColumnName",
                value: "firstname");

            migrationBuilder.UpdateData(
                table: "tblGridColumnDetails",
                keyColumn: "Id",
                keyValue: 3,
                column: "ColumnName",
                value: "lastname");

            migrationBuilder.UpdateData(
                table: "tblGridColumnDetails",
                keyColumn: "Id",
                keyValue: 4,
                column: "ColumnName",
                value: "username");

            migrationBuilder.UpdateData(
                table: "tblGridColumnDetails",
                keyColumn: "Id",
                keyValue: 5,
                column: "ColumnName",
                value: "phone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tblGridColumnDetails",
                keyColumn: "Id",
                keyValue: 2,
                column: "ColumnName",
                value: "FirstName");

            migrationBuilder.UpdateData(
                table: "tblGridColumnDetails",
                keyColumn: "Id",
                keyValue: 3,
                column: "ColumnName",
                value: "lastName");

            migrationBuilder.UpdateData(
                table: "tblGridColumnDetails",
                keyColumn: "Id",
                keyValue: 4,
                column: "ColumnName",
                value: "UserName");

            migrationBuilder.UpdateData(
                table: "tblGridColumnDetails",
                keyColumn: "Id",
                keyValue: 5,
                column: "ColumnName",
                value: "Phone");
        }
    }
}
