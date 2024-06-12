using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIDependent.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "Dependent",
                columns: table => new
                {
                    Document = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerDocument = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependent", x => x.Document);
                    table.ForeignKey(
                        name: "FK_Dependent_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dependent_Customer_CustomerDocument",
                        column: x => x.CustomerDocument,
                        principalTable: "Customer",
                        principalColumn: "Document");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dependent_AddressId",
                table: "Dependent",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependent_CustomerDocument",
                table: "Dependent",
                column: "CustomerDocument");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependent");
        }
    }
}
