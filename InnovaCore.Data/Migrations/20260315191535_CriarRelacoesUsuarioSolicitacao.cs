using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnovaCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriarRelacoesUsuarioSolicitacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioAlteracaoStatus",
                table: "Solicitacoes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_UsuarioAlteracaoStatus",
                table: "Solicitacoes",
                column: "UsuarioAlteracaoStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_AspNetUsers_UsuarioAlteracaoStatus",
                table: "Solicitacoes",
                column: "UsuarioAlteracaoStatus",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_AspNetUsers_UsuarioAlteracaoStatus",
                table: "Solicitacoes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitacoes_UsuarioAlteracaoStatus",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "UsuarioAlteracaoStatus",
                table: "Solicitacoes");
        }
    }
}
