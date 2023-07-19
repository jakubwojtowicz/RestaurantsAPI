using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantAPI.Migrations
{
    public partial class RestaurantUserIDAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByID",
                table: "Restaurants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_CreatedByID",
                table: "Restaurants",
                column: "CreatedByID");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Users_CreatedByID",
                table: "Restaurants",
                column: "CreatedByID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Users_CreatedByID",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_CreatedByID",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "CreatedByID",
                table: "Restaurants");
        }
    }
}
