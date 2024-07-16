using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GridProjectPortals.DB.Migrations
{
    /// <inheritdoc />
    public partial class intial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblGridColumnDef",
                table: "tblGridColumnDef");

            migrationBuilder.RenameTable(
                name: "tblGridColumnDef",
                newName: "tblGridColumnDefs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblGridColumnDefs",
                table: "tblGridColumnDefs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblGridColumnDefs",
                table: "tblGridColumnDefs");

            migrationBuilder.RenameTable(
                name: "tblGridColumnDefs",
                newName: "tblGridColumnDef");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblGridColumnDef",
                table: "tblGridColumnDef",
                column: "Id");
        }
    }
}
