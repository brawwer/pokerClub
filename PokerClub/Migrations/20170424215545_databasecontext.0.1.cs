using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PokerClub.Migrations
{
    public partial class databasecontext01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokerTables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokerTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValueCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokerTableHasMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<string>(nullable: true),
                    PokerTableId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokerTableHasMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokerTableHasMembers_PokerTables_PokerTableId",
                        column: x => x.PokerTableId,
                        principalTable: "PokerTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PokerTableValuations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    MemberId = table.Column<string>(nullable: true),
                    PokerTableId = table.Column<int>(nullable: false),
                    ValueCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokerTableValuations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokerTableValuations_PokerTables_PokerTableId",
                        column: x => x.PokerTableId,
                        principalTable: "PokerTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokerTableValuations_ValueCategories_ValueCategoryId",
                        column: x => x.ValueCategoryId,
                        principalTable: "ValueCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokerTableHasMembers_PokerTableId",
                table: "PokerTableHasMembers",
                column: "PokerTableId");

            migrationBuilder.CreateIndex(
                name: "IX_PokerTableValuations_PokerTableId",
                table: "PokerTableValuations",
                column: "PokerTableId");

            migrationBuilder.CreateIndex(
                name: "IX_PokerTableValuations_ValueCategoryId",
                table: "PokerTableValuations",
                column: "ValueCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokerTableHasMembers");

            migrationBuilder.DropTable(
                name: "PokerTableValuations");

            migrationBuilder.DropTable(
                name: "PokerTables");

            migrationBuilder.DropTable(
                name: "ValueCategories");
        }
    }
}
