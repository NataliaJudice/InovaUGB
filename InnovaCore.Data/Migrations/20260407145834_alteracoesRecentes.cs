using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnovaCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class alteracoesRecentes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdUsuario",
                table: "Setor",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.Sql(@"
            EXEC('CREATE VIEW [dbo].[VwQtdePorSetor] AS 
            SELECT
                so.IdSetor,
                s.Nome, 
                COUNT(SO.Id) AS QTDE_POR_SETOR
            FROM Solicitacoes SO
            LEFT JOIN Setor S ON S.Id = SO.IdSetor 
            GROUP BY so.IdSetor, s.Nome')
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdUsuario",
                table: "Setor",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.Sql("DROP VIEW [dbo].[VwQtdePorSetor]");
        }
    }
}
