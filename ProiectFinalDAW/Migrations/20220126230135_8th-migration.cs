using Microsoft.EntityFrameworkCore.Migrations;

namespace ProiectFinalDAW.Migrations
{
    public partial class _8thmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_User_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_User_FavouriteAddresses_FavouriteAddressId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_User_FavouriteAddressId",
                table: "Users",
                newName: "IX_Users_FavouriteAddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FavouriteAddresses_FavouriteAddressId",
                table: "Users",
                column: "FavouriteAddressId",
                principalTable: "FavouriteAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_FavouriteAddresses_FavouriteAddressId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Users_FavouriteAddressId",
                table: "User",
                newName: "IX_User_FavouriteAddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_User_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_FavouriteAddresses_FavouriteAddressId",
                table: "User",
                column: "FavouriteAddressId",
                principalTable: "FavouriteAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
