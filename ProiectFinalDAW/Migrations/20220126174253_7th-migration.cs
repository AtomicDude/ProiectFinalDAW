using Microsoft.EntityFrameworkCore.Migrations;

namespace ProiectFinalDAW.Migrations
{
    public partial class _7thmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Produsul nu exista",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Produsul nu exista",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.Sql(
            @"
                CREATE TRIGGER [dbo].[FavouriteAddresses_UPDATE] ON [dbo].[FavouriteAddresses]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    DECLARE @auxId uniqueidentifier

                    SELECT @auxId = INSERTED.Id
                    FROM INSERTED

                    UPDATE dbo.FavouriteAddresses
                    SET DateModified = GETDATE()
                    WHERE Id = @auxId
                END
            ");

            migrationBuilder.Sql(
            @"
                CREATE TRIGGER [dbo].[Users_UPDATE] ON [dbo].[User]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    DECLARE @auxId uniqueidentifier

                    SELECT @auxId = INSERTED.Id
                    FROM INSERTED

                    UPDATE dbo.Users
                    SET DateModified = GETDATE()
                    WHERE Id = @auxId
                END
            ");

            migrationBuilder.Sql(
            @"
                CREATE TRIGGER [dbo].[Orders_UPDATE] ON [dbo].[Orders]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    DECLARE @auxId uniqueidentifier

                    SELECT @auxId = INSERTED.Id
                    FROM INSERTED

                    UPDATE dbo.Orders
                    SET DateModified = GETDATE()
                    WHERE Id = @auxId
                END
            ");

            migrationBuilder.Sql(
            @"
                CREATE TRIGGER [dbo].[OrderDetails_UPDATE] ON [dbo].[OrderDetails]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    DECLARE @auxId uniqueidentifier

                    SELECT @auxId = INSERTED.Id
                    FROM INSERTED

                    UPDATE dbo.OrderDetails
                    SET DateModified = GETDATE()
                    WHERE Id = @auxId
                END
            ");

            migrationBuilder.Sql(
            @"
                CREATE TRIGGER [dbo].[Products_UPDATE] ON [dbo].[Products]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    DECLARE @auxId uniqueidentifier

                    SELECT @auxId = INSERTED.Id
                    FROM INSERTED

                    UPDATE dbo.Products
                    SET DateModified = GETDATE()
                    WHERE Id = @auxId
                END
            ");

            migrationBuilder.Sql(
            @"
                CREATE TRIGGER [dbo].[Categories_UPDATE] ON [dbo].[Categories]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    DECLARE @auxId uniqueidentifier

                    SELECT @auxId = INSERTED.Id
                    FROM INSERTED

                    UPDATE dbo.Categories
                    SET DateModified = GETDATE()
                    WHERE Id = @auxId
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Produsul nu exista");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "Produsul nu exista");

            migrationBuilder.AlterColumn<bool>(
                name: "Active",
                table: "Products",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
