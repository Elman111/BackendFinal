﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackFinal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAboutArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AboutAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Head = table.Column<string>(type: "TEXT", nullable: true),
                    TextHead = table.Column<string>(type: "TEXT", nullable: true),
                    TextBottom = table.Column<string>(type: "TEXT", nullable: true),
                    Url = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutAreas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutAreas");
        }
    }
}
