using Microsoft.EntityFrameworkCore.Migrations;

namespace BP.Data.Migrations
{
    public partial class ChangeToAlcoholPercentage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlcoolPercentage",
                table: "Beers");

            migrationBuilder.AddColumn<double>(
                name: "AlcoholPercentage",
                table: "Beers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 1,
                column: "AlcoholPercentage",
                value: 6.5999999999999996);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 2,
                column: "AlcoholPercentage",
                value: 8.5999999999999996);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 3,
                column: "AlcoholPercentage",
                value: 7.5);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 4,
                column: "AlcoholPercentage",
                value: 8.8000000000000007);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 5,
                column: "AlcoholPercentage",
                value: 7.9000000000000004);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlcoholPercentage",
                table: "Beers");

            migrationBuilder.AddColumn<double>(
                name: "AlcoolPercentage",
                table: "Beers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 1,
                column: "AlcoolPercentage",
                value: 6.5999999999999996);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 2,
                column: "AlcoolPercentage",
                value: 8.5999999999999996);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 3,
                column: "AlcoolPercentage",
                value: 7.5);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 4,
                column: "AlcoolPercentage",
                value: 8.8000000000000007);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 5,
                column: "AlcoolPercentage",
                value: 7.9000000000000004);
        }
    }
}
