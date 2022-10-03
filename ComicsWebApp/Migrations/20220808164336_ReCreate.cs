using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicsWebApp.Migrations
{
    public partial class ReCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ComicsGenres",
                columns: new[] { "Id", "GenreName" },
                values: new object[,]
                {
                    { 1, "Detective" },
                    { 2, "Historical" },
                    { 3, "Science Fiction" },
                    { 4, "Educational" },
                    { 5, "Adventure" },
                    { 6, "Romantic" },
                    { 7, "Horror" },
                    { 8, "Fantasy" },
                    { 9, "Humor" }
                });

            migrationBuilder.AddColumn<string>(
                name: "CoverType",
                table: "Comics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "Comics",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PublicationFormat",
                table: "Comics",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Comics",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Comics",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.RenameColumn(
                name: "YearOfPublisihing",
                table: "Comics",
                newName: "YearOfPublication");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ComicsGenres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ComicsGenres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ComicsGenres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ComicsGenres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ComicsGenres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ComicsGenres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ComicsGenres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ComicsGenres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ComicsGenres",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DropColumn(
                name: "CoverType",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "ComicsPages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ComicsPages");

            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "Comics",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "PublicationFormat",
                table: "Comics",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Comics",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Comics",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.RenameColumn(
                name: "YearOfPublication",
                table: "Comics",
                newName: "YearOfPublisihing");
        }
    }
}
