using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackFinal.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacherAreaSocial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherAreaSocials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Facebook = table.Column<string>(type: "TEXT", nullable: true),
                    Penterest = table.Column<string>(type: "TEXT", nullable: true),
                    Viemo = table.Column<string>(type: "TEXT", nullable: true),
                    Twitter = table.Column<string>(type: "TEXT", nullable: true),
                    TeacherAreaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherAreaSocials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherAreaSocials_TeacherAreas_TeacherAreaId",
                        column: x => x.TeacherAreaId,
                        principalTable: "TeacherAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherAreaSocials_TeacherAreaId",
                table: "TeacherAreaSocials",
                column: "TeacherAreaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherAreaSocials");
        }
    }
}
