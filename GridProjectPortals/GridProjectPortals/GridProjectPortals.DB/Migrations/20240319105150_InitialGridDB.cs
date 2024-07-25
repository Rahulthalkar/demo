using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GridProjectPortals.DB.Migrations
{
    /// <inheritdoc />
    public partial class InitialGridDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblGridColumnDef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GridName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGridColumnDef", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblGridColumnDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GridId = table.Column<int>(type: "int", nullable: true),
                    ColumnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnDataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isSearchable = table.Column<bool>(type: "bit", nullable: true),
                    isVisible = table.Column<bool>(type: "bit", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: true),
                    isFix = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGridColumnDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblGridPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Page = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGridPages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "tblGridColumnDef",
                columns: new[] { "Id", "GridName", "PageId", "UserId" },
                values: new object[] { 1, "grdEmployee", 1, 1 });

            migrationBuilder.InsertData(
                table: "tblGridColumnDetails",
                columns: new[] { "Id", "ColumnDataType", "ColumnName", "GridId", "Sequence", "isFix", "isSearchable", "isVisible" },
                values: new object[,]
                {
                    { 1, "int", "id", 1, 1, false, false, false },
                    { 2, "string", "FirstName", 1, 2, false, true, true },
                    { 3, "string", "lastName", 1, 3, false, true, true },
                    { 4, "string", "UserName", 1, 4, false, true, true },
                    { 5, "string", "Phone", 1, 5, false, true, true }
                });

            migrationBuilder.InsertData(
                table: "tblGridPages",
                columns: new[] { "Id", "Page" },
                values: new object[] { 1, "Employee" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblGridColumnDef");

            migrationBuilder.DropTable(
                name: "tblGridColumnDetails");

            migrationBuilder.DropTable(
                name: "tblGridPages");
        }
    }
}
