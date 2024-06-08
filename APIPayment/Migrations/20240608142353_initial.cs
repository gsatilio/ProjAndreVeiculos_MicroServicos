using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIPayment.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boleto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boleto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PixType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PixType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pix",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PixTypeId = table.Column<int>(type: "int", nullable: false),
                    PixKey = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pix", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pix_PixType_PixTypeId",
                        column: x => x.PixTypeId,
                        principalTable: "PixType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditCardId = table.Column<int>(type: "int", nullable: false),
                    BoletoId = table.Column<int>(type: "int", nullable: false),
                    PixId = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Boleto_BoletoId",
                        column: x => x.BoletoId,
                        principalTable: "Boleto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_CreditCard_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payment_Pix_PixId",
                        column: x => x.PixId,
                        principalTable: "Pix",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BoletoId",
                table: "Payment",
                column: "BoletoId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CreditCardId",
                table: "Payment",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PixId",
                table: "Payment",
                column: "PixId");

            migrationBuilder.CreateIndex(
                name: "IX_Pix_PixTypeId",
                table: "Pix",
                column: "PixTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Boleto");

            migrationBuilder.DropTable(
                name: "CreditCard");

            migrationBuilder.DropTable(
                name: "Pix");

            migrationBuilder.DropTable(
                name: "PixType");
        }
    }
}
