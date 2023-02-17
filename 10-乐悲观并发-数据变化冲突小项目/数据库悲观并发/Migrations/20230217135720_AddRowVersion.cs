using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace 数据库悲观并发.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_houses",
                table: "houses");

            migrationBuilder.RenameTable(
                name: "houses",
                newName: "Houses");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Houses",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Houses",
                table: "Houses",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Houses",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Houses");

            migrationBuilder.RenameTable(
                name: "Houses",
                newName: "houses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_houses",
                table: "houses",
                column: "Id");
        }
    }
}
