using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GridProjectPortals.DB.Migrations
{
    /// <inheritdoc />
    public partial class intiailCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblGridDataTypeOperators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorInWords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Operator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGridDataTypeOperators", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "tblGridDataTypeOperators",
                columns: new[] { "Id", "DataType", "Operator", "OperatorInWords" },
                values: new object[,]
                {
                    { 1, "int", ">", "GreaterThan" },
                    { 2, "int", "<", "LessThan" },
                    { 3, "int", "=", "EqualsTo" },
                    { 4, "string", "", "StartsWith" },
                    { 5, "string", "", "EndsWith" },
                    { 6, "string", "", "Contains" },
                    { 7, "string", "", "In" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblGridDataTypeOperators");
        }
    }
}
