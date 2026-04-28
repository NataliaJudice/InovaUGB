using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnovaCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class ajusteTarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "NomeResponsavel",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: true);

           
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeResponsavel",
                table: "Tarefas");

           

           
        }
    }
}
