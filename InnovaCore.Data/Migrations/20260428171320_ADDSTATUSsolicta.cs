using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InnovaCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class ADDSTATUSsolicta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SolicitacaoStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCadastro",
                value: new DateTime(2026, 4, 28, 14, 13, 19, 868, DateTimeKind.Local).AddTicks(9870));

            migrationBuilder.UpdateData(
                table: "SolicitacaoStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCadastro",
                value: new DateTime(2026, 4, 28, 14, 13, 19, 868, DateTimeKind.Local).AddTicks(9872));

            migrationBuilder.UpdateData(
                table: "SolicitacaoStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCadastro",
                value: new DateTime(2026, 4, 28, 14, 13, 19, 868, DateTimeKind.Local).AddTicks(9873));

            migrationBuilder.InsertData(
                table: "TarefaStatus",
                columns: new[] { "Id", "DataCadastro", "Nome", "Status" },
                values: new object[,]
                {
                    { 7, new DateTime(2026, 4, 28, 14, 13, 19, 868, DateTimeKind.Local).AddTicks(9720), "Pendente", true },
                    { 8, new DateTime(2026, 4, 28, 14, 13, 19, 868, DateTimeKind.Local).AddTicks(9733), "Em Andamento", true },
                    { 9, new DateTime(2026, 4, 28, 14, 13, 19, 868, DateTimeKind.Local).AddTicks(9735), "Concluída", true }
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

            migrationBuilder.UpdateData(
                table: "SolicitacaoStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCadastro",
                value: new DateTime(2026, 4, 28, 14, 10, 47, 125, DateTimeKind.Local).AddTicks(835));

            migrationBuilder.UpdateData(
                table: "SolicitacaoStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataCadastro",
                value: new DateTime(2026, 4, 28, 14, 10, 47, 125, DateTimeKind.Local).AddTicks(854));

            migrationBuilder.UpdateData(
                table: "SolicitacaoStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCadastro",
                value: new DateTime(2026, 4, 28, 14, 10, 47, 125, DateTimeKind.Local).AddTicks(856));
        }
    }
}
