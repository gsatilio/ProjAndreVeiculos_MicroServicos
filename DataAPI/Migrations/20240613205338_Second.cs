using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAPI.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Boleto_BoletoId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_CreditCard_CreditCardId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Pix_PixId",
                table: "Payment");

            migrationBuilder.AlterColumn<int>(
                name: "PixId",
                table: "Payment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CreditCardId",
                table: "Payment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BoletoId",
                table: "Payment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Boleto_BoletoId",
                table: "Payment",
                column: "BoletoId",
                principalTable: "Boleto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_CreditCard_CreditCardId",
                table: "Payment",
                column: "CreditCardId",
                principalTable: "CreditCard",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Pix_PixId",
                table: "Payment",
                column: "PixId",
                principalTable: "Pix",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Boleto_BoletoId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_CreditCard_CreditCardId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Pix_PixId",
                table: "Payment");

            migrationBuilder.AlterColumn<int>(
                name: "PixId",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreditCardId",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BoletoId",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Boleto_BoletoId",
                table: "Payment",
                column: "BoletoId",
                principalTable: "Boleto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_CreditCard_CreditCardId",
                table: "Payment",
                column: "CreditCardId",
                principalTable: "CreditCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Pix_PixId",
                table: "Payment",
                column: "PixId",
                principalTable: "Pix",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
