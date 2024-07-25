using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GridProjectPortals.DB.Migrations
{
    /// <inheritdoc />
    public partial class db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FailedLoginAttempts = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblEmployees_tblEmployees_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "tblEmployees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblEmployees_tblEmployees_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "tblEmployees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblGridColumnDefs",
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
                    table.PrimaryKey("PK_tblGridColumnDefs", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "tblComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplayComment = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblComments_tblComments_ReplayComment",
                        column: x => x.ReplayComment,
                        principalTable: "tblComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblComments_tblEmployees_UserId",
                        column: x => x.UserId,
                        principalTable: "tblEmployees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblReplayComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReplayComments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblReplayComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblReplayComments_tblComments_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "tblComments",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "tblGridColumnDefs",
                columns: new[] { "Id", "GridName", "PageId", "UserId" },
                values: new object[] { 1, "grdEmployeeDetails", 1, 1 });

            migrationBuilder.InsertData(
                table: "tblGridColumnDetails",
                columns: new[] { "Id", "ColumnDataType", "ColumnName", "GridId", "Sequence", "isFix", "isSearchable", "isVisible" },
                values: new object[,]
                {
                    { 1, "int", "id", 1, 1, false, false, false },
                    { 2, "string", "firstname", 1, 2, false, true, true },
                    { 3, "string", "lastname", 1, 3, false, true, true },
                    { 4, "string", "username", 1, 4, false, true, true },
                    { 5, "string", "phone", 1, 5, false, true, true }
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

            migrationBuilder.InsertData(
                table: "tblGridPages",
                columns: new[] { "Id", "Page" },
                values: new object[] { 1, "Employees" });

            migrationBuilder.CreateIndex(
                name: "IX_tblComments_ReplayComment",
                table: "tblComments",
                column: "ReplayComment");

            migrationBuilder.CreateIndex(
                name: "IX_tblComments_UserId",
                table: "tblComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployees_CreatedBy",
                table: "tblEmployees",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployees_UpdatedBy",
                table: "tblEmployees",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tblReplayComments_CommentsId",
                table: "tblReplayComments",
                column: "CommentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblGridColumnDefs");

            migrationBuilder.DropTable(
                name: "tblGridColumnDetails");

            migrationBuilder.DropTable(
                name: "tblGridDataTypeOperators");

            migrationBuilder.DropTable(
                name: "tblGridPages");

            migrationBuilder.DropTable(
                name: "tblReplayComments");

            migrationBuilder.DropTable(
                name: "tblComments");

            migrationBuilder.DropTable(
                name: "tblEmployees");
        }
    }
}
