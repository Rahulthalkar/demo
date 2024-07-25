using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GridProjectPortals.DB.Migrations
{
    /// <inheritdoc />
    public partial class initialUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "tblEmployees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "tblEmployees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FailedLoginAttempts",
                table: "tblEmployees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tblEmployees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "tblEmployees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "tblEmployees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "tblEmployees",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployees_CreatedBy",
                table: "tblEmployees",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployees_UpdatedBy",
                table: "tblEmployees",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_tblEmployees_tblEmployees_CreatedBy",
                table: "tblEmployees",
                column: "CreatedBy",
                principalTable: "tblEmployees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblEmployees_tblEmployees_UpdatedBy",
                table: "tblEmployees",
                column: "UpdatedBy",
                principalTable: "tblEmployees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblEmployees_tblEmployees_CreatedBy",
                table: "tblEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_tblEmployees_tblEmployees_UpdatedBy",
                table: "tblEmployees");

            migrationBuilder.DropIndex(
                name: "IX_tblEmployees_CreatedBy",
                table: "tblEmployees");

            migrationBuilder.DropIndex(
                name: "IX_tblEmployees_UpdatedBy",
                table: "tblEmployees");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "tblEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "tblEmployees");

            migrationBuilder.DropColumn(
                name: "FailedLoginAttempts",
                table: "tblEmployees");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tblEmployees");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "tblEmployees");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "tblEmployees");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "tblEmployees");
        }
    }
}
