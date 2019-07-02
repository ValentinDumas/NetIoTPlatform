using Microsoft.EntityFrameworkCore.Migrations;

namespace CalculEngineEndpoint.Migrations
{
    public partial class InitialCreate6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "result",
                table: "Calcul",
                newName: "sum");

            migrationBuilder.AddColumn<float>(
                name: "average",
                table: "Calcul",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "max",
                table: "Calcul",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "min",
                table: "Calcul",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Calcul",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "average",
                table: "Calcul");

            migrationBuilder.DropColumn(
                name: "max",
                table: "Calcul");

            migrationBuilder.DropColumn(
                name: "min",
                table: "Calcul");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Calcul");

            migrationBuilder.RenameColumn(
                name: "sum",
                table: "Calcul",
                newName: "result");
        }
    }
}
