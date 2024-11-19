using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universidade.Migrations
{
    /// <inheritdoc />
    public partial class initialV1Apresentacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataRegistro",
                value: new DateTime(2024, 11, 19, 19, 8, 54, 400, DateTimeKind.Local).AddTicks(3261));

            migrationBuilder.UpdateData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataRegistro",
                value: new DateTime(2024, 11, 19, 19, 8, 54, 400, DateTimeKind.Local).AddTicks(3263));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataRegistro",
                value: new DateTime(2024, 11, 18, 22, 1, 51, 696, DateTimeKind.Local).AddTicks(8468));

            migrationBuilder.UpdateData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataRegistro",
                value: new DateTime(2024, 11, 18, 22, 1, 51, 696, DateTimeKind.Local).AddTicks(8470));
        }
    }
}
