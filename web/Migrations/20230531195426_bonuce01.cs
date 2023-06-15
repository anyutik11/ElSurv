using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Migrations
{
    public partial class bonuce01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Company",
                type: "varchar(1024)",
                maxLength: 1024,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "phoneForClient",
                table: "Company",
                type: "varchar(11)",
                maxLength: 11,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bonuce",
                columns: table => new
                {
                    id = table.Column<string>(type: "char(22)", maxLength: 22, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dtd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    sum = table.Column<int>(type: "int", nullable: false),
                    remark = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    companyId = table.Column<string>(type: "char(22)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    guestId = table.Column<string>(type: "char(22)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonuce", x => x.id);
                    table.ForeignKey(
                        name: "FK_Bonuce_Company_companyId",
                        column: x => x.companyId,
                        principalTable: "Company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bonuce_Guest_guestId",
                        column: x => x.guestId,
                        principalTable: "Guest",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Bonuce_companyId",
                table: "Bonuce",
                column: "companyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonuce_dtd_companyId",
                table: "Bonuce",
                columns: new[] { "dtd", "companyId" });

            migrationBuilder.CreateIndex(
                name: "IX_Bonuce_guestId",
                table: "Bonuce",
                column: "guestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bonuce");

            migrationBuilder.DropColumn(
                name: "address",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "phoneForClient",
                table: "Company");
        }
    }
}
