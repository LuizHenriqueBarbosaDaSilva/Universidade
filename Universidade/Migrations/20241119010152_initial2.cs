using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universidade.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Data", "Matricula", "Nome" },
                values: new object[] { new DateOnly(2023, 9, 21), 202314593, "Maria Lopes" });

            migrationBuilder.UpdateData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Data", "Matricula", "Nome" },
                values: new object[] { new DateOnly(2023, 10, 22), 202314956, "Joao Carlos" });

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

            migrationBuilder.UpdateData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Data", "Matricula", "Nome" },
                values: new object[] { new DateOnly(2013, 1, 20), 20231214, "Jon Cleber" });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "Data", "Matricula", "Nome" },
                values: new object[] { 2, new DateOnly(2013, 1, 20), 20231215, "Leo John" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Data", "Matricula", "Nome" },
                values: new object[] { new DateOnly(2, 10, 20), 2, "John Doe" });

            migrationBuilder.UpdateData(
                table: "Alunos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Data", "Matricula", "Nome" },
                values: new object[] { new DateOnly(3, 10, 20), 3, "Jane Doe" });

            migrationBuilder.UpdateData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataRegistro",
                value: new DateTime(2024, 11, 17, 18, 6, 34, 108, DateTimeKind.Local).AddTicks(9606));

            migrationBuilder.UpdateData(
                table: "Disciplinas",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataRegistro",
                value: new DateTime(2024, 11, 17, 18, 6, 34, 108, DateTimeKind.Local).AddTicks(9609));

            migrationBuilder.UpdateData(
                table: "Professores",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Data", "Matricula", "Nome" },
                values: new object[] { new DateOnly(1, 1, 1), 1, "Jesus" });
        }
    }
}
