using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIConductor.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Uf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });
            */
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
                name: "CNH",
                columns: table => new
                {
                    DriverLicense = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNH", x => x.DriverLicense);
                    table.ForeignKey(
                        name: "FK_CNH_Category_CategoryId",
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
                    DriverLicense = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conductor", x => x.Document);
                    /*table.ForeignKey(
                        name: "FK_Conductor_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);*/
                    table.ForeignKey(
                        name: "FK_Conductor_CNH_DriverLicense1",
                        column: x => x.DriverLicense,
                        principalTable: "CNH",
                        principalColumn: "DriverLicense",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CNH_CategoryId",
                table: "CNH",
                column: "CategoryId");
            /*
            migrationBuilder.CreateIndex(
                name: "IX_Conductor_AddressId",
                table: "Conductor",
                column: "AddressId");
            */
            migrationBuilder.CreateIndex(
                name: "IX_Conductor_DriverLicense1",
                table: "Conductor",
                column: "DriverLicense");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conductor");
            /*
            migrationBuilder.DropTable(
                name: "Address");
            */
            migrationBuilder.DropTable(
                name: "CNH");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
