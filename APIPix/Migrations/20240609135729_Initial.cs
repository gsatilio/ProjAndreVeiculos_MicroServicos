﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIPix.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Pix_PixTypeId",
                table: "Pix",
                column: "PixTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pix");

            migrationBuilder.DropTable(
                name: "PixType");
        }
    }
}
