using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommentSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentSet_ArticleSet_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "ArticleSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliverySet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliverySet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliverySet_OrderSet_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Stu_Tea",
                columns: table => new
                {
                    StudentsId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Stu_Tea", x => new { x.StudentsId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_T_Stu_Tea_StudentSet_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "StudentSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_Stu_Tea_TeacherSet_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "TeacherSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentSet_ArticleId",
                table: "CommentSet",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliverySet_OrderId",
                table: "DeliverySet",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_Stu_Tea_TeachersId",
                table: "T_Stu_Tea",
                column: "TeachersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentSet");

            migrationBuilder.DropTable(
                name: "DeliverySet");

            migrationBuilder.DropTable(
                name: "T_Stu_Tea");

            migrationBuilder.DropTable(
                name: "ArticleSet");

            migrationBuilder.DropTable(
                name: "OrderSet");

            migrationBuilder.DropTable(
                name: "StudentSet");

            migrationBuilder.DropTable(
                name: "TeacherSet");
        }
    }
}
