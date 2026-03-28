using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnovaCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaSetor3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_AspNetUsers_UsuarioId",
                table: "Solicitacoes");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Solicitacoes",
                newName: "IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_Solicitacoes_UsuarioId",
                table: "Solicitacoes",
                newName: "IX_Solicitacoes_IdUsuario");

            migrationBuilder.AddColumn<int>(
                name: "IdSetor",
                table: "Solicitacoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Setor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IdUsuario = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setor_AspNetUsers_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_IdSetor",
                table: "Solicitacoes",
                column: "IdSetor");

            migrationBuilder.CreateIndex(
                name: "IX_Setor_IdUsuario",
                table: "Setor",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_AspNetUsers_IdUsuario",
                table: "Solicitacoes",
                column: "IdUsuario",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Setor_IdSetor",
                table: "Solicitacoes",
                column: "IdSetor",
                principalTable: "Setor",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_AspNetUsers_IdUsuario",
                table: "Solicitacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Setor_IdSetor",
                table: "Solicitacoes");

            migrationBuilder.DropTable(
                name: "Setor");

            migrationBuilder.DropIndex(
                name: "IX_Solicitacoes_IdSetor",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "IdSetor",
                table: "Solicitacoes");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Solicitacoes",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Solicitacoes_IdUsuario",
                table: "Solicitacoes",
                newName: "IX_Solicitacoes_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_AspNetUsers_UsuarioId",
                table: "Solicitacoes",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
