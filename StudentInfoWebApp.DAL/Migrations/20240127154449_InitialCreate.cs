using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentInfoWebApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COURSES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COURSES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GROUPS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GROUPS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GROUPS_COURSES_CourseId",
                        column: x => x.CourseId,
                        principalTable: "COURSES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STUDENTS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_STUDENTS_GROUPS_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GROUPS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_COURSES_Name",
                table: "COURSES",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GROUPS_CourseId",
                table: "GROUPS",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_GROUPS_Name",
                table: "GROUPS",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_STUDENTS_GroupId",
                table: "STUDENTS",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STUDENTS");

            migrationBuilder.DropTable(
                name: "GROUPS");

            migrationBuilder.DropTable(
                name: "COURSES");
        }
    }
}
