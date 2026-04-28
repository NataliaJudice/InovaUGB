using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InnovaCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class ADDSTATUStarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "TarefaStatus",
                columns: new[] { "Id", "DataCadastro", "Nome", "Status" },
                values: new object[,]
                {
                    { 7, new DateTime(2026, 4, 28, 14, 5, 50, 135, DateTimeKind.Local).AddTicks(7137), "Pendente", true },
                    { 8, new DateTime(2026, 4, 28, 14, 5, 50, 135, DateTimeKind.Local).AddTicks(7149), "Em Andamento", true },
                    { 9, new DateTime(2026, 4, 28, 14, 5, 50, 135, DateTimeKind.Local).AddTicks(7151), "Concluída", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TarefaStatus",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TarefaStatus",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TarefaStatus",
                keyColumn: "Id",
                keyValue: 9);

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
    }
}
