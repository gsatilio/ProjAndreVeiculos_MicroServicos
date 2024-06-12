using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIInsurance.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "Insurance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerDocument = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Deductible = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarLicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MainConductorDocument = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurance_Car_CarLicensePlate",
                        column: x => x.CarLicensePlate,
                        principalTable: "Car",
                        principalColumn: "LicensePlate",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Insurance_Conductor_MainConductorDocument",
                        column: x => x.MainConductorDocument,
                        principalTable: "Conductor",
                        principalColumn: "Document",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Insurance_Customer_CustomerDocument",
                        column: x => x.CustomerDocument,
                        principalTable: "Customer",
                        principalColumn: "Document");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conductor_AddressId",
                table: "Conductor",
                column: "AddressId");


            migrationBuilder.CreateIndex(
                name: "IX_Insurance_MainConductorDocument",
                table: "Insurance",
                column: "MainConductorDocument");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Insurance");

        }
    }
}
