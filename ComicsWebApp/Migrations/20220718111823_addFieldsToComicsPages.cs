using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicsWebApp.Migrations
{
    public partial class addFieldsToComicsPages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "ComicsPages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ComicsPages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "ComicsPages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ComicsPages");
        }
    }
}
