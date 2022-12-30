using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FakeCommerce.Api.Migrations
{
    public partial class updatedorderentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ShippingAddress_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingAddress_UserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryFee",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_UserId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "SubTotal",
                table: "Orders",
                newName: "Total");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Orders",
                newName: "SubTotal");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryFee",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ShippingAddress_UserId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddress_UserId",
                table: "Orders",
                column: "ShippingAddress_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ShippingAddress_UserId",
                table: "Orders",
                column: "ShippingAddress_UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
