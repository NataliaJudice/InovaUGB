using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InnovaCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class ADDSTATUS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SolicitacaoStatus",
                columns: new[] { "Id", "DataCadastro", "NomeStatus", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 28, 14, 1, 53, 494, DateTimeKind.Local).AddTicks(7254), "Enviada", true },
                    { 2, new DateTime(2026, 4, 28, 14, 1, 53, 494, DateTimeKind.Local).AddTicks(7268), "Aprovada", true },
                    { 3, new DateTime(2026, 4, 28, 14, 1, 53, 494, DateTimeKind.Local).AddTicks(7270), "Inviabilizada", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SolicitacaoStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SolicitacaoStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SolicitacaoStatus",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
