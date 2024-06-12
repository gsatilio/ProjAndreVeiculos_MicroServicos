using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDriver.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverLicense",
                columns: table => new
                {
                    DriverId = table.Column<long>(type: "bigint", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverLicense", x => x.DriverId);
                    table.ForeignKey(
                        name: "FK_DriverLicense_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conductor",
                columns: table => new
                {
                    Document = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DriverLicenseDriverId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conductor", x => x.Document);
                    table.ForeignKey(
                        name: "FK_Conductor_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conductor_DriverLicense_DriverLicenseDriverId",
                        column: x => x.DriverLicenseDriverId,
                        principalTable: "DriverLicense",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conductor_DriverLicenseDriverId",
                table: "Conductor",
                column: "DriverLicenseDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverLicense_CategoryId",
                table: "DriverLicense",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conductor");

            migrationBuilder.DropTable(
                name: "DriverLicense");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
