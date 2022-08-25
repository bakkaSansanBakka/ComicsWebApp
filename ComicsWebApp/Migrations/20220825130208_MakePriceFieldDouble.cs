using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicsWebApp.Migrations
{
    public partial class MakePriceFieldDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Comics",
                type: "float",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real",
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Comics",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(101)",
                oldMaxLength: 101);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Comics",
                type: "real",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Comics",
                type: "nvarchar(101)",
                maxLength: 101,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
