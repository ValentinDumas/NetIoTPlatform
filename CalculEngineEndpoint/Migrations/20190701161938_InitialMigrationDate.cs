using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CalculEngineEndpoint.Migrations
{
    public partial class InitialMigrationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "date",
                table: "Calcul",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "date",
                table: "Calcul",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
