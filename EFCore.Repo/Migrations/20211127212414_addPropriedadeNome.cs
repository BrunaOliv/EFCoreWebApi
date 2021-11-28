using Microsoft.EntityFrameworkCore.Migrations;


namespace EFCore.Repo.Migrations
{
    public partial class addPropriedadeNome : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Batalhas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Batalhas");
        }
    }
}
