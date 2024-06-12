using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIFinancing.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "Financing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    FinancingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankCNPJ = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Financing_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Financing");

        }
    }
}
