using Microsoft.EntityFrameworkCore.Migrations;

namespace BP.Data.Migrations
{
    public partial class InitialContext2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WholesalerBeer_Beers_BeerId",
                table: "WholesalerBeer");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesalerBeer_Wholesalers_WholesalerId",
                table: "WholesalerBeer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WholesalerBeer",
                table: "WholesalerBeer");

            migrationBuilder.RenameTable(
                name: "WholesalerBeer",
                newName: "WholesalerBeers");

            migrationBuilder.RenameIndex(
                name: "IX_WholesalerBeer_WholesalerId",
                table: "WholesalerBeers",
                newName: "IX_WholesalerBeers_WholesalerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WholesalerBeers",
                table: "WholesalerBeers",
                columns: new[] { "BeerId", "WholesalerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalerBeers_Beers_BeerId",
                table: "WholesalerBeers",
                column: "BeerId",
                principalTable: "Beers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalerBeers_Wholesalers_WholesalerId",
                table: "WholesalerBeers",
                column: "WholesalerId",
                principalTable: "Wholesalers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WholesalerBeers_Beers_BeerId",
                table: "WholesalerBeers");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesalerBeers_Wholesalers_WholesalerId",
                table: "WholesalerBeers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WholesalerBeers",
                table: "WholesalerBeers");

            migrationBuilder.RenameTable(
                name: "WholesalerBeers",
                newName: "WholesalerBeer");

            migrationBuilder.RenameIndex(
                name: "IX_WholesalerBeers_WholesalerId",
                table: "WholesalerBeer",
                newName: "IX_WholesalerBeer_WholesalerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WholesalerBeer",
                table: "WholesalerBeer",
                columns: new[] { "BeerId", "WholesalerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalerBeer_Beers_BeerId",
                table: "WholesalerBeer",
                column: "BeerId",
                principalTable: "Beers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalerBeer_Wholesalers_WholesalerId",
                table: "WholesalerBeer",
                column: "WholesalerId",
                principalTable: "Wholesalers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
